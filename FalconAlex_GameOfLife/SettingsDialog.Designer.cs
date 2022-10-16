
namespace FalconAlex_GameOfLife
{
    partial class SettingsDialog
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
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TickTime = new System.Windows.Forms.TextBox();
            this.XSize = new System.Windows.Forms.TextBox();
            this.YSize = new System.Windows.Forms.TextBox();
            this.SquareArray = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Location = new System.Drawing.Point(12, 108);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 0;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(145, 108);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Milliseconds per Tick";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "X Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y Size";
            // 
            // TickTime
            // 
            this.TickTime.Location = new System.Drawing.Point(126, 5);
            this.TickTime.Name = "TickTime";
            this.TickTime.Size = new System.Drawing.Size(100, 20);
            this.TickTime.TabIndex = 5;
            // 
            // XSize
            // 
            this.XSize.Location = new System.Drawing.Point(126, 26);
            this.XSize.Name = "XSize";
            this.XSize.Size = new System.Drawing.Size(100, 20);
            this.XSize.TabIndex = 6;
            // 
            // YSize
            // 
            this.YSize.Location = new System.Drawing.Point(126, 48);
            this.YSize.Name = "YSize";
            this.YSize.Size = new System.Drawing.Size(100, 20);
            this.YSize.TabIndex = 7;
            // 
            // SquareArray
            // 
            this.SquareArray.AutoSize = true;
            this.SquareArray.Checked = true;
            this.SquareArray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SquareArray.Location = new System.Drawing.Point(72, 74);
            this.SquareArray.Name = "SquareArray";
            this.SquareArray.Size = new System.Drawing.Size(87, 17);
            this.SquareArray.TabIndex = 8;
            this.SquareArray.Text = "Square Array";
            this.SquareArray.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Max recommended: 150 x150";
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(232, 143);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SquareArray);
            this.Controls.Add(this.YSize);
            this.Controls.Add(this.XSize);
            this.Controls.Add(this.TickTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsDialog";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsDialog_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TickTime;
        private System.Windows.Forms.TextBox XSize;
        private System.Windows.Forms.TextBox YSize;
        private System.Windows.Forms.CheckBox SquareArray;
        private System.Windows.Forms.Label label4;
    }
}