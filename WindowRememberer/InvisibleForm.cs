﻿using System;
using System.Windows.Forms;

namespace WindowRememberer
{
    public partial class InvisibleForm : Form
    {
        private const int WM_HOTKEY = 0x312;

        private HotKeyHandler hotKeyHandler;

        public InvisibleForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;

            hotKeyHandler = new HotKeyHandler(Handle);

            base.OnLoad(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                hotKeyHandler.OnHotKeyPressed(m.LParam);
            }

            base.WndProc(ref m);
        }
    }
}
