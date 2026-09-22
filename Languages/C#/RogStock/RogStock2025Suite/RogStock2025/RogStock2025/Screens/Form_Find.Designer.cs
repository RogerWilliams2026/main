using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
{
    partial class frmFind
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BTNClose = new System.Windows.Forms.Button();
            this.BTNFind = new System.Windows.Forms.Button();
            this.DGVResults = new System.Windows.Forms.DataGridView();
            this.PANField6 = new System.Windows.Forms.Panel();
            this.DTPField6To = new System.Windows.Forms.DateTimePicker();
            this.DTPField6From = new System.Windows.Forms.DateTimePicker();
            this.NUDField6 = new System.Windows.Forms.NumericUpDown();
            this.TXTField6 = new System.Windows.Forms.TextBox();
            this.LBLField6 = new System.Windows.Forms.Label();
            this.CHKField6 = new System.Windows.Forms.CheckBox();
            this.CMBField6Comparison = new System.Windows.Forms.ComboBox();
            this.PANField5 = new System.Windows.Forms.Panel();
            this.DTPField5To = new System.Windows.Forms.DateTimePicker();
            this.DTPField5From = new System.Windows.Forms.DateTimePicker();
            this.NUDField5 = new System.Windows.Forms.NumericUpDown();
            this.TXTField5 = new System.Windows.Forms.TextBox();
            this.LBLField5 = new System.Windows.Forms.Label();
            this.CHKField5 = new System.Windows.Forms.CheckBox();
            this.CMBField5Comparison = new System.Windows.Forms.ComboBox();
            this.PANField4 = new System.Windows.Forms.Panel();
            this.DTPField4To = new System.Windows.Forms.DateTimePicker();
            this.DTPField4From = new System.Windows.Forms.DateTimePicker();
            this.NUDField4 = new System.Windows.Forms.NumericUpDown();
            this.TXTField4 = new System.Windows.Forms.TextBox();
            this.LBLField4 = new System.Windows.Forms.Label();
            this.CHKField4 = new System.Windows.Forms.CheckBox();
            this.CMBField4Comparison = new System.Windows.Forms.ComboBox();
            this.PANField2 = new System.Windows.Forms.Panel();
            this.DTPField2To = new System.Windows.Forms.DateTimePicker();
            this.DTPField2From = new System.Windows.Forms.DateTimePicker();
            this.NUDField2 = new System.Windows.Forms.NumericUpDown();
            this.TXTField2 = new System.Windows.Forms.TextBox();
            this.LBLField2 = new System.Windows.Forms.Label();
            this.CHKField2 = new System.Windows.Forms.CheckBox();
            this.CMBField2Comparison = new System.Windows.Forms.ComboBox();
            this.PANField3 = new System.Windows.Forms.Panel();
            this.DTPField3o = new System.Windows.Forms.DateTimePicker();
            this.DTPField3From = new System.Windows.Forms.DateTimePicker();
            this.NUDField3 = new System.Windows.Forms.NumericUpDown();
            this.TXTField3 = new System.Windows.Forms.TextBox();
            this.LBLField3 = new System.Windows.Forms.Label();
            this.CHKField3 = new System.Windows.Forms.CheckBox();
            this.CMBField3Comparison = new System.Windows.Forms.ComboBox();
            this.PANField1 = new System.Windows.Forms.Panel();
            this.DTPField1To = new System.Windows.Forms.DateTimePicker();
            this.DTPField1From = new System.Windows.Forms.DateTimePicker();
            this.NUDField1 = new System.Windows.Forms.NumericUpDown();
            this.TXTField1 = new System.Windows.Forms.TextBox();
            this.LBLField1 = new System.Windows.Forms.Label();
            this.CHKField1 = new System.Windows.Forms.CheckBox();
            this.CMBField1Comparison = new System.Windows.Forms.ComboBox();
            this.CHKPanel6 = new System.Windows.Forms.CheckBox();
            this.CHKPanel5 = new System.Windows.Forms.CheckBox();
            this.CHKPanel4 = new System.Windows.Forms.CheckBox();
            this.CHKPanel3 = new System.Windows.Forms.CheckBox();
            this.CHKPanel2 = new System.Windows.Forms.CheckBox();
            this.CHKPanel1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGVResults)).BeginInit();
            this.PANField6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDField6)).BeginInit();
            this.PANField5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDField5)).BeginInit();
            this.PANField4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDField4)).BeginInit();
            this.PANField2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDField2)).BeginInit();
            this.PANField3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDField3)).BeginInit();
            this.PANField1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDField1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTNClose
            // 
            this.BTNClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTNClose.Location = new System.Drawing.Point(568, 52);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(75, 23);
            this.BTNClose.TabIndex = 11;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // BTNFind
            // 
            this.BTNFind.Location = new System.Drawing.Point(568, 10);
            this.BTNFind.Name = "BTNFind";
            this.BTNFind.Size = new System.Drawing.Size(75, 23);
            this.BTNFind.TabIndex = 12;
            this.BTNFind.Text = "Find";
            this.BTNFind.UseVisualStyleBackColor = true;
            this.BTNFind.Click += new System.EventHandler(this.BTNFind_Click);
            // 
            // DGVResults
            // 
            this.DGVResults.AllowUserToAddRows = false;
            this.DGVResults.AllowUserToDeleteRows = false;
            this.DGVResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.DGVResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DGVResults.Enabled = false;
            this.DGVResults.Location = new System.Drawing.Point(5, 288);
            this.DGVResults.Name = "DGVResults";
            this.DGVResults.ReadOnly = true;
            this.DGVResults.ShowEditingIcon = false;
            this.DGVResults.ShowRowErrors = false;
            this.DGVResults.Size = new System.Drawing.Size(643, 255);
            this.DGVResults.TabIndex = 14;
            this.DGVResults.DoubleClick += new System.EventHandler(this.DGVResults_DoubleClick);
            // 
            // PANField6
            // 
            this.PANField6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANField6.Controls.Add(this.DTPField6To);
            this.PANField6.Controls.Add(this.DTPField6From);
            this.PANField6.Controls.Add(this.NUDField6);
            this.PANField6.Controls.Add(this.TXTField6);
            this.PANField6.Controls.Add(this.LBLField6);
            this.PANField6.Controls.Add(this.CHKField6);
            this.PANField6.Controls.Add(this.CMBField6Comparison);
            this.PANField6.Enabled = false;
            this.PANField6.Location = new System.Drawing.Point(45, 197);
            this.PANField6.Name = "PANField6";
            this.PANField6.Size = new System.Drawing.Size(386, 31);
            this.PANField6.TabIndex = 26;
            // 
            // DTPField6To
            // 
            this.DTPField6To.Location = new System.Drawing.Point(147, 0);
            this.DTPField6To.Name = "DTPField6To";
            this.DTPField6To.Size = new System.Drawing.Size(41, 20);
            this.DTPField6To.TabIndex = 12;
            this.DTPField6To.Visible = false;
            // 
            // DTPField6From
            // 
            this.DTPField6From.Location = new System.Drawing.Point(332, 2);
            this.DTPField6From.Name = "DTPField6From";
            this.DTPField6From.Size = new System.Drawing.Size(41, 20);
            this.DTPField6From.TabIndex = 11;
            this.DTPField6From.Visible = false;
            // 
            // NUDField6
            // 
            this.NUDField6.Location = new System.Drawing.Point(232, 5);
            this.NUDField6.Name = "NUDField6";
            this.NUDField6.Size = new System.Drawing.Size(62, 20);
            this.NUDField6.TabIndex = 10;
            this.NUDField6.Visible = false;
            // 
            // TXTField6
            // 
            this.TXTField6.Location = new System.Drawing.Point(229, 4);
            this.TXTField6.Name = "TXTField6";
            this.TXTField6.Size = new System.Drawing.Size(91, 20);
            this.TXTField6.TabIndex = 9;
            this.TXTField6.Visible = false;
            // 
            // LBLField6
            // 
            this.LBLField6.AutoSize = true;
            this.LBLField6.Location = new System.Drawing.Point(2, 7);
            this.LBLField6.Name = "LBLField6";
            this.LBLField6.Size = new System.Drawing.Size(0, 13);
            this.LBLField6.TabIndex = 7;
            // 
            // CHKField6
            // 
            this.CHKField6.AutoSize = true;
            this.CHKField6.Location = new System.Drawing.Point(87, 9);
            this.CHKField6.Name = "CHKField6";
            this.CHKField6.Size = new System.Drawing.Size(15, 14);
            this.CHKField6.TabIndex = 6;
            this.CHKField6.UseVisualStyleBackColor = true;
            this.CHKField6.Visible = false;
            // 
            // CMBField6Comparison
            // 
            this.CMBField6Comparison.FormattingEnabled = true;
            this.CMBField6Comparison.Items.AddRange(new object[] {
            "=",
            "<",
            ">",
            "<>",
            "LIKE"});
            this.CMBField6Comparison.Location = new System.Drawing.Point(179, 3);
            this.CMBField6Comparison.Name = "CMBField6Comparison";
            this.CMBField6Comparison.Size = new System.Drawing.Size(42, 21);
            this.CMBField6Comparison.TabIndex = 2;
            // 
            // PANField5
            // 
            this.PANField5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANField5.Controls.Add(this.DTPField5To);
            this.PANField5.Controls.Add(this.DTPField5From);
            this.PANField5.Controls.Add(this.NUDField5);
            this.PANField5.Controls.Add(this.TXTField5);
            this.PANField5.Controls.Add(this.LBLField5);
            this.PANField5.Controls.Add(this.CHKField5);
            this.PANField5.Controls.Add(this.CMBField5Comparison);
            this.PANField5.Enabled = false;
            this.PANField5.Location = new System.Drawing.Point(45, 161);
            this.PANField5.Name = "PANField5";
            this.PANField5.Size = new System.Drawing.Size(386, 31);
            this.PANField5.TabIndex = 25;
            // 
            // DTPField5To
            // 
            this.DTPField5To.Location = new System.Drawing.Point(134, 4);
            this.DTPField5To.Name = "DTPField5To";
            this.DTPField5To.Size = new System.Drawing.Size(41, 20);
            this.DTPField5To.TabIndex = 12;
            this.DTPField5To.Visible = false;
            // 
            // DTPField5From
            // 
            this.DTPField5From.Location = new System.Drawing.Point(332, 1);
            this.DTPField5From.Name = "DTPField5From";
            this.DTPField5From.Size = new System.Drawing.Size(41, 20);
            this.DTPField5From.TabIndex = 11;
            this.DTPField5From.Visible = false;
            // 
            // NUDField5
            // 
            this.NUDField5.Location = new System.Drawing.Point(232, 4);
            this.NUDField5.Name = "NUDField5";
            this.NUDField5.Size = new System.Drawing.Size(62, 20);
            this.NUDField5.TabIndex = 10;
            this.NUDField5.Visible = false;
            // 
            // TXTField5
            // 
            this.TXTField5.Location = new System.Drawing.Point(229, 3);
            this.TXTField5.Name = "TXTField5";
            this.TXTField5.Size = new System.Drawing.Size(91, 20);
            this.TXTField5.TabIndex = 9;
            this.TXTField5.Visible = false;
            // 
            // LBLField5
            // 
            this.LBLField5.AutoSize = true;
            this.LBLField5.Location = new System.Drawing.Point(3, 7);
            this.LBLField5.Name = "LBLField5";
            this.LBLField5.Size = new System.Drawing.Size(0, 13);
            this.LBLField5.TabIndex = 7;
            // 
            // CHKField5
            // 
            this.CHKField5.AutoSize = true;
            this.CHKField5.Location = new System.Drawing.Point(88, 9);
            this.CHKField5.Name = "CHKField5";
            this.CHKField5.Size = new System.Drawing.Size(15, 14);
            this.CHKField5.TabIndex = 6;
            this.CHKField5.UseVisualStyleBackColor = true;
            this.CHKField5.Visible = false;
            // 
            // CMBField5Comparison
            // 
            this.CMBField5Comparison.FormattingEnabled = true;
            this.CMBField5Comparison.Items.AddRange(new object[] {
            "=",
            "<",
            ">",
            "<>",
            "LIKE"});
            this.CMBField5Comparison.Location = new System.Drawing.Point(179, 5);
            this.CMBField5Comparison.Name = "CMBField5Comparison";
            this.CMBField5Comparison.Size = new System.Drawing.Size(42, 21);
            this.CMBField5Comparison.TabIndex = 2;
            // 
            // PANField4
            // 
            this.PANField4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANField4.Controls.Add(this.DTPField4To);
            this.PANField4.Controls.Add(this.DTPField4From);
            this.PANField4.Controls.Add(this.NUDField4);
            this.PANField4.Controls.Add(this.TXTField4);
            this.PANField4.Controls.Add(this.LBLField4);
            this.PANField4.Controls.Add(this.CHKField4);
            this.PANField4.Controls.Add(this.CMBField4Comparison);
            this.PANField4.Enabled = false;
            this.PANField4.Location = new System.Drawing.Point(45, 126);
            this.PANField4.Name = "PANField4";
            this.PANField4.Size = new System.Drawing.Size(386, 31);
            this.PANField4.TabIndex = 24;
            // 
            // DTPField4To
            // 
            this.DTPField4To.Location = new System.Drawing.Point(143, 6);
            this.DTPField4To.Name = "DTPField4To";
            this.DTPField4To.Size = new System.Drawing.Size(41, 20);
            this.DTPField4To.TabIndex = 12;
            this.DTPField4To.Visible = false;
            // 
            // DTPField4From
            // 
            this.DTPField4From.Location = new System.Drawing.Point(335, 0);
            this.DTPField4From.Name = "DTPField4From";
            this.DTPField4From.Size = new System.Drawing.Size(41, 20);
            this.DTPField4From.TabIndex = 11;
            this.DTPField4From.Visible = false;
            // 
            // NUDField4
            // 
            this.NUDField4.Location = new System.Drawing.Point(236, 3);
            this.NUDField4.Name = "NUDField4";
            this.NUDField4.Size = new System.Drawing.Size(62, 20);
            this.NUDField4.TabIndex = 10;
            this.NUDField4.Visible = false;
            // 
            // TXTField4
            // 
            this.TXTField4.Location = new System.Drawing.Point(232, 3);
            this.TXTField4.Name = "TXTField4";
            this.TXTField4.Size = new System.Drawing.Size(91, 20);
            this.TXTField4.TabIndex = 9;
            this.TXTField4.Visible = false;
            // 
            // LBLField4
            // 
            this.LBLField4.AutoSize = true;
            this.LBLField4.Location = new System.Drawing.Point(3, 7);
            this.LBLField4.Name = "LBLField4";
            this.LBLField4.Size = new System.Drawing.Size(0, 13);
            this.LBLField4.TabIndex = 7;
            // 
            // CHKField4
            // 
            this.CHKField4.AutoSize = true;
            this.CHKField4.Location = new System.Drawing.Point(88, 9);
            this.CHKField4.Name = "CHKField4";
            this.CHKField4.Size = new System.Drawing.Size(15, 14);
            this.CHKField4.TabIndex = 6;
            this.CHKField4.UseVisualStyleBackColor = true;
            this.CHKField4.Visible = false;
            // 
            // CMBField4Comparison
            // 
            this.CMBField4Comparison.FormattingEnabled = true;
            this.CMBField4Comparison.Items.AddRange(new object[] {
            "=",
            "<",
            ">",
            "<>",
            "LIKE"});
            this.CMBField4Comparison.Location = new System.Drawing.Point(179, 3);
            this.CMBField4Comparison.Name = "CMBField4Comparison";
            this.CMBField4Comparison.Size = new System.Drawing.Size(42, 21);
            this.CMBField4Comparison.TabIndex = 2;
            // 
            // PANField2
            // 
            this.PANField2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANField2.Controls.Add(this.DTPField2To);
            this.PANField2.Controls.Add(this.DTPField2From);
            this.PANField2.Controls.Add(this.NUDField2);
            this.PANField2.Controls.Add(this.TXTField2);
            this.PANField2.Controls.Add(this.LBLField2);
            this.PANField2.Controls.Add(this.CHKField2);
            this.PANField2.Controls.Add(this.CMBField2Comparison);
            this.PANField2.Enabled = false;
            this.PANField2.Location = new System.Drawing.Point(45, 55);
            this.PANField2.Name = "PANField2";
            this.PANField2.Size = new System.Drawing.Size(386, 31);
            this.PANField2.TabIndex = 22;
            // 
            // DTPField2To
            // 
            this.DTPField2To.Location = new System.Drawing.Point(152, 3);
            this.DTPField2To.Name = "DTPField2To";
            this.DTPField2To.Size = new System.Drawing.Size(41, 20);
            this.DTPField2To.TabIndex = 12;
            this.DTPField2To.Visible = false;
            // 
            // DTPField2From
            // 
            this.DTPField2From.Location = new System.Drawing.Point(335, 3);
            this.DTPField2From.Name = "DTPField2From";
            this.DTPField2From.Size = new System.Drawing.Size(41, 20);
            this.DTPField2From.TabIndex = 11;
            this.DTPField2From.Visible = false;
            // 
            // NUDField2
            // 
            this.NUDField2.Location = new System.Drawing.Point(236, 6);
            this.NUDField2.Name = "NUDField2";
            this.NUDField2.Size = new System.Drawing.Size(62, 20);
            this.NUDField2.TabIndex = 10;
            this.NUDField2.Visible = false;
            // 
            // TXTField2
            // 
            this.TXTField2.Location = new System.Drawing.Point(232, 5);
            this.TXTField2.Name = "TXTField2";
            this.TXTField2.Size = new System.Drawing.Size(91, 20);
            this.TXTField2.TabIndex = 9;
            this.TXTField2.Visible = false;
            // 
            // LBLField2
            // 
            this.LBLField2.AutoSize = true;
            this.LBLField2.Location = new System.Drawing.Point(5, 7);
            this.LBLField2.Name = "LBLField2";
            this.LBLField2.Size = new System.Drawing.Size(0, 13);
            this.LBLField2.TabIndex = 7;
            // 
            // CHKField2
            // 
            this.CHKField2.AutoSize = true;
            this.CHKField2.Location = new System.Drawing.Point(90, 9);
            this.CHKField2.Name = "CHKField2";
            this.CHKField2.Size = new System.Drawing.Size(15, 14);
            this.CHKField2.TabIndex = 6;
            this.CHKField2.UseVisualStyleBackColor = true;
            this.CHKField2.Visible = false;
            // 
            // CMBField2Comparison
            // 
            this.CMBField2Comparison.FormattingEnabled = true;
            this.CMBField2Comparison.Items.AddRange(new object[] {
            "=",
            "<",
            ">",
            "<>",
            "LIKE"});
            this.CMBField2Comparison.Location = new System.Drawing.Point(179, 6);
            this.CMBField2Comparison.Name = "CMBField2Comparison";
            this.CMBField2Comparison.Size = new System.Drawing.Size(42, 21);
            this.CMBField2Comparison.TabIndex = 3;
            // 
            // PANField3
            // 
            this.PANField3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANField3.Controls.Add(this.DTPField3o);
            this.PANField3.Controls.Add(this.DTPField3From);
            this.PANField3.Controls.Add(this.NUDField3);
            this.PANField3.Controls.Add(this.TXTField3);
            this.PANField3.Controls.Add(this.LBLField3);
            this.PANField3.Controls.Add(this.CHKField3);
            this.PANField3.Controls.Add(this.CMBField3Comparison);
            this.PANField3.Enabled = false;
            this.PANField3.Location = new System.Drawing.Point(45, 90);
            this.PANField3.Name = "PANField3";
            this.PANField3.Size = new System.Drawing.Size(386, 31);
            this.PANField3.TabIndex = 23;
            // 
            // DTPField3o
            // 
            this.DTPField3o.Location = new System.Drawing.Point(134, 2);
            this.DTPField3o.Name = "DTPField3o";
            this.DTPField3o.Size = new System.Drawing.Size(41, 20);
            this.DTPField3o.TabIndex = 12;
            this.DTPField3o.Visible = false;
            // 
            // DTPField3From
            // 
            this.DTPField3From.Location = new System.Drawing.Point(339, 3);
            this.DTPField3From.Name = "DTPField3From";
            this.DTPField3From.Size = new System.Drawing.Size(41, 20);
            this.DTPField3From.TabIndex = 11;
            this.DTPField3From.Visible = false;
            // 
            // NUDField3
            // 
            this.NUDField3.Location = new System.Drawing.Point(240, 7);
            this.NUDField3.Name = "NUDField3";
            this.NUDField3.Size = new System.Drawing.Size(62, 20);
            this.NUDField3.TabIndex = 10;
            this.NUDField3.Visible = false;
            // 
            // TXTField3
            // 
            this.TXTField3.Location = new System.Drawing.Point(237, 6);
            this.TXTField3.Name = "TXTField3";
            this.TXTField3.Size = new System.Drawing.Size(91, 20);
            this.TXTField3.TabIndex = 9;
            this.TXTField3.Visible = false;
            // 
            // LBLField3
            // 
            this.LBLField3.AutoSize = true;
            this.LBLField3.Location = new System.Drawing.Point(5, 7);
            this.LBLField3.Name = "LBLField3";
            this.LBLField3.Size = new System.Drawing.Size(0, 13);
            this.LBLField3.TabIndex = 7;
            // 
            // CHKField3
            // 
            this.CHKField3.AutoSize = true;
            this.CHKField3.Location = new System.Drawing.Point(90, 9);
            this.CHKField3.Name = "CHKField3";
            this.CHKField3.Size = new System.Drawing.Size(15, 14);
            this.CHKField3.TabIndex = 6;
            this.CHKField3.UseVisualStyleBackColor = true;
            this.CHKField3.Visible = false;
            // 
            // CMBField3Comparison
            // 
            this.CMBField3Comparison.FormattingEnabled = true;
            this.CMBField3Comparison.Items.AddRange(new object[] {
            "=",
            "<",
            ">",
            "<>",
            "LIKE"});
            this.CMBField3Comparison.Location = new System.Drawing.Point(179, 3);
            this.CMBField3Comparison.Name = "CMBField3Comparison";
            this.CMBField3Comparison.Size = new System.Drawing.Size(42, 21);
            this.CMBField3Comparison.TabIndex = 2;
            // 
            // PANField1
            // 
            this.PANField1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANField1.Controls.Add(this.DTPField1To);
            this.PANField1.Controls.Add(this.DTPField1From);
            this.PANField1.Controls.Add(this.NUDField1);
            this.PANField1.Controls.Add(this.TXTField1);
            this.PANField1.Controls.Add(this.LBLField1);
            this.PANField1.Controls.Add(this.CHKField1);
            this.PANField1.Controls.Add(this.CMBField1Comparison);
            this.PANField1.Enabled = false;
            this.PANField1.Location = new System.Drawing.Point(45, 19);
            this.PANField1.Name = "PANField1";
            this.PANField1.Size = new System.Drawing.Size(386, 31);
            this.PANField1.TabIndex = 21;
            // 
            // DTPField1To
            // 
            this.DTPField1To.Location = new System.Drawing.Point(152, 3);
            this.DTPField1To.Name = "DTPField1To";
            this.DTPField1To.Size = new System.Drawing.Size(41, 20);
            this.DTPField1To.TabIndex = 9;
            this.DTPField1To.Visible = false;
            // 
            // DTPField1From
            // 
            this.DTPField1From.Location = new System.Drawing.Point(336, 0);
            this.DTPField1From.Name = "DTPField1From";
            this.DTPField1From.Size = new System.Drawing.Size(41, 20);
            this.DTPField1From.TabIndex = 8;
            this.DTPField1From.Visible = false;
            // 
            // NUDField1
            // 
            this.NUDField1.Location = new System.Drawing.Point(237, 3);
            this.NUDField1.Name = "NUDField1";
            this.NUDField1.Size = new System.Drawing.Size(62, 20);
            this.NUDField1.TabIndex = 7;
            this.NUDField1.Visible = false;
            // 
            // TXTField1
            // 
            this.TXTField1.Location = new System.Drawing.Point(233, 3);
            this.TXTField1.Name = "TXTField1";
            this.TXTField1.Size = new System.Drawing.Size(91, 20);
            this.TXTField1.TabIndex = 6;
            this.TXTField1.Visible = false;
            // 
            // LBLField1
            // 
            this.LBLField1.AutoSize = true;
            this.LBLField1.Location = new System.Drawing.Point(5, 7);
            this.LBLField1.Name = "LBLField1";
            this.LBLField1.Size = new System.Drawing.Size(0, 13);
            this.LBLField1.TabIndex = 5;
            // 
            // CHKField1
            // 
            this.CHKField1.AutoSize = true;
            this.CHKField1.Location = new System.Drawing.Point(90, 9);
            this.CHKField1.Name = "CHKField1";
            this.CHKField1.Size = new System.Drawing.Size(15, 14);
            this.CHKField1.TabIndex = 4;
            this.CHKField1.UseVisualStyleBackColor = true;
            this.CHKField1.Visible = false;
            // 
            // CMBField1Comparison
            // 
            this.CMBField1Comparison.FormattingEnabled = true;
            this.CMBField1Comparison.Items.AddRange(new object[] {
            "=",
            "<",
            ">",
            "<>",
            "LIKE"});
            this.CMBField1Comparison.Location = new System.Drawing.Point(179, 6);
            this.CMBField1Comparison.Name = "CMBField1Comparison";
            this.CMBField1Comparison.Size = new System.Drawing.Size(42, 21);
            this.CMBField1Comparison.TabIndex = 3;
            this.CMBField1Comparison.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CMBField1Comparison_KeyDown);
            // 
            // CHKPanel6
            // 
            this.CHKPanel6.AutoSize = true;
            this.CHKPanel6.Location = new System.Drawing.Point(21, 205);
            this.CHKPanel6.Name = "CHKPanel6";
            this.CHKPanel6.Size = new System.Drawing.Size(15, 14);
            this.CHKPanel6.TabIndex = 20;
            this.CHKPanel6.UseVisualStyleBackColor = true;
            // 
            // CHKPanel5
            // 
            this.CHKPanel5.AutoSize = true;
            this.CHKPanel5.Location = new System.Drawing.Point(21, 169);
            this.CHKPanel5.Name = "CHKPanel5";
            this.CHKPanel5.Size = new System.Drawing.Size(15, 14);
            this.CHKPanel5.TabIndex = 19;
            this.CHKPanel5.UseVisualStyleBackColor = true;
            // 
            // CHKPanel4
            // 
            this.CHKPanel4.AutoSize = true;
            this.CHKPanel4.Location = new System.Drawing.Point(21, 135);
            this.CHKPanel4.Name = "CHKPanel4";
            this.CHKPanel4.Size = new System.Drawing.Size(15, 14);
            this.CHKPanel4.TabIndex = 18;
            this.CHKPanel4.UseVisualStyleBackColor = true;
            // 
            // CHKPanel3
            // 
            this.CHKPanel3.AutoSize = true;
            this.CHKPanel3.Location = new System.Drawing.Point(21, 96);
            this.CHKPanel3.Name = "CHKPanel3";
            this.CHKPanel3.Size = new System.Drawing.Size(15, 14);
            this.CHKPanel3.TabIndex = 17;
            this.CHKPanel3.UseVisualStyleBackColor = true;
            // 
            // CHKPanel2
            // 
            this.CHKPanel2.AutoSize = true;
            this.CHKPanel2.Location = new System.Drawing.Point(21, 63);
            this.CHKPanel2.Name = "CHKPanel2";
            this.CHKPanel2.Size = new System.Drawing.Size(15, 14);
            this.CHKPanel2.TabIndex = 16;
            this.CHKPanel2.UseVisualStyleBackColor = true;
            // 
            // CHKPanel1
            // 
            this.CHKPanel1.AutoSize = true;
            this.CHKPanel1.Location = new System.Drawing.Point(21, 28);
            this.CHKPanel1.Name = "CHKPanel1";
            this.CHKPanel1.Size = new System.Drawing.Size(15, 14);
            this.CHKPanel1.TabIndex = 15;
            this.CHKPanel1.UseVisualStyleBackColor = true;
            // 
            // frmFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BTNClose;
            this.ClientSize = new System.Drawing.Size(657, 549);
            this.Controls.Add(this.PANField6);
            this.Controls.Add(this.PANField5);
            this.Controls.Add(this.PANField4);
            this.Controls.Add(this.PANField2);
            this.Controls.Add(this.PANField3);
            this.Controls.Add(this.PANField1);
            this.Controls.Add(this.CHKPanel6);
            this.Controls.Add(this.CHKPanel5);
            this.Controls.Add(this.CHKPanel4);
            this.Controls.Add(this.CHKPanel3);
            this.Controls.Add(this.CHKPanel2);
            this.Controls.Add(this.CHKPanel1);
            this.Controls.Add(this.DGVResults);
            this.Controls.Add(this.BTNFind);
            this.Controls.Add(this.BTNClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFind";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form_Find_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmFind_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.DGVResults)).EndInit();
            this.PANField6.ResumeLayout(false);
            this.PANField6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDField6)).EndInit();
            this.PANField5.ResumeLayout(false);
            this.PANField5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDField5)).EndInit();
            this.PANField4.ResumeLayout(false);
            this.PANField4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDField4)).EndInit();
            this.PANField2.ResumeLayout(false);
            this.PANField2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDField2)).EndInit();
            this.PANField3.ResumeLayout(false);
            this.PANField3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDField3)).EndInit();
            this.PANField1.ResumeLayout(false);
            this.PANField1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDField1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button BTNClose;
        private Button BTNFind;
        private DataGridView DGVResults;
        private Panel PANField6;
        private DateTimePicker DTPField6To;
        private DateTimePicker DTPField6From;
        private NumericUpDown NUDField6;
        private TextBox TXTField6;
        private Label LBLField6;
        private CheckBox CHKField6;
        private ComboBox CMBField6Comparison;
        private Panel PANField5;
        private DateTimePicker DTPField5To;
        private DateTimePicker DTPField5From;
        private NumericUpDown NUDField5;
        private TextBox TXTField5;
        private Label LBLField5;
        private CheckBox CHKField5;
        private ComboBox CMBField5Comparison;
        private Panel PANField4;
        private DateTimePicker DTPField4To;
        private DateTimePicker DTPField4From;
        private NumericUpDown NUDField4;
        private TextBox TXTField4;
        private Label LBLField4;
        private CheckBox CHKField4;
        private ComboBox CMBField4Comparison;
        private Panel PANField2;
        private DateTimePicker DTPField2To;
        private DateTimePicker DTPField2From;
        private NumericUpDown NUDField2;
        private TextBox TXTField2;
        private Label LBLField2;
        private CheckBox CHKField2;
        private ComboBox CMBField2Comparison;
        private Panel PANField3;
        private DateTimePicker DTPField3o;
        private DateTimePicker DTPField3From;
        private NumericUpDown NUDField3;
        private TextBox TXTField3;
        private Label LBLField3;
        private CheckBox CHKField3;
        private ComboBox CMBField3Comparison;
        private Panel PANField1;
        private DateTimePicker DTPField1To;
        private DateTimePicker DTPField1From;
        private NumericUpDown NUDField1;
        private TextBox TXTField1;
        private Label LBLField1;
        private CheckBox CHKField1;
        private ComboBox CMBField1Comparison;
        private CheckBox CHKPanel6;
        private CheckBox CHKPanel5;
        private CheckBox CHKPanel4;
        private CheckBox CHKPanel3;
        private CheckBox CHKPanel2;
        private CheckBox CHKPanel1;
    }
}