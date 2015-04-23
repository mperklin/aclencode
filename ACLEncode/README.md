#ACLEncode Readme

###Written by Michael Perklin

1. Introduction
2. How It Works / Limitations
3. Usage
4. Warnings
5. Thanks
6. References

---------------------------------------------------------------------------
##Introduction
ACLEncode is a tool that allows you to encode files as Access Control Entries (ACEs) attached to a files. ACEs exist in the Access Control List (ACL) of a file on an NTFS filesystem. This list of entries tells Windows which users and groups have permission to perform various acts on a file such as reading,
writing, executing, etc.

ACLEncode takes a file and encodes it as multiple ACEs and spreads them out across many files. ACLEncode simply chops up a file and writes the bytes as-is into ACEs without performing any data scrambling or encrypting operations.

---------------------------------------------------------------------------
##How it works / Limitations
ACLEncode encodes files as Security Identifiers (S-IDs) within ACEs. The maximum length for each S-ID is 68 bytes, however 8 bytes are used for other purposes. As a result, ACLEncode can only encode 60 bytes of a file
within each ACE.

ACLEncode splits up your input file into 60-byte chunks and writes each chunk into ACEs that are placed onto a list of files that you choose.

Each file's Access Control List can only hold a maximum of 64kB. As a result, the sum of all Access Control Entries must be less than 64kB.

Using the maximum size for each of the components of an ACE, this brings the total to about 860 entries per file, provided there are NO OTHER ACEs present for legitimate purposes.

ACLEncode is designed to limit its use to 512 entries per file to acommodate legitimate ACEs.

---------------------------------------------------------------------------
##Usage
ACLEncode needs to know where to write all of the ACEs. You must provide a text file with a list of fully-qualified paths to files that exist on an NTFS volume.

You can create a file list quickly using the "Create Filelist" button.

Once you have a list of files you can choose a TARGET file to encode, then click Encode. It's as simple as that.

---------------------------------------------------------------------------
##Warnings
Be aware that when NTFS is being queried for a new S-ID it has never heard of, it will store that S-ID for future use to speed up future permission checks. This means each chunk of your file will be cached for the future.

NTFS has *NO* ability to prune this list of cached S-IDs by design. This means even after you've removed ACEs from a file's ACL, the S-ID file chunk will stay behind forever.

### THIS PROGRAM WILL PERMANENTLY EAT YOUR NTFS DISK SPACE!
If you encode a 100MB file now and remove it from your file's ACLs, that 100MB will never be reclaimed.

--------------------------------------------------------------------------
##Thanks
Thanks to everyone who helped me test and discuss different strategies for ACLEncode... Mostly Josh, Nick, Jole, and Reesh.

Also thanks to Eugene for seeding the curiosity in my mind of how to hide something on a volume in a way that's difficult to detect.

Thanks to family for living with me always being busy, figuring out ways to break NTFS and then rebuilding my VMs.

--------------------------------------------------------------------------
##References
The following URLs were helpful in my research to understand various
limitations of NTFS and its security structures. The site ntfs.com was
the most helpful by far.

http://msdn.microsoft.com/en-us/library/gg465313.aspx
http://stackoverflow.com/questions/1140528/what-is-the-maximum-length-of-a-sid-in-sddl-format
http://technet.microsoft.com/en-us/library/cc962011.aspx
http://msdn.microsoft.com/en-CA/library/ms229078(v=vs.85).aspx
https://github.com/mosa/Mono-Class-Libraries/blob/master/mcs/class/corlib/System.Security.AccessControl/FileSystemRights.cs
http://msdn.microsoft.com/en-us/library/system.security.accesscontrol.filesystemrights.aspx
http://www.ntfs.com/ntfs-permissions-access-entries.htm
http://www.ntfs.com/ntfs-permissions-security-descriptor.htm
http://support.microsoft.com/kb/279682