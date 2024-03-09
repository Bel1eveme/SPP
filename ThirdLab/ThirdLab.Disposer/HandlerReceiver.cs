using System.Runtime.InteropServices;

namespace ThirdLab.Disposer;

public class HandlerReceiver
{
    public bool ErrorOccured { private set; get; } = false;
    
    public string ErrorMessage { private set; get; } = string.Empty;
    
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern nint CreateFile(string lpFileName, uint dwDesiredAccess, 
        uint dwShareMode, IntPtr lpSecurityAttributes,
        uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

    public nint OpenFile(string filePath)
    {
        ErrorOccured = false;
        ErrorMessage = string.Empty;
        
        const uint genericRead = 0x80000000;
        const uint openExisting = 3;
        const uint fileAttributeNormal = 0x80;
        const uint fileFlagBackupSemantics = 0x02000000;
        
        nint fileHandle = CreateFile(filePath, genericRead, 0, nint.Zero, openExisting, 
            fileAttributeNormal | fileFlagBackupSemantics, nint.Zero);
        
        if (fileHandle != nint.Zero && fileHandle.ToInt32() != -1)
        {
            return fileHandle;
        }

        ErrorOccured = true;
        ErrorMessage = "Failed to open file. Error code: " + Marshal.GetLastWin32Error();
        
        return nint.Zero;
    }
}