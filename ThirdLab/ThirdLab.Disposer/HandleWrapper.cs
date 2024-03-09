using System.Runtime.InteropServices;

namespace ThirdLab.Disposer;

public class HandleWrapper : SafeHandle
{
    public HandleWrapper(nint handle) : base(nint.Zero, true)
    {
        this.handle = handle;
    }
    
    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(nint hObject);

    protected override bool ReleaseHandle()
    {
        return CloseHandle(handle);
    }

    public override bool IsInvalid => handle == nint.Zero;
}