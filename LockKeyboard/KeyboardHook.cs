#pragma warning disable CA2101 // P/Invoke 文字列引数に対してマーシャリングを指定します
#pragma warning disable SYSLIB1054 // コンパイル時に P/Invoke マーシャリング コードを生成するには、'DllImportAttribute' の代わりに 'LibraryImportAttribute' を使用します

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LockKeyboard
{
    // キーボードフックの責務を持つクラス
    public static class KeyboardHook
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        private static IntPtr hookId = IntPtr.Zero;
        private static readonly LowLevelKeyboardProc proc = HookCallback;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn,
            IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public static void SetHook()
        {
            using Process curProcess = Process.GetCurrentProcess();
            using ProcessModule curModule = curProcess.MainModule!;
            hookId = SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }

        public static void Unhook()
        {
            if (hookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(hookId);
                hookId = IntPtr.Zero;
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // nCodeが0ではない場合は、次のフックプロシージャに処理を渡さなければならないらしい
            if (nCode != 0)
                return CallNextHookEx(hookId, nCode, wParam, lParam);

            return (IntPtr)1;

            // wParamには、イベントの種類（通常キーが押された、離された。ALTを押しながら通常キーが押された、離されたの4種類ある)が入っているが、
            // 本アプリではすべて無視したいので、wParamはチェックする必要がない。

            // lParamには押されたキーの情報が入っているが、これも本アプリではすべて無視したいので、lParamもチェックする必要がない。
        }
    }
}
