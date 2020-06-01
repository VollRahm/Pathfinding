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

namespace Pathfinding
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Vars.MainCanv = MainCanvas;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Vars.Engine.DrawNodes(new Point(1,1), new Point(40, 20));
        }

        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            await Vars.Engine.FindPath();
        }

        private void MainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(MainCanvas);
            Vars.Engine.ToggleWall(pos);
        }
    }
}
