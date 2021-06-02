using HotelGUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelGUI
{
    public class MainWindowViewModel : BindableBase
    {      
        private HomeViewModel homeViewModel = new HomeViewModel();
        private BindableBase currentViewModel;

        public MainWindowViewModel()
        {            
            CurrentViewModel = homeViewModel;
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }
    }
}
