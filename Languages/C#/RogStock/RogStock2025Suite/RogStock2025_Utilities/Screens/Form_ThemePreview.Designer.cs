namespace RogStock2025_Utilities.Screens
{
    partial class Form_ThemePreview
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
            TreeNode treeNode1 = new TreeNode("SubChild1");
            TreeNode treeNode2 = new TreeNode("Child1", new TreeNode[] { treeNode1 });
            TreeNode treeNode3 = new TreeNode("Child2");
            TreeNode treeNode4 = new TreeNode("Root", new TreeNode[] { treeNode2, treeNode3 });
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "Item1", "SubItem1" }, -1);
            ListViewItem listViewItem2 = new ListViewItem(new string[] { "Item2", "SubItem2" }, -1);
            ListViewItem listViewItem3 = new ListViewItem(new string[] { "Item3", "SubItem3" }, -1);
            ListViewItem listViewItem4 = new ListViewItem(new string[] { "Item4", "SubItem4" }, -1);
            TestButton = new Button();
            TestLabel = new Label();
            TestRadioButton = new RadioButton();
            TestCheckBox = new CheckBox();
            TestNumericUpDown = new NumericUpDown();
            TestTextBox = new TextBox();
            TestComboBox = new ComboBox();
            TestTreeView = new TreeView();
            TestListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            TestListBox = new ListBox();
            TestPanel = new Panel();
            comboBox1 = new ComboBox();
            label1 = new Label();
            TestTabControl = new TabControl();
            TestTabPage = new TabPage();
            checkBox2 = new CheckBox();
            label2 = new Label();
            tabPage2 = new TabPage();
            listBox1 = new ListBox();
            label3 = new Label();
            TestDataGridView = new DataGridView();
            TestGroupBox = new GroupBox();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            TestStatusStrip = new StatusStrip();
            TestToolStripStatusLabel = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)TestNumericUpDown).BeginInit();
            TestPanel.SuspendLayout();
            TestTabControl.SuspendLayout();
            TestTabPage.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TestDataGridView).BeginInit();
            TestGroupBox.SuspendLayout();
            TestStatusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // TestButton
            // 
            TestButton.ForeColor = SystemColors.ControlText;
            TestButton.Location = new Point(434, 23);
            TestButton.Name = "TestButton";
            TestButton.Size = new Size(91, 23);
            TestButton.TabIndex = 67;
            TestButton.Text = " Test Button";
            TestButton.UseVisualStyleBackColor = true;
            // 
            // TestLabel
            // 
            TestLabel.AutoSize = true;
            TestLabel.Location = new Point(163, 85);
            TestLabel.Name = "TestLabel";
            TestLabel.Size = new Size(58, 15);
            TestLabel.TabIndex = 66;
            TestLabel.Text = "Test Label";
            // 
            // TestRadioButton
            // 
            TestRadioButton.AutoSize = true;
            TestRadioButton.Location = new Point(311, 81);
            TestRadioButton.Name = "TestRadioButton";
            TestRadioButton.Size = new Size(114, 19);
            TestRadioButton.TabIndex = 65;
            TestRadioButton.TabStop = true;
            TestRadioButton.Text = "Test RadioButton";
            TestRadioButton.UseVisualStyleBackColor = true;
            // 
            // TestCheckBox
            // 
            TestCheckBox.AutoSize = true;
            TestCheckBox.Location = new Point(311, 56);
            TestCheckBox.Name = "TestCheckBox";
            TestCheckBox.Size = new Size(102, 19);
            TestCheckBox.TabIndex = 63;
            TestCheckBox.Text = "Test CheckBox";
            TestCheckBox.UseVisualStyleBackColor = true;
            // 
            // TestNumericUpDown
            // 
            TestNumericUpDown.Location = new Point(163, 52);
            TestNumericUpDown.Name = "TestNumericUpDown";
            TestNumericUpDown.Size = new Size(120, 23);
            TestNumericUpDown.TabIndex = 62;
            // 
            // TestTextBox
            // 
            TestTextBox.Location = new Point(311, 23);
            TestTextBox.Name = "TestTextBox";
            TestTextBox.Size = new Size(100, 23);
            TestTextBox.TabIndex = 61;
            TestTextBox.Text = "Test TextBox";
            // 
            // TestComboBox
            // 
            TestComboBox.FormattingEnabled = true;
            TestComboBox.Items.AddRange(new object[] { "Item1", "Item2" });
            TestComboBox.Location = new Point(163, 23);
            TestComboBox.Name = "TestComboBox";
            TestComboBox.Size = new Size(121, 23);
            TestComboBox.TabIndex = 60;
            // 
            // TestTreeView
            // 
            TestTreeView.Location = new Point(401, 132);
            TestTreeView.Name = "TestTreeView";
            treeNode1.Name = "Node2";
            treeNode1.Text = "SubChild1";
            treeNode2.Name = "Node1";
            treeNode2.Text = "Child1";
            treeNode3.Name = "Node3";
            treeNode3.Text = "Child2";
            treeNode4.Name = "Node0";
            treeNode4.Text = "Root";
            TestTreeView.Nodes.AddRange(new TreeNode[] { treeNode4 });
            TestTreeView.Size = new Size(121, 97);
            TestTreeView.TabIndex = 59;
            // 
            // TestListView
            // 
            TestListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            TestListView.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2, listViewItem3, listViewItem4 });
            TestListView.Location = new Point(205, 132);
            TestListView.Name = "TestListView";
            TestListView.Size = new Size(178, 97);
            TestListView.TabIndex = 58;
            TestListView.UseCompatibleStateImageBehavior = false;
            TestListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Header1";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Header2";
            columnHeader2.Width = 100;
            // 
            // TestListBox
            // 
            TestListBox.FormattingEnabled = true;
            TestListBox.ItemHeight = 15;
            TestListBox.Items.AddRange(new object[] { "Item1", "Item2", "Item3", "Item4" });
            TestListBox.Location = new Point(12, 23);
            TestListBox.Name = "TestListBox";
            TestListBox.Size = new Size(120, 79);
            TestListBox.TabIndex = 57;
            // 
            // TestPanel
            // 
            TestPanel.Controls.Add(comboBox1);
            TestPanel.Controls.Add(label1);
            TestPanel.Location = new Point(13, 132);
            TestPanel.Name = "TestPanel";
            TestPanel.Size = new Size(170, 97);
            TestPanel.TabIndex = 56;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Item1", "Item2", "Item3", "Item4" });
            comboBox1.Location = new Point(13, 33);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(125, 23);
            comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 13);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 0;
            label1.Text = "Test Panel";
            // 
            // TestTabControl
            // 
            TestTabControl.Controls.Add(TestTabPage);
            TestTabControl.Controls.Add(tabPage2);
            TestTabControl.Location = new Point(540, 132);
            TestTabControl.Name = "TestTabControl";
            TestTabControl.SelectedIndex = 0;
            TestTabControl.Size = new Size(200, 100);
            TestTabControl.TabIndex = 55;
            // 
            // TestTabPage
            // 
            TestTabPage.Controls.Add(checkBox2);
            TestTabPage.Controls.Add(label2);
            TestTabPage.Location = new Point(4, 24);
            TestTabPage.Name = "TestTabPage";
            TestTabPage.Padding = new Padding(3);
            TestTabPage.Size = new Size(192, 72);
            TestTabPage.TabIndex = 0;
            TestTabPage.Text = "Test TabPage1";
            TestTabPage.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(16, 37);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(63, 19);
            checkBox2.TabIndex = 1;
            checkBox2.Text = "Option";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 10);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 0;
            label2.Text = "Page1";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(listBox1);
            tabPage2.Controls.Add(label3);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(192, 72);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Items.AddRange(new object[] { "Item1", "Item2" });
            listBox1.Location = new Point(15, 31);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(105, 34);
            listBox1.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 9);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 0;
            label3.Text = "Page2";
            // 
            // TestDataGridView
            // 
            TestDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TestDataGridView.Location = new Point(12, 238);
            TestDataGridView.Name = "TestDataGridView";
            TestDataGridView.Size = new Size(724, 126);
            TestDataGridView.TabIndex = 54;
            // 
            // TestGroupBox
            // 
            TestGroupBox.Controls.Add(radioButton2);
            TestGroupBox.Controls.Add(radioButton1);
            TestGroupBox.Location = new Point(544, 12);
            TestGroupBox.Name = "TestGroupBox";
            TestGroupBox.Size = new Size(153, 100);
            TestGroupBox.TabIndex = 53;
            TestGroupBox.TabStop = false;
            TestGroupBox.Text = "Test GroupBox";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(15, 59);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(67, 19);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "Button2";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(15, 34);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(67, 19);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "Button1";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // TestStatusStrip
            // 
            TestStatusStrip.Items.AddRange(new ToolStripItem[] { TestToolStripStatusLabel });
            TestStatusStrip.Location = new Point(0, 367);
            TestStatusStrip.Name = "TestStatusStrip";
            TestStatusStrip.Size = new Size(751, 22);
            TestStatusStrip.TabIndex = 68;
            TestStatusStrip.Text = "statusStrip1";
            // 
            // TestToolStripStatusLabel
            // 
            TestToolStripStatusLabel.Name = "TestToolStripStatusLabel";
            TestToolStripStatusLabel.Size = new Size(90, 17);
            TestToolStripStatusLabel.Text = "Status Bar Label";
            // 
            // Form_ThemePreview
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(751, 389);
            Controls.Add(TestStatusStrip);
            Controls.Add(TestButton);
            Controls.Add(TestLabel);
            Controls.Add(TestRadioButton);
            Controls.Add(TestCheckBox);
            Controls.Add(TestNumericUpDown);
            Controls.Add(TestTextBox);
            Controls.Add(TestComboBox);
            Controls.Add(TestTreeView);
            Controls.Add(TestListView);
            Controls.Add(TestListBox);
            Controls.Add(TestPanel);
            Controls.Add(TestTabControl);
            Controls.Add(TestDataGridView);
            Controls.Add(TestGroupBox);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form_ThemePreview";
            Text = "Form_ThemePreview";
            Load += Form_ThemePreview_Load;
            ((System.ComponentModel.ISupportInitialize)TestNumericUpDown).EndInit();
            TestPanel.ResumeLayout(false);
            TestPanel.PerformLayout();
            TestTabControl.ResumeLayout(false);
            TestTabPage.ResumeLayout(false);
            TestTabPage.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TestDataGridView).EndInit();
            TestGroupBox.ResumeLayout(false);
            TestGroupBox.PerformLayout();
            TestStatusStrip.ResumeLayout(false);
            TestStatusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button TestButton;
        private Label TestLabel;
        private RadioButton TestRadioButton;
        private CheckBox TestCheckBox;
        private NumericUpDown TestNumericUpDown;
        private TextBox TestTextBox;
        private ComboBox TestComboBox;
        private TreeView TestTreeView;
        private ListView TestListView;
        private ListBox TestListBox;
        private Panel TestPanel;
        private TabControl TestTabControl;
        private TabPage TestTabPage;
        private TabPage tabPage2;
        private DataGridView TestDataGridView;
        private GroupBox TestGroupBox;
        private StatusStrip TestStatusStrip;
        private ToolStripStatusLabel TestToolStripStatusLabel;
        private Label label1;
        private CheckBox checkBox2;
        private Label label2;
        private ListBox listBox1;
        private Label label3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ComboBox comboBox1;
    }
}