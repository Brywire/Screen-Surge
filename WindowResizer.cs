using System.Runtime.InteropServices;

public class WindowResizer
{
    // Import SetWindowPos from user32.dll
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(
        IntPtr hWnd, // Handle to Window 
        IntPtr hWndInsertAfter, // Z Order
        int X, int Y, // New position of the window
        int cx, int cy, // New width and height of window, calculated by shrinkAmount
        uint uFlags // Specifies how window should be moved & resized. Uses SWP_NOMOVE & SWP_NOZORDER
        );

    // Import GetWindowRect from user32.dll
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect); // Getting current window size

    // Define the RECT structure
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    private static readonly uint SWP_NOMOVE = 0x0002;
    private static readonly uint SWP_NOZORDER = 0x0004;
    
    // Method to shrink the window by a specified amount
    public static void ShrinkWindowBy(IntPtr windowHandle, int shrinkAmount)
    {
        GetWindowRect(windowHandle, out RECT rect);
        int newWidth = rect.Right - rect.Left - shrinkAmount;
        int newHeight = rect.Bottom - rect.Top - shrinkAmount;

        SetWindowPos(windowHandle, IntPtr.Zero, 0, 0, newWidth, newHeight, SWP_NOMOVE | SWP_NOZORDER);
    }

    public static void ExpandWindowBy(IntPtr windowHandle, int expandAmount, BorderSide side)
    {
        GetWindowRect(windowHandle, out RECT rect);
        int newWidth = rect.Right - rect.Left;
        int newHeight = rect.Bottom - rect.Top;

        switch (side)
        {
            case BorderSide.Top:
                // Expand the window upwards
                SetWindowPos(windowHandle, IntPtr.Zero, rect.Left, rect.Top - expandAmount, newWidth, newHeight + expandAmount, SWP_NOZORDER);
                break;
            case BorderSide.Right:
                // Expand the window to the right
                SetWindowPos(windowHandle, IntPtr.Zero, 0, 0, newWidth + expandAmount, newHeight, SWP_NOMOVE | SWP_NOZORDER);
                break;
            case BorderSide.Bottom:
                // Expand the window downwards
                SetWindowPos(windowHandle, IntPtr.Zero, 0, 0, newWidth, newHeight + expandAmount, SWP_NOMOVE | SWP_NOZORDER);
                break;
            case BorderSide.Left:
                // Expand the window to the left
                SetWindowPos(windowHandle, IntPtr.Zero, rect.Left - expandAmount, rect.Top, newWidth + expandAmount, newHeight, SWP_NOZORDER);
                break;
            default:
                break;
        }
    }
}
