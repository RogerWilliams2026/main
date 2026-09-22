using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RogStock2026.Screens;

namespace RogStock2026
{
    public partial class frmLogin : Form
    {
        /*
           Modified 03/03/2026 By Roger Williams

           for some bizarre reason this form wont receive mouse messages (?!)
           so manually implemented titlebar NCHITTEST

           
           Created 12/02/2025 By Roger Williams

           Login screen!

         */

        private bool blnDragging = false;
        private Point pntLastLocation;

        public frmLogin()
        {
            InitializeComponent();
        }
        //other
        private void LoginUser()
        {
            /*
               Created 17/02/2025 By Roger Williams

               logins user if valid and creates record in login_current

             */

            frmMain frmTemp;

            if (Modules.clsData.CheckLogin(this.TXTUser.Text, this.TXTPassword.Text))
            {
                //create record in login_current
                Modules.clsData.CreateCurrentLoginRecord(this.TXTUser.Text);

                this.Hide();
                //show main form
                frmTemp = new frmMain();
                frmTemp.Show();
            }
            else
            {
                MessageBox.Show("Invalid User Name or Password", "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //form events
        private void BTNCancel_Click(object sender, EventArgs e)
        {
            /*
               Created 15/02/2025 By Roger Williams


             */
            this.Close();
            Application.Exit();
        }

        private void BTNLogin_Click(object sender, EventArgs e)
        {
            /*
               Created 15/02/2025 By Roger Williams

               Login user

             */

            LoginUser();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //load theme
            Modules.clsView.ReadThemeData();
            //apply system theme
            Modules.clsView.SetTheme(this, null);
        }

        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ////if pointer inside "title bar"
            //if (e.Y <= Modules.clsView.CNST_INT_TITLEBARHEIGHT)
            //{
            //    if (e.Button == MouseButtons.Left)
            //    {
            //        //move form
            //        Modules.clsView.User32_DLL.ReleaseCapture();
            //        Modules.clsView.User32_DLL.SendMessage(this.Handle, Modules.clsView.WM_NCLBUTTONDOWN, (IntPtr)Modules.clsView.HTCAPTION, new IntPtr(0));
            //    }
            //}
        }

        private void frmLogin_Paint(object sender, PaintEventArgs e)
        {
            //fill titlebar with PANTitle back colour
            Modules.clsView.FillTitleBar(e.Graphics, this.PANTitle.BackColor, this.PANTitle.Width, this.Width - this.PANTitle.Width, this.PANTitle.Height);
        }

        private void PANTitle_MouseDown(object sender, MouseEventArgs e)
        {
            blnDragging = true;
            pntLastLocation = e.Location;
        }

        private void PANTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (blnDragging)
            {
                this.Location = new Point(
                (this.Location.X - pntLastLocation.X) + e.X,
                (this.Location.Y - pntLastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void PANTitle_MouseUp(object sender, MouseEventArgs e)
        {
            blnDragging = false;
        }
    }
}
