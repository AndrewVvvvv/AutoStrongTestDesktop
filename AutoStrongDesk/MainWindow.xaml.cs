using AutoStrongDesk.ViewModels;
using System.Windows;

namespace AutoStrongDesk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await((PostsViewModel)DataContext).LoadPosts();
        }
    }
}