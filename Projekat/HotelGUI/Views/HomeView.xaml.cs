using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace HotelGUI.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "hotel":
                    e.Cancel = true;
                    break;
                case "vlasnik":
                    e.Cancel = true;
                    break;
                case "menadzer":
                    e.Cancel = true;
                    break;
                case "drzi":
                    e.Cancel = true;
                    break;
                case "restoran":
                    e.Cancel = true;
                    break;
                case "soba":
                    e.Cancel = true;
                    break;
                case "radnik":
                    e.Cancel = true;
                    break;
                case "gost":
                    e.Cancel = true;
                    break;
                case "recepcionar":
                    e.Cancel = true;
                    break;
                case "spremacica":
                    e.Cancel = true;
                    break;
                case "konobar":
                    e.Cancel = true;
                    break;
                case "odseda":
                    e.Cancel = true;
                    break;
            }
        }
    }
}
