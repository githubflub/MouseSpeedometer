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

            // Start readouts using a timer
            readoutTimer = new Timer();
            readoutTimer.Tick += new EventHandler(update_readouts);
            readoutTimer.Interval = 100;
            readoutTimer.Start(); 
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

        private void updateMaxSpeedReadout(object MouseModel, double new_max_speed)
        {
            // if CPI hasn't been set, cancel. 
            if (mouse.get_cpi() <= 0)
            {
                max_speed_readout_lbl.Text = "Set CPI first";
                return; 
            }

            max_speed_readout_lbl.Text = new_max_speed.ToString(); 
        }

        private void updateCurrentSpeedReadout(object MouseModel, double new_current_speed)
        {
            // if CPI hasn't been set, cancel. 
            if (mouse.get_cpi() <= 0)
            {
                current_speed_readout_lbl.Text = "Set CPI first";
                return;
            }

            current_speed_readout_lbl.Text = new_current_speed.ToString(); 
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
            }
            else
            {
                cpi_readout_lbl.Text = "Please set a CPI value"; 
            }
        }

        private void update_readouts(object sender, EventArgs e) {}

        private void reset(object sender, EventArgs e)
        {
            mouse.reset(); 
        }
    }
}
