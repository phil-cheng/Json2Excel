namespace json2Excel
{
    partial class IndexForm
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.jsonTextBox = new System.Windows.Forms.TextBox();
            this.transferButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.titleLabel.ForeColor = System.Drawing.Color.Red;
            this.titleLabel.Location = new System.Drawing.Point(39, 19);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(399, 33);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "输入或者拖拽json到文本框";
            // 
            // jsonTextBox
            // 
            this.jsonTextBox.AcceptsReturn = true;
            this.jsonTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jsonTextBox.Location = new System.Drawing.Point(31, 67);
            this.jsonTextBox.Multiline = true;
            this.jsonTextBox.Name = "jsonTextBox";
            this.jsonTextBox.Size = new System.Drawing.Size(811, 659);
            this.jsonTextBox.TabIndex = 1;
            // 
            // transferButton
            // 
            this.transferButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.transferButton.Location = new System.Drawing.Point(31, 747);
            this.transferButton.Name = "transferButton";
            this.transferButton.Size = new System.Drawing.Size(210, 50);
            this.transferButton.TabIndex = 2;
            this.transferButton.Text = "转换";
            this.transferButton.UseVisualStyleBackColor = true;
            this.transferButton.Click += new System.EventHandler(this.TransferButton_Click);
            // 
            // IndexForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 897);
            this.Controls.Add(this.transferButton);
            this.Controls.Add(this.jsonTextBox);
            this.Controls.Add(this.titleLabel);
            this.Name = "IndexForm";
            this.Text = "输入json";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.IndexForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.IndexForm_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox jsonTextBox;
        private System.Windows.Forms.Button transferButton;
    }
}