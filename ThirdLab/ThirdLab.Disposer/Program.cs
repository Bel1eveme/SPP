using System.Runtime.InteropServices;
using ThirdLab.Disposer;

HandlerReceiver handleReceiver = new HandlerReceiver(); 

nint handle = handleReceiver.OpenFile("D:\\q\\t.txt");

if (handleReceiver.ErrorOccured)
{
    Console.WriteLine(handleReceiver.ErrorMessage);
    return;
}

using var handleWrapper = new HandleWrapper(handle);

Console.WriteLine($"Used handle {handleWrapper.DangerousGetHandle()}");