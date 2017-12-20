using LottoWPF.Model;
using LottoWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LottoWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private LottoGameModel _model;
        private LottoViewModel _viewModel;

        private MainWindow _view;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _viewModel = new LottoViewModel();
            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Show();
        }



    }
}
