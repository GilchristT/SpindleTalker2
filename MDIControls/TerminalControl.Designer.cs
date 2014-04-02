namespace SpindleTalker2
{
    partial class TerminalControl
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
            this.tableLayoutPanelSerialBase = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxResponse = new System.Windows.Forms.GroupBox();
            this.textBoxResponse = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxSent = new System.Windows.Forms.TextBox();
            this.commandBuilder1 = new SpindleTalker2.UserControls.CommandBuilder();
            this.tableLayoutPanelSerialBase.SuspendLayout();
            this.groupBoxResponse.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelSerialBase
            // 
            this.tableLayoutPanelSerialBase.ColumnCount = 2;
            this.tableLayoutPanelSerialBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSerialBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSerialBase.Controls.Add(this.groupBoxResponse, 0, 0);
            this.tableLayoutPanelSerialBase.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanelSerialBase.Controls.Add(this.commandBuilder1, 0, 1);
            this.tableLayoutPanelSerialBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelSerialBase.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelSerialBase.Name = "tableLayoutPanelSerialBase";
            this.tableLayoutPanelSerialBase.RowCount = 2;
            this.tableLayoutPanelSerialBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSerialBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanelSerialBase.Size = new System.Drawing.Size(730, 214);
            this.tableLayoutPanelSerialBase.TabIndex = 0;
            // 
            // groupBoxResponse
            // 
            this.groupBoxResponse.Controls.Add(this.textBoxResponse);
            this.groupBoxResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxResponse.Location = new System.Drawing.Point(368, 3);
            this.groupBoxResponse.Name = "groupBoxResponse";
            this.groupBoxResponse.Size = new System.Drawing.Size(359, 148);
            this.groupBoxResponse.TabIndex = 10;
            this.groupBoxResponse.TabStop = false;
            this.groupBoxResponse.Text = "Response";
            // 
            // textBoxResponse
            // 
            this.textBoxResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxResponse.Location = new System.Drawing.Point(3, 16);
            this.textBoxResponse.Multiline = true;
            this.textBoxResponse.Name = "textBoxResponse";
            this.textBoxResponse.ReadOnly = true;
            this.textBoxResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResponse.Size = new System.Drawing.Size(353, 129);
            this.textBoxResponse.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxSent);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(359, 148);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sent Command";
            // 
            // textBoxSent
            // 
            this.textBoxSent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSent.Location = new System.Drawing.Point(3, 16);
            this.textBoxSent.Multiline = true;
            this.textBoxSent.Name = "textBoxSent";
            this.textBoxSent.ReadOnly = true;
            this.textBoxSent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSent.Size = new System.Drawing.Size(353, 129);
            this.textBoxSent.TabIndex = 0;
            // 
            // commandBuilder1
            // 
            this.tableLayoutPanelSerialBase.SetColumnSpan(this.commandBuilder1, 2);
            this.commandBuilder1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandBuilder1.Location = new System.Drawing.Point(3, 157);
            this.commandBuilder1.Name = "commandBuilder1";
            this.commandBuilder1.Size = new System.Drawing.Size(724, 54);
            this.commandBuilder1.TabIndex = 11;
            // 
            // TerminalControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelSerialBase);
            this.Name = "TerminalControl";
            this.Size = new System.Drawing.Size(730, 214);
            this.Tag = "Terminal";
            this.tableLayoutPanelSerialBase.ResumeLayout(false);
            this.groupBoxResponse.ResumeLayout(false);
            this.groupBoxResponse.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSerialBase;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxResponse;
        private UserControls.CommandBuilder commandBuilder1;
        public System.Windows.Forms.TextBox textBoxResponse;
        public System.Windows.Forms.TextBox textBoxSent;
    }
}