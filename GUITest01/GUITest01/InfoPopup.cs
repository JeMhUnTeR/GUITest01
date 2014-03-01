using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace GUITest01
{
    public partial class InfoPopup : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public InfoPopup()
        {
            InitializeComponent();
            label11.Text = new FileInfo(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName).Length + " bytes";
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://forums.fuwanovel.org/index.php?/topic/3659-downloadpre-patched-koiken-otome-prologue-partial-patch/");
        }

        private void label21_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.flyingpantsu.net/");
        }
    }
}
