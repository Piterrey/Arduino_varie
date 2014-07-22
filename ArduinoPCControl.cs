using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.IO.Ports;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace ArduinoSerial
{
    public partial class Form1 : Form
    {
        
        SerialPort port;
        /*-------------------------------------------- codice per la simulazione del click ---------------------------------------*/

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]                // importo la dll di controllo del mouse

        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);         // definisco la firma del metodo

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;                                                                  //
        private const int MOUSEEVENTF_LEFTUP = 0x04;                                                                    // assegnazione costanti
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;                                                                 //
        private const int MOUSEEVENTF_RIGHTUP = 0x10;                                                                   //

        /*------------------------------------------------------------------------------------------------------------------------*/
        public Form1()
        {
            InitializeComponent();
            /*------------------------------------------------------ popolo il combo box con le porte com attive ---------------------------------------------------*/
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBoxComP.Items.Add(s);
            }

            /*------------------------------------------------ seleziono di default come connessione l'ultima COM port ---------------------------------------------*/
            comboBoxComP.SelectedIndex = comboBoxComP.Items.Count - 1;

            /*---------------------------------------------------------- ottengo il controllo del puntatore --------------------------------------------------------*/
            this.Cursor = new Cursor(Cursor.Current.Handle);
        }

        private void buttonA_Click(object sender, EventArgs e)             // bottone ON
        {
            port.Write("1");
        }

        private void buttonB_Click(object sender, System.EventArgs e)      // bottone OFF
        {
            port.Write("0");
        }

        private void labelReceived_Click(object sender, System.EventArgs e)
        {

        }

        /*--------------------------------------------------------- bottone per attivare la modalità di scolto ----------------------------------------------------*/

        private void buttonListen_Click(object sender, System.EventArgs e)
        {
            int x = 0;
            textBoxReceived.Text = "";

            while (x != 5)
            {
                /*------------------------------ controllo se l'arduino risponde qualcosa ----------------------------------------*/
                while (port.BytesToRead <= 0) { }
                String myVar = port.ReadLine();

                switch (myVar)
                {
                    case "Right\r":
                        Cursor.Position = new Point(Cursor.Position.X + 10, Cursor.Position.Y);
                        break;

                    case "Left\r":
                        Cursor.Position = new Point(Cursor.Position.X -10, Cursor.Position.Y);
                        break;

                    case "Up\r":
                        Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 10);
                        break;

                    case "Down\r":
                        Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 10);
                        break;

                    case "Click\r":
                        LeftMouseClick();
                        break;

                    case "Off\r":
                        x = 5;
                        break;

                    default:
                        break;
                }
                textBoxReceived.Text += myVar + '\n';
            }
        }

        public void LeftMouseClick()
        {
            //Call the imported function with the cursor's current position

            uint X =(uint) Cursor.Position.X;
            uint Y = (uint) Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        /*----------------------------------------------------------eseguo la connessione nel caso in cui venga cambiata la porta da utilizzare ----------------------------------------------*/
 
        private void comboBoxComP_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            /*----------------------------------------- attivo i controlli solo se tutto è andato a buon fine  ----------------------------------*/
            
            String selected = comboBoxComP.SelectedItem.ToString();
            port = new SerialPort(selected, 57600);
            try
            {
                //un-comment this line to cause the arduino to re-boot when the serial connects
                //port.DtrEnabled = true;

                port.Open();                            // apro la porta

                buttonA.Enabled = true;                 // 
                buttonB.Enabled = true;                 // attivo i bottoni 
                buttonListen.Enabled = true;            //
            }
            catch (Exception ex)
            {
                //alert the user that we could not connect to the serial port
                MessageBox.Show("Error connecting to the COM port");
            }
        }
    }
}
