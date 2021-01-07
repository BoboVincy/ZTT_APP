namespace ViewWinform.ModbusClient
{
    partial class ModbusClient
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
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonPortSetting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 0;
            // 
            // ButtonPortSetting
            // 
            this.ButtonPortSetting.Location = new System.Drawing.Point(20, 13);
            this.ButtonPortSetting.Name = "ButtonPortSetting";
            this.ButtonPortSetting.Size = new System.Drawing.Size(75, 23);
            this.ButtonPortSetting.TabIndex = 1;
            this.ButtonPortSetting.Text = "端口设置";
            this.ButtonPortSetting.UseVisualStyleBackColor = true;
            this.ButtonPortSetting.Click += new System.EventHandler(this.ButtonPortSetting_Click);
            // 
            // ModbusClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ButtonPortSetting);
            this.Controls.Add(this.label1);
            this.Name = "ModbusClient";
            this.Text = "ModbusClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonPortSetting;
    }
}