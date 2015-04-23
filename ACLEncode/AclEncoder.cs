using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ACLencoder
{
    /// <summary>
    /// A class that allows you to encode and decode files as ACL entries
    /// </summary>
    public class AclEncoder
    {
        /// <summary>
        /// This combination of FileSystemRights flags identifies an ACL entry as part of the AclEncoding scheme.
        /// </summary>
        private const FileSystemRights CANARY_RIGHTS = FileSystemRights.Synchronize | FileSystemRights.ReadPermissions;

        private const int   CANARY_VALUE = (int)CANARY_RIGHTS; // FileSystemRights.Synchronize + FileSystemRights.ReadPermissions
        private const int   SID_LENGTH = 68;
        private const int   SID_HEADER_LENGTH = 8;
        private const int   MAX_CHUNK_LENGTH = 60;
        private const int   MAX_ROUNDS = 512;
        private ArrayList   _fileList;

        /// <summary>
        /// Instantiates a new AclEncoder that can be used to encode/decode files as ACL entries
        /// </summary>
        /// <param name="fileList">A path to the list of files to use for encoding/decoding</param>
        public AclEncoder(string fileList)
        {
            this.LoadFileList(fileList);
        }

        ~AclEncoder()
        {
            _fileList = null;
        }

        /// <summary>
        /// Loads the list of files identified at the <paramref name="filePath"/>
        /// </summary>
        /// <param name="filePath">A path to the list of files to use for encoding/decoding</param>
        private void LoadFileList(string filePath)
        {
            // Scan the list of drives available to the system and remember NTFS drives
            ArrayList NTFSDrives = new ArrayList();
            DriveInfo[] DriveList = DriveInfo.GetDrives();
            foreach (DriveInfo Info in DriveList)
            {
                if (Info.IsReady == true)
                {
                    if (Info.DriveFormat.Equals("NTFS"))
                        NTFSDrives.Add(Info.Name);
                }
            }

            // Open the file for reading
            _fileList = new ArrayList();
            StreamReader FileListReader = new StreamReader(filePath, true);

            while (!FileListReader.EndOfStream)
            {
                string EntryPath = FileListReader.ReadLine();

                // Make sure the filepath is fully qualified
                if (!EntryPath.StartsWith(Path.GetPathRoot(EntryPath)))
                {
                    if (FileListReader != null)
                        FileListReader.Close();

                    throw new ApplicationException("The following file referenced in your filelist is not fully qualified: " + EntryPath);
                }

                // Make sure this file exists...
                if (!File.Exists(EntryPath))
                {
                    if (FileListReader != null)
                        FileListReader.Close();

                    throw new ApplicationException("The following file referenced in your filelist does not exist: " + EntryPath);
                }

                // Make sure this file exists on an NTFS volume..
                if (!NTFSDrives.Contains(Path.GetPathRoot(EntryPath)))
                {
                    if (FileListReader != null)
                        FileListReader.Close();

                    throw new ApplicationException("The following file referenced in your filelist is not on an NTFS volume: " + EntryPath);
                }

                _fileList.Add(EntryPath);
            }

            if (FileListReader != null)
                FileListReader.Close();

            if (_fileList.Count == 0)
                throw new ApplicationException("No files were loaded from the specified file list!");
        }

        /// <summary>
        /// Encodes the file specified in <paramref name="filePath"/> as ACL entries on files listed in the FileList
        /// </summary>
        /// <param name="filePath">The path of the file to read and encode as ACL entries</param>
        public void Encode(string filePath)
        {
            if (_fileList == null)
                throw new ApplicationException("No FileList loaded.");

            // 68-bytes per S-ID
            // 1st byte is always 1 (revision)
            // 2nd byte is a length of sub-identifiers (maximum 15 subidentifiers)
            // 3rd-8th byte (6 bytes) is the identifier authority. This is set to 'unused' or 000000000004.
            // After that there are 15x4byte chunks (each sub-identifier as counted in the 2nd byte)
            // 
            // This has a maximum S-ID size of 1 + 1 + 6 + 4x15 = 68 bytes
            // usable size is 60 bytes

            byte[] Buffer = new byte[SID_LENGTH];

            int FileOffset = 0;
            int BytesRead = 0;
            int ChunkCount = 0;
            int ChunkIndex = 0;
            int RoundNumber = 0;
            int RawFileRights = CANARY_VALUE;

            // Open the file to be encoded for reading
            FileStream InputStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // If the file's too large to be encoded with this list, abort
            if (InputStream.Length > (MAX_CHUNK_LENGTH * _fileList.Count * MAX_ROUNDS))
            {
                InputStream.Close();
                throw new ApplicationException("The input file is larger than the file list can hold.\nUse a longer list.");
            }

            // Loop through the bytes in the file so we can encode it
            while (FileOffset < InputStream.Length && RoundNumber < MAX_ROUNDS)
            {
                // Read 60 bytes from the input file into bytes 9-68 of the Chunk array
                WipeSIDBytes(ref Buffer);
                BytesRead = InputStream.Read(Buffer, SID_HEADER_LENGTH, Buffer.Length - SID_HEADER_LENGTH);

                // The last chunk may be less than 60 bytes long.
                // Pad the remaining bytes with the length of the chunk (i.e. if the chunk is 16 bytes long, pad all remaining 44 bytes with the value 0x10 (16))
                if (BytesRead < MAX_CHUNK_LENGTH)
                {
                    for (int i = (BytesRead + SID_HEADER_LENGTH); i < Buffer.Length; i++)
                    {
                        Buffer[i] = (byte)BytesRead;
                    }
                }

                // Create a new S-ID from this chunk. The 1st and 2nd bytes should be static "0x01 0x0F"
                SecurityIdentifier ID = new SecurityIdentifier(Buffer, 0);

                string CurrentFilePath = (string)_fileList[ChunkIndex];

                // Read the current ACL Entries from the next file in our filelist
                FileSecurity Security = File.GetAccessControl(CurrentFilePath);

                // Create the FileRights necessary to identify this file AND know its round position
                RawFileRights = CANARY_VALUE + RoundNumber;
                FileSystemRights FileRights = (FileSystemRights)RawFileRights;

                // Add the SID to the file's security list
                Security.AddAccessRule(new FileSystemAccessRule(ID, FileRights, AccessControlType.Allow));

                // Update the ACL on the file
                File.SetAccessControl(CurrentFilePath, Security);

                // Keep track of where we are in the file
                FileOffset += BytesRead;

                ChunkCount += 1;

                // Increment/wrap the ListIndex. Keep track of the #wraps...
                RoundNumber = ChunkCount / _fileList.Count;
                ChunkIndex = ChunkCount % _fileList.Count;
            }

            // Close the file
            if (InputStream != null)
                InputStream.Close();
        }

        /// <summary>
        /// Decodes an ACLEncoded file into the path specified
        /// </summary>
        /// <param name="outputFilePath">The path to a file where the ACLEncoded file will be written</param>
        public void Decode(string outputFilePath)
        {
            int ChunkCount = 0;
            int RoundNumber = 0;
            int ChunkIndex = 0;

            bool LookForMoreChunks = true;

            byte[] Buffer = new byte[SID_LENGTH];

            FileStream OutputStream = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            
            // Keep looping until we don't detect any more Access Control Entries
            while (LookForMoreChunks == true)
            {
                LookForMoreChunks = false;  // Abort by default. Continue if we found one.

                string FilePath = (string)_fileList[ChunkIndex];

                // Retrieve ACL entries from the file
                FileSecurity Security = File.GetAccessControl(FilePath);

                // Loop through ACL entries to find encoded entries with the current round number
                foreach (FileSystemAccessRule Rule in Security.GetAccessRules(true, false, typeof(SecurityIdentifier)))
                {
                    // If it's an encoded entry...
                    if (IsEncodedEntry(Rule))
                    {
                        // Parse out the RoundNumber from the FileSystemRights
                        int ParsedRoundNumber = (int)(Rule.FileSystemRights ^ CANARY_RIGHTS);

                        // If it's the current Round#, read the bytes from the SID
                        if (ParsedRoundNumber == RoundNumber)
                        {
                            SecurityIdentifier Identifier = new SecurityIdentifier(Rule.IdentityReference.Value);
                            Buffer.Initialize();
                            Identifier.GetBinaryForm(Buffer, 0);

                            OutputStream.Write(Buffer, 8, SID_LENGTH - 8);

                            // We found our entry. Continue to the next file/chunk
                            ChunkCount += 1;
                            LookForMoreChunks = true;
                            break;
                        }
                    }
                }

                // If we didn't find a chunk
                if (LookForMoreChunks == false)
                {
                    // ...and we've found chunks before
                    if (ChunkCount >= 1)
                    {
                        // The chunk from the last ACL entry was the last one.
                        // We need to find the true length of the last chunk and trim it down

                        int ChunkLength = MAX_CHUNK_LENGTH;
                        byte LastValue = Buffer[Buffer.Length - 1];

                        if (Buffer[LastValue + SID_HEADER_LENGTH] == LastValue)
                        {
                            ChunkLength = LastValue;

                            // Trim the file to the true chunk length
                            long NewFileLength = OutputStream.Length - (MAX_CHUNK_LENGTH - ChunkLength);
                            OutputStream.SetLength(NewFileLength);
                        }
                    }
                    else
                    {
                        // We didn't find a chunk, and we haven't found any before
                        if (OutputStream != null)
                            OutputStream.Close();
                        throw new ApplicationException("No ACLEncoded entries were found on any of the targeted files.");
                    }
                }

                RoundNumber = ChunkCount / _fileList.Count;
                ChunkIndex = ChunkCount % _fileList.Count;

            }

            // Close the file
            if (OutputStream != null)
                OutputStream.Close();
        }

        /// <summary>
        /// Wipes all ACL entries from the files listed that appear to be chunks of ACLEncoded files
        /// </summary>
        public void RemoveEncodedFile()
        {
            // Loop through all files in the filelist
            foreach (string FilePath in _fileList)
            {
                // Retrieve ACL entries from the file
                FileSecurity Security = File.GetAccessControl(FilePath);

                // Loop through ACL entries
                foreach (FileSystemAccessRule Rule in Security.GetAccessRules(true, false, typeof(SecurityIdentifier)))
                {
                    // If the entry has the canary value, remove it
                    if (IsEncodedEntry(Rule))
                    {
                        Security.RemoveAccessRule(Rule);
                    }
                }
                // Write ACL entries back to the file
                File.SetAccessControl(FilePath, Security);
            }
        }

        /// <summary>
        /// Identifies whether or not a particular FileSystemAccessRule represents a piece of an ACLEncoded file
        /// </summary>
        /// <param name="rule">An ACL rule to test</param>
        /// <returns><value>true</value> if the ACL rule is an ACLEncoded file. <value>false</value> if it isn't.</returns>
        private bool IsEncodedEntry(FileSystemAccessRule rule)
        {
            FileSystemRights Rights = rule.FileSystemRights;
            AccessControlType ACType = rule.AccessControlType;

            return (((rule.FileSystemRights & CANARY_RIGHTS) == CANARY_RIGHTS) && (rule.AccessControlType == AccessControlType.Allow));
        }

        /// <summary>
        /// Zeroes out the specified byte array and adds the SID header
        /// </summary>
        /// <param name="sid">A reference to a byte array that represents an SID</param>
        private void WipeSIDBytes(ref byte[] sid)
        {
            sid.Initialize();
            sid[0] = 0x01;    // Revision Byte
            sid[1] = 0x0F;    // #Sub-Identifiers (Maximum 15)

            sid[2] = 0x00;    // These six bytes
            sid[3] = 0x00;    // are the Identifier Authority
            sid[4] = 0x00;    // 
            sid[5] = 0x00;    // The value "00 00 00 00 00 04"
            sid[6] = 0x00;    // means "Not used"
            sid[7] = 0x04;    //
        }
    }
}
