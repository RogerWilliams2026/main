using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;


namespace RogStock2026_Utilities.Screens
{
    public partial class frmLogin_Utilities : Form
    {
        /*
           Created 12/02/2026 By Roger Williams

           Login screen!

         */

        frmMain frmTemp;

        //for manual mouse move of form
        bool blnDragging = false;
        Point pntLastLocation;

        public frmLogin_Utilities()
        {
            InitializeComponent();
        }
        //other
        private void LoginUser()
        {
            /*
               Created 17/02/2026 By Roger Williams

               logins user if valid and creates record in login_current

             */

            if (Modules.clsData_Utilities.CheckLogin(this.TXTUser.Text, this.TXTPassword.Text))
            {
                //create record in login_current
                //    Modules.clsData_Utilities.CreateCurrentLoginRecord(this.TXTUser.Text);

                //     if (Modules.clsData_Utilities.IsUserIngroup(this.TXTUser.Text,"admin"))
                //     { 
                  this.Close();
                //     }
                //hide
                //this.Hide();
                ////open main menu form
                //frmTemp = new frmMain();
                //frmTemp.Show();
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
               Created 15/02/2026 By Roger Williams


             */
            this.Close();
            Application.Exit();
        }

        private void BTNLogin_Click(object sender, EventArgs e)
        {
            /*
               Created 15/02/2026 By Roger Williams

               Login user

             */
            LoginUser();
        }

        private void frmLogin_Utilities_Load(object sender, EventArgs e)
        {
            //load theme
            Modules.clsView_Utilities.ReadThemeData();
            //apply system theme
            Modules.clsView_Utilities.SetTheme(this, null);
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