
namespace RochesterConverter
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openPdfMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCsvMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.itemCodeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.qtyTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.orderListView = new System.Windows.Forms.ListView();
            this.orderDateTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.customerTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.UDFPOTextBox = new System.Windows.Forms.TextBox();
            this.MASPOTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.UDFDocTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.errorListView = new System.Windows.Forms.ListView();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1336, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPdfMenuStrip,
            this.saveCsvMenuStrip});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // openPdfMenuStrip
            // 
            this.openPdfMenuStrip.Name = "openPdfMenuStrip";
            this.openPdfMenuStrip.Size = new System.Drawing.Size(127, 22);
            this.openPdfMenuStrip.Text = "Open PDF";
            this.openPdfMenuStrip.Click += new System.EventHandler(this.openPdfMenuStrip_Click);
            // 
            // saveCsvMenuStrip
            // 
            this.saveCsvMenuStrip.Name = "saveCsvMenuStrip";
            this.saveCsvMenuStrip.Size = new System.Drawing.Size(127, 22);
            this.saveCsvMenuStrip.Text = "Save CSV";
            this.saveCsvMenuStrip.Click += new System.EventHandler(this.saveCsvMenuStrip_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(64, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 30);
            this.label1.TabIndex = 1;
            // 
            // itemCodeTextBox
            // 
            this.itemCodeTextBox.Location = new System.Drawing.Point(233, 269);
            this.itemCodeTextBox.Name = "itemCodeTextBox";
            this.itemCodeTextBox.Size = new System.Drawing.Size(111, 23);
            this.itemCodeTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(146, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Item Code";
            // 
            // qtyTextBox
            // 
            this.qtyTextBox.Location = new System.Drawing.Point(525, 269);
            this.qtyTextBox.Name = "qtyTextBox";
            this.qtyTextBox.Size = new System.Drawing.Size(111, 23);
            this.qtyTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(476, 267);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Qty";
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.saveButton.Location = new System.Drawing.Point(707, 269);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(99, 31);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Modify";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.ModifyButton_Click);
            // 
            // orderListView
            // 
            this.orderListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.orderListView.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.orderListView.HideSelection = false;
            this.orderListView.Location = new System.Drawing.Point(0, 306);
            this.orderListView.Name = "orderListView";
            this.orderListView.Size = new System.Drawing.Size(1336, 548);
            this.orderListView.TabIndex = 6;
            this.orderListView.UseCompatibleStateImageBehavior = false;
            this.orderListView.Click += new System.EventHandler(this.ListView1_Click);
            // 
            // orderDateTextBox
            // 
            this.orderDateTextBox.Location = new System.Drawing.Point(233, 227);
            this.orderDateTextBox.Name = "orderDateTextBox";
            this.orderDateTextBox.Size = new System.Drawing.Size(111, 23);
            this.orderDateTextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(140, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "Order Date";
            // 
            // customerTextBox
            // 
            this.customerTextBox.Location = new System.Drawing.Point(233, 191);
            this.customerTextBox.Name = "customerTextBox";
            this.customerTextBox.Size = new System.Drawing.Size(111, 23);
            this.customerTextBox.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(149, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "Customer";
            // 
            // UDFPOTextBox
            // 
            this.UDFPOTextBox.Location = new System.Drawing.Point(525, 227);
            this.UDFPOTextBox.Name = "UDFPOTextBox";
            this.UDFPOTextBox.Size = new System.Drawing.Size(111, 23);
            this.UDFPOTextBox.TabIndex = 3;
            // 
            // MASPOTextBox
            // 
            this.MASPOTextBox.Location = new System.Drawing.Point(525, 185);
            this.MASPOTextBox.Name = "MASPOTextBox";
            this.MASPOTextBox.Size = new System.Drawing.Size(111, 23);
            this.MASPOTextBox.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(446, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 21);
            this.label6.TabIndex = 4;
            this.label6.Text = "UDF PO";
            // 
            // UDFDocTextBox
            // 
            this.UDFDocTextBox.Location = new System.Drawing.Point(525, 149);
            this.UDFDocTextBox.Name = "UDFDocTextBox";
            this.UDFDocTextBox.Size = new System.Drawing.Size(111, 23);
            this.UDFDocTextBox.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(443, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 21);
            this.label7.TabIndex = 4;
            this.label7.Text = "MAS PO";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(440, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 21);
            this.label8.TabIndex = 4;
            this.label8.Text = "UDF Doc";
            // 
            // errorListView
            // 
            this.errorListView.Dock = System.Windows.Forms.DockStyle.Right;
            this.errorListView.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.errorListView.HideSelection = false;
            this.errorListView.Location = new System.Drawing.Point(944, 24);
            this.errorListView.Name = "errorListView";
            this.errorListView.Size = new System.Drawing.Size(392, 282);
            this.errorListView.TabIndex = 7;
            this.errorListView.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 854);
            this.Controls.Add(this.errorListView);
            this.Controls.Add(this.orderListView);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UDFDocTextBox);
            this.Controls.Add(this.customerTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MASPOTextBox);
            this.Controls.Add(this.orderDateTextBox);
            this.Controls.Add(this.qtyTextBox);
            this.Controls.Add(this.UDFPOTextBox);
            this.Controls.Add(this.itemCodeTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Rochester Converter";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openPdfMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem saveCsvMenuStrip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox itemCodeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox qtyTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ListView orderListView;
        private System.Windows.Forms.TextBox orderDateTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox customerTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox UDFPOTextBox;
        private System.Windows.Forms.TextBox MASPOTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox UDFDocTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView errorListView;
    }
}

