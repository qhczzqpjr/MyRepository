using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Diagnostics;


    class WindowResizer
    {
        private const int WmSyscommand = 0x112;
        private HwndSource _hwndSource;
        Window _activeWin;

        public WindowResizer(Window activeW)
        {
            _activeWin = activeW as Window;

            _activeWin.SourceInitialized += new EventHandler(InitializeWindowSource);
        }


        public void ResetCursor()
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                _activeWin.Cursor = Cursors.Arrow;
            }
        }

        public void DragWindow()
        {
            _activeWin.DragMove();
        }

        private void InitializeWindowSource(object sender, EventArgs e)
        {
            _hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            _hwndSource.AddHook(new HwndSourceHook(WndProc));
        }

        IntPtr _retInt = IntPtr.Zero;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            Debug.WriteLine("WndProc messages: " + msg.ToString());
            //
            // Check incoming window system messages
            //
            if (msg == WmSyscommand)
            {
                Debug.WriteLine("WndProc messages: " + msg.ToString());
            }

            return IntPtr.Zero;
        }



        public enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);


        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_hwndSource.Handle, WmSyscommand, (IntPtr)(61440 + direction), IntPtr.Zero);
        }


        public void resizeWindow(object sender)
        {
            Rectangle clickedRectangle = sender as Rectangle;

            switch (clickedRectangle.Name)
            {
                case "top":
                    _activeWin.Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Top);
                    break;
                case "bottom":
                    _activeWin.Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Bottom);
                    break;
                case "left":
                    _activeWin.Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Left);
                    break;
                case "right":
                    _activeWin.Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Right);
                    break;
                case "topLeft":
                    _activeWin.Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.TopLeft);
                    break;
                case "topRight":
                    _activeWin.Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.TopRight);
                    break;
                case "bottomLeft":
                    _activeWin.Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.BottomLeft);
                    break;
                case "bottomRight":
                    _activeWin.Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.BottomRight);
                    break;
                default:
                    break;
            }
        }


        public void DisplayResizeCursor(object sender)
        {

            Rectangle clickedRectangle = sender as Rectangle;

            switch (clickedRectangle.Name)
            {
                case "top":
                    _activeWin.Cursor = Cursors.SizeNS;
                    break;
                case "bottom":
                    _activeWin.Cursor = Cursors.SizeNS;
                    break;
                case "left":
                    _activeWin.Cursor = Cursors.SizeWE;
                    break;
                case "right":
                    _activeWin.Cursor = Cursors.SizeWE;
                    break;
                case "topLeft":
                    _activeWin.Cursor = Cursors.SizeNWSE;
                    break;
                case "topRight":
                    _activeWin.Cursor = Cursors.SizeNESW;
                    break;
                case "bottomLeft":
                    _activeWin.Cursor = Cursors.SizeNESW;
                    break;
                case "bottomRight":
                    _activeWin.Cursor = Cursors.SizeNWSE;
                    break;
                default:
                    break;
            }
        }

    }

