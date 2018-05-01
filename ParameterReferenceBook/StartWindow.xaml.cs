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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ParameterReferenceBook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private DispatcherTimer _dTimerShow;
        private DispatcherTimer _dTimerHide;
        private Int16 _delayTime;

        public StartWindow()
        {
            InitializeComponent();
            Opacity = 0; _delayTime = 0;

            _dTimerShow = new DispatcherTimer();
            _dTimerShow.Tick += new EventHandler(dTimerShow_Tick);
            _dTimerShow.Interval = TimeSpan.FromSeconds(0.1);
            _dTimerShow.Start();
        }

        private void dTimerShow_Tick(object sender, EventArgs e)
        {
            if (Opacity <= 1)
            {
                Opacity += 0.15;
                return;
            }

            Opacity = 1;
            _dTimerShow.Stop();
        }

        private void dTimerHide_Tick(object sender, EventArgs e)
        {
            if (_delayTime < 1)
            {
                _delayTime += 1;
                return;
            }

            _dTimerHide.Stop();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            _dTimerHide = new DispatcherTimer();
            _dTimerHide.Tick += new EventHandler(dTimerHide_Tick);
            _dTimerHide.Interval = TimeSpan.FromSeconds(0.095);
            _dTimerHide.Start();
        }
    }
}