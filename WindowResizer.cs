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

    // Method to shrink the window by a specified amount
    public static void ShrinkWindowBy(IntPtr windowHandle, int shrinkAmount)
    {
        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOZORDER = 0x0004;

        GetWindowRect(windowHandle, out RECT rect);
        int newWidth = rect.Right - rect.Left - shrinkAmount;
        int newHeight = rect.Bottom - rect.Top - shrinkAmount;

        SetWindowPos(windowHandle, IntPtr.Zero, 0, 0, newWidth, newHeight, SWP_NOMOVE | SWP_NOZORDER);
    }
}
