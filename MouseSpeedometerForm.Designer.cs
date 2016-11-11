namespace MouseSpeedometer
{
    partial class MouseSpeedometerForm
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
            this.set_cpi_input = new System.Windows.Forms.TextBox();
            this.set_cpi_btn = new System.Windows.Forms.Button();
            this.set_cpi_lbl = new System.Windows.Forms.Label();
            this.cpi_lbl = new System.Windows.Forms.Label();
            this.max_speed_lbl = new System.Windows.Forms.Label();
            this.current_speed_lbl = new System.Windows.Forms.Label();
            this.CPI_display = new System.Windows.Forms.Label();
            this.max_speed_readout_lbl = new System.Windows.Forms.Label();
            this.cpi_readout_lbl = new System.Windows.Forms.Label();
            this.current_speed_readout_lbl = new System.Windows.Forms.Label();
            this.reset_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // set_cpi_input
            // 
            this.set_cpi_input.Location = new System.Drawing.Point(12, 34);
            this.set_cpi_input.Name = "set_cpi_input";
            this.set_cpi_input.Size = new System.Drawing.Size(100, 20);
            this.set_cpi_input.TabIndex = 0;
            // 
            // set_cpi_btn
            // 
            this.set_cpi_btn.Location = new System.Drawing.Point(129, 32);
            this.set_cpi_btn.Name = "set_cpi_btn";
            this.set_cpi_btn.Size = new System.Drawing.Size(75, 23);
            this.set_cpi_btn.TabIndex = 1;
            this.set_cpi_btn.Text = "Set CPI";
            this.set_cpi_btn.UseVisualStyleBackColor = true;
            this.set_cpi_btn.Click += new System.EventHandler(this.set_cpi_button_click);
            // 
            // set_cpi_lbl
            // 
            this.set_cpi_lbl.AutoSize = true;
            this.set_cpi_lbl.Location = new System.Drawing.Point(12, 18);
            this.set_cpi_lbl.Name = "set_cpi_lbl";
            this.set_cpi_lbl.Size = new System.Drawing.Size(46, 13);
            this.set_cpi_lbl.TabIndex = 2;
            this.set_cpi_lbl.Text = "Set CPI:";
            // 
            // cpi_lbl
            // 
            this.cpi_lbl.AutoSize = true;
            this.cpi_lbl.Location = new System.Drawing.Point(12, 84);
            this.cpi_lbl.Name = "cpi_lbl";
            this.cpi_lbl.Size = new System.Drawing.Size(24, 13);
            this.cpi_lbl.TabIndex = 3;
            this.cpi_lbl.Text = "CPI";
            // 
            // max_speed_lbl
            // 
            this.max_speed_lbl.AutoSize = true;
            this.max_speed_lbl.Location = new System.Drawing.Point(12, 137);
            this.max_speed_lbl.Name = "max_speed_lbl";
            this.max_speed_lbl.Size = new System.Drawing.Size(88, 13);
            this.max_speed_lbl.TabIndex = 4;
            this.max_speed_lbl.Text = "Max Speed (m/s)";
            // 
            // current_speed_lbl
            // 
            this.current_speed_lbl.AutoSize = true;
            this.current_speed_lbl.Location = new System.Drawing.Point(12, 193);
            this.current_speed_lbl.Name = "current_speed_lbl";
            this.current_speed_lbl.Size = new System.Drawing.Size(102, 13);
            this.current_speed_lbl.TabIndex = 5;
            this.current_speed_lbl.Text = "Current Speed (m/s)";
            // 
            // CPI_display
            // 
            this.CPI_display.AutoSize = true;
            this.CPI_display.Location = new System.Drawing.Point(166, 84);
            this.CPI_display.Name = "CPI_display";
            this.CPI_display.Size = new System.Drawing.Size(0, 13);
            this.CPI_display.TabIndex = 6;
            // 
            // max_speed_readout_lbl
            // 
            this.max_speed_readout_lbl.AutoSize = true;
            this.max_speed_readout_lbl.Location = new System.Drawing.Point(133, 137);
            this.max_speed_readout_lbl.Name = "max_speed_readout_lbl";
            this.max_speed_readout_lbl.Size = new System.Drawing.Size(35, 13);
            this.max_speed_readout_lbl.TabIndex = 7;
            this.max_speed_readout_lbl.Text = "label5";
            // 
            // cpi_readout_lbl
            // 
            this.cpi_readout_lbl.AutoSize = true;
            this.cpi_readout_lbl.Location = new System.Drawing.Point(133, 84);
            this.cpi_readout_lbl.Name = "cpi_readout_lbl";
            this.cpi_readout_lbl.Size = new System.Drawing.Size(114, 13);
            this.cpi_readout_lbl.TabIndex = 8;
            this.cpi_readout_lbl.Text = "Please set a CPI value";
            // 
            // current_speed_readout_lbl
            // 
            this.current_speed_readout_lbl.AutoSize = true;
            this.current_speed_readout_lbl.Location = new System.Drawing.Point(133, 193);
            this.current_speed_readout_lbl.Name = "current_speed_readout_lbl";
            this.current_speed_readout_lbl.Size = new System.Drawing.Size(35, 13);
            this.current_speed_readout_lbl.TabIndex = 9;
            this.current_speed_readout_lbl.Text = "label5";
            // 
            // reset_btn
            // 
            this.reset_btn.Location = new System.Drawing.Point(209, 132);
            this.reset_btn.Name = "reset_btn";
            this.reset_btn.Size = new System.Drawing.Size(63, 23);
            this.reset_btn.TabIndex = 10;
            this.reset_btn.Text = "Reset";
            this.reset_btn.UseVisualStyleBackColor = true;
            // 
            // MouseSpeedometerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.reset_btn);
            this.Controls.Add(this.current_speed_readout_lbl);
            this.Controls.Add(this.cpi_readout_lbl);
            this.Controls.Add(this.max_speed_readout_lbl);
            this.Controls.Add(this.CPI_display);
            this.Controls.Add(this.current_speed_lbl);
            this.Controls.Add(this.max_speed_lbl);
            this.Controls.Add(this.cpi_lbl);
            this.Controls.Add(this.set_cpi_lbl);
            this.Controls.Add(this.set_cpi_btn);
            this.Controls.Add(this.set_cpi_input);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MouseSpeedometerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mouse Speedometer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox set_cpi_input;
        private System.Windows.Forms.Button set_cpi_btn;
        private System.Windows.Forms.Label set_cpi_lbl;
        private System.Windows.Forms.Label cpi_lbl;
        private System.Windows.Forms.Label max_speed_lbl;
        private System.Windows.Forms.Label current_speed_lbl;
        private System.Windows.Forms.Label CPI_display;
        private System.Windows.Forms.Label max_speed_readout_lbl;
        private System.Windows.Forms.Label cpi_readout_lbl;
        private System.Windows.Forms.Label current_speed_readout_lbl;
        private System.Windows.Forms.Button reset_btn;
    }
}

