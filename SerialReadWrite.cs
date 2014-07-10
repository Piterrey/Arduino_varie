using System;
using System.IO.Ports;

namespace ArduinoSerial
{
    public partial class Form1 : Form
    {
        SerialPort port;
        public Form1()
        {
            InitializeComponent();
            port = new SerialPort("COM10", 9600);
            try
            {
                //un-comment this line to cause the arduino to re-boot when the serial connects
                //port.DtrEnabled = true;

                port.Open();
            }
            catch (Exception ex)
            {
                //alert the user that we could not connect to the serial port
            }
            
        }

        private void buttonA_Click(object sender, EventArgs e)
        {
            port.Write("a");
        }

        private void buttonB_Click(object sender, System.EventArgs e)
        {
            port.Write("b");
        }

        private void labelReceived_Click(object sender, System.EventArgs e)
        {

        }

        private void buttonListen_Click(object sender, System.EventArgs e)
        {
            textBoxReceived.Text = "";
            while (port.BytesToRead < 0) { }
            String myVar = port.ReadLine();
            textBoxReceived.Text = myVar;
        }
    }
}
