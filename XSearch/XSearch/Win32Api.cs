using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPFTest2
{
    public class Win32Api
    {

        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_SYSKEYUP = 0x105;
        public const int WH_KEYBOARD_LL = 13;

        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCood, Int32 wParam, IntPtr lParam);
        [DllImport("user32")]
        public static extern int ToAscii(int vVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpbTranKey, int fuState);
        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public extern static IntPtr GetModuleHanlde(string lpModuleName);

        public class KeyboardHook
        {
            int hHook;
            Win32Api.HookProc KeyboardHookDetegate;
            public event KeyEventHandler OnKeyDownEvent;
            public event KeyEventHandler OnKeyUpEvent;
            public event KeyPressEventHandler OnKeyPressEvent;
            public KeyboardHook() { }

            public void SetHook()
            {
                KeyboardHookDetegate = new HookProc(KeyboardHookProc);
                Process cProcess = Process.GetCurrentProcess();
                ProcessModule cModule = cProcess.MainModule;
                var mh = Win32Api.GetModuleHanlde(cModule.ModuleName);
                hHook = Win32Api.SetWindowsHookEx(Win32Api.WH_KEYBOARD_LL, KeyboardHookDetegate, mh, 0);
            }

            public void UnHook()
            {
                Win32Api.UnhookWindowsHookEx(hHook);
            }
            private List<Keys> preKeysList = new List<Keys>();
            private int KeyboardHookProc(int nCode, int wParam, IntPtr lParam)
            {
                if ((nCode >= 0) && (OnKeyDownEvent != null || OnKeyUpEvent != null || OnKeyPressEvent != null))
                {
                    Win32Api.KeyboardHookStruct KeyDataFromHook = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                    Keys keyData = (Keys)KeyDataFromHook.vkCode;
                    if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == Win32Api.WM_KEYDOWN || wParam == Win32Api.WM_SYSKEYDOWN))
                    {
                        if (IsCtrlAltShiftKeys(keyData) && preKeysList.IndexOf(keyData) == -1)
                        {
                            preKeysList.Add(keyData);
                        }
                    }
                    //WM_KEYDOWN和WM_SYSKEYDOWN消息，将会引发OnKeyDownEvent事件
                    if (OnKeyDownEvent != null && (wParam == Win32Api.WM_KEYDOWN || wParam == Win32Api.WM_SYSKEYDOWN))
                    {
                        KeyEventArgs e = new KeyEventArgs(GetDownKeys(keyData));

                        OnKeyDownEvent(this, e);
                    }
                    //WM_KEYDOWN消息将引发OnKeyPressEvent
                    if (OnKeyPressEvent != null && wParam == Win32Api.WM_KEYDOWN)
                    {
                        byte[] keyState = new byte[256];
                        Win32Api.GetKeyboardState(keyState);
                        byte[] inBuffer = new byte[2];
                        if (Win32Api.ToAscii(KeyDataFromHook.vkCode, KeyDataFromHook.scanCode, keyState, inBuffer, KeyDataFromHook.flags) == 1)
                        {
                            KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
                            OnKeyPressEvent(this, e);
                        }
                    }
                    //松开控制键
                    if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == Win32Api.WM_KEYUP || wParam == Win32Api.WM_SYSKEYUP))
                    {
                        if (IsCtrlAltShiftKeys(keyData))
                        {
                            for (int i = preKeysList.Count - 1; i >= 0; i--)
                            {
                                if (preKeysList[i] == keyData) { preKeysList.RemoveAt(i); }
                            }
                        }
                    }
                    //WM_KEYUP和WM_SYSKEYUP消息，将引发OnKeyUpEvent事件
                    if (OnKeyUpEvent != null && (wParam == Win32Api.WM_KEYUP || wParam == Win32Api.WM_SYSKEYUP))
                    {
                        KeyEventArgs e = new KeyEventArgs(GetDownKeys(keyData));
                        OnKeyUpEvent(this, e);
                    }
                }
                return Win32Api.CallNextHookEx(hHook, nCode, wParam, lParam);

            }

            private Keys GetDownKeys(Keys key)
            {
                Keys rtnKey = Keys.None;
                foreach (Keys i in preKeysList)
                {
                    if (i == Keys.LControlKey || i == Keys.RControlKey) { rtnKey = rtnKey | Keys.Control; }
                    if (i == Keys.LMenu || i == Keys.RMenu) { rtnKey = rtnKey | Keys.Alt; }
                    if (i == Keys.LShiftKey || i == Keys.RShiftKey) { rtnKey = rtnKey | Keys.Shift; }
                }
                return rtnKey | key;

            }

            private bool IsCtrlAltShiftKeys(Keys key)
            {
                if (key == Keys.LControlKey || key == Keys.RControlKey || key == Keys.LMenu || key == Keys.RMenu || key == Keys.LShiftKey || key == Keys.RShiftKey) { return true; }
                return false;

            }
        }
    }
}
