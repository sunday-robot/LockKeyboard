namespace LockKeyboard
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Text = "Lock Keyboard";
            FormBorderStyle = FormBorderStyle.FixedToolWindow;  // サイズ変更は不可
            StartPosition = FormStartPosition.Manual;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TopMost = true; // 常に最前面に表示

            Controls.Add(new Label
            {
                Text = "キーボードロック中\n解除するには×をクリックしてください",
                AutoSize = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            });

            FormClosing += (s, e) =>
            {
                // 終了時にフック解除
                KeyboardHook.Unhook();
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 起動時にフック
            KeyboardHook.SetHook();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            var wa = Screen.PrimaryScreen!.WorkingArea;
            Location = new Point(wa.Right - Bounds.Width, wa.Bottom - Bounds.Height);
            // ↑WinFormsのバグで、Boundsには正しいサイズが入らないことがある。
            // このため、右下にぴったり貼り付くように配置されないことがある。
            // 少なくとも4kモニターで拡大率を170%に設定したモニターでは、実際よりも大きな幅、高さが設定されるので、
            // ウィンドウの位置が少し右下隅よりも離れた位置になってしまう。
        }
    }
}
