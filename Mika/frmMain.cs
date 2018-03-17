using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;
using System.Runtime.InteropServices;
namespace Mika
{
    public partial class frmMain : Form
    {

        #region WindowAPI
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        private static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        #endregion
        public frmMain()
        {
            InitializeComponent();
        }

        private Size GetControlSize(IntPtr hWnd)
        {
            RECT pRect;
            Size cSize = new Size();
            // get coordinates relative to window
            GetWindowRect(hWnd, out pRect);

            cSize.Width = pRect.Right - pRect.Left;
            cSize.Height = pRect.Bottom - pRect.Top;

            return cSize;
        }

        private void GetList()
        {
            string[] defBlist = { "devenv", "ShellExperienceHost" };
            lvListW.Items.Clear();
            Process[] processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                var proc = process.ProcessName;
                var hwnd = process.MainWindowHandle;
                var winn = process.MainWindowTitle;
                if (hwnd != new IntPtr(0) && winn != string.Empty && hwnd != this.Handle)
                    if (!defBlist.Contains(proc))
                        lvListW.Items.Add(new ListViewItem(new string[] { winn, hwnd.ToString() }));
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            GetList();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            Rectangle res = Screen.PrimaryScreen.Bounds;
            foreach (ListViewItem item in lvListW.Items)
            {
                IntPtr hwnd = new IntPtr(int.Parse(item.SubItems[1].Text));
                SetWindowPos(hwnd, -2, (res.Width / 2) - (GetControlSize(hwnd).Width / 2), (res.Height / 2) - (GetControlSize(hwnd).Height / 2), 0, 0, 0x1 | 0x10);
            }
            GetList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetList();
        }

        private void lvListW_DoubleClick(object sender, EventArgs e)
        {
            Rectangle res = Screen.PrimaryScreen.Bounds;
            foreach (ListViewItem item in lvListW.Items)
            {
                if (item.Selected)
                {
                    IntPtr hwnd = new IntPtr(int.Parse(item.SubItems[1].Text));
                    SetWindowPos(hwnd, -2, (res.Width / 2) - (GetControlSize(hwnd).Width / 2), (res.Height / 2) - (GetControlSize(hwnd).Height / 2), 0, 0, 0x1 | 0x10);
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Rectangle res = Screen.PrimaryScreen.Bounds;
            foreach (ListViewItem item in lvListW.Items)
            {
                if (item.Selected)
                {
                    IntPtr hwnd = new IntPtr(int.Parse(item.SubItems[1].Text));
                    SetWindowPos(hwnd, -2, (res.Width / 2) - (GetControlSize(hwnd).Width / 2), (res.Height / 2) - (GetControlSize(hwnd).Height / 2), 0, 0, 0x1 | 0x10);
                }
            }
        }
    }
}
