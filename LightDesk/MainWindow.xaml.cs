using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LightDesk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort port;
        public MainWindow()
        {
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = port;
                COMSelectionBox.Items.Add(item);
            }
        }

        private void COMConnectButton_Click(object sender, RoutedEventArgs e)
        {
            COMConnectButton.IsEnabled = false;
            COMConnectButton.Content = "Connected";
            ComboBoxItem selected = (ComboBoxItem) COMSelectionBox.SelectedItem;
            port = new SerialPort(selected.Content.ToString(), 9600);
            port.Open();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(port != null)
            {
                port.Close();

            }
        }

        private void RedButton_Click(object sender, RoutedEventArgs e)
        {
            port.Write("001:000\r");
            port.Write("002:000\r");
            port.Write("000:254\r");
           

        }

        private void GreenButton_Click(object sender, RoutedEventArgs e)
        {
            port.Write("001:254\r");
            port.Write("000:000\r");
            port.Write("002:000\r");
        }

        private void BlueButton_Click(object sender, RoutedEventArgs e)
        {
            port.Write("002:254\r");
            port.Write("000:000\r");
            port.Write("001:000\r");
        }

        private void FadeButton_Click(object sender, RoutedEventArgs e)
        {
            for(int x = 255; x > 0; x--)
            {
                port.Write("000:" + x.ToString("000") + "\r");
                this.Title = x.ToString("000");
            }
            for (int x = 0; x < 255; x++)
            {
                this.Title = x.ToString("000");

                port.Write("000:" + x.ToString("000") + "\r");

            }
        }
    }
}
