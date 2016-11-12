using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/* System.Windows.Forms.Timer used to update readouts every x seconds */


namespace MouseSpeedometer
{
    public partial class MouseSpeedometerForm : Form
    {
        private MouseModel mouse;
        private Timer readoutTimer; 

        public MouseSpeedometerForm()
        {
            InitializeComponent();

            // Start model
            mouse = new MouseModel();

            /**
             * I believe Handle is a reference to this program
             * for the operating system to use. 
             */ 
            this.mouse.RegisterRawInputMouse(Handle); // Register mouse with operating system. 
            this.mouse.hnms += new MouseModel.NewMaxSpeedHandler(this.updateMaxSpeedReadout);
            this.mouse.hncs += new MouseModel.NewCurrentSpeedHandler(this.updateCurrentSpeedReadout); 
        }

        /**
         * Forms have a "hidden" method called WndProc. 
         * It receives messages from the OS. I am overriding it 
         * here so that I can customize it by making it run
         * ProcessRawInput(), which I defined in MouseModel.cs
         */ 
        protected override void WndProc(ref Message m)
        {
            this.mouse.ProcessRawInput(m);
            base.WndProc(ref m);
        }

        private void set_cpi_warning(Label lbl)
        {
            int fontsize = 8; 
            lbl.Font = new Font(lbl.Font.FontFamily, fontsize);
            lbl.Text = "Set CPI first";
        }

        private void updateMaxSpeedReadout(object MouseModel, double new_max_speed)
        {
            // if CPI hasn't been set, cancel. 
            if (mouse.cpiNotSet())
            {
                set_cpi_warning(max_speed_readout_lbl);
                return; 
            }

            max_speed_readout_lbl.Text = new_max_speed.ToString("N3"); 
        }

        private void updateCurrentSpeedReadout(object MouseModel, double new_current_speed)
        {
            // if CPI hasn't been set, cancel. 
            if (mouse.cpiNotSet())
            {
                set_cpi_warning(current_speed_readout_lbl);
                return;
            }

            current_speed_readout_lbl.Text = new_current_speed.ToString("N3"); 
        }
        
        private void set_cpi_button_click(object sender, EventArgs e)
        {
            int inputted_cpi;
            bool set_cpi_result; 

            int.TryParse(set_cpi_input.Text, out inputted_cpi);
            set_cpi_result = mouse.set_cpi(inputted_cpi); 

            if (set_cpi_result)
            {
                cpi_readout_lbl.Text = set_cpi_input.Text;
                cpi_set_font_boost(current_speed_readout_lbl);
                cpi_set_font_boost(max_speed_readout_lbl);

            }
            else
            {
                cpi_readout_lbl.Text = "Please set a CPI value"; 
            }
        }

        /**
         * Makes readout label font bigger after CPI 
         * has been set. 
         */ 
        private void cpi_set_font_boost(Label lbl)
        {
            int fontsize = 16;
            lbl.Font = new Font(lbl.Font.FontFamily, fontsize);
        }

        private void reset(object sender, EventArgs e)
        {
            mouse.reset(); 
        }
    }
}
