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
    public partial class GUITest : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public bool readyForPatch = false;
        private InfoPopup infoPopup = new InfoPopup();

        public GUITest()
        {
            InitializeComponent();
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text + "\\恋剣乙女.exe"))
            {
                exeLabel.ForeColor = Color.FromArgb(0,255,0);
                exeLabel.Text = "This is the game installation folder.";
                statusBarLabel.Text = "Game executable found. Patching will replace old files.";
            }
            else
            {
                exeLabel.ForeColor = Color.FromArgb(255, 0, 0);
                exeLabel.Text = "Game installation not found on this folder.";
                statusBarLabel.Text = "Game executable not found. Patching will just create new files.";
            }

            readyForPatch = (KoikenPatcher.Properties.Resources.Extract != null);
            readyLabel.Visible = true;
            startPatch.Visible = true;
            if (!readyForPatch)
            {
                readyLabel.ForeColor = Color.Red;
                readyLabel.Text = "Files not found.";
                startPatch.Text = "Quit";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void openFolder_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            statusBarLabel.Text = "Waiting for folder selection...";
            if (openFolder.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = openFolder.SelectedPath;
            }
            else
            {
                statusBarLabel.Text = "Cancelled.";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.flyingpantsu.net");
            }
            catch {}
        }

        private void button5_Click(object sender, EventArgs e)
        {
            infoPopup.Show();
            infoPopup.Activate();
        }

        private void startPatch_Click(object sender, EventArgs e)
        {
            if (readyForPatch)
            {
                Patch();
            }
            else
            {
                Application.Exit();
            }
        }

        private void Patch ()
        {
            panel5.Visible = false;
            button3.Visible = false;
            // start patch sequence
            MessageBox.Show(KoikenPatcher.Properties.Resources.Extract.ToString());
        }
    }
}