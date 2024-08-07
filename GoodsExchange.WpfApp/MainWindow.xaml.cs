﻿using GoodsExchange.WpfApp.UI;
using System.Windows;

namespace GoodsExchange.WpfApp
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
        private async void Open_wPosts_Click(object sender, RoutedEventArgs e)
        {
            var p = new wPost();
            p.Owner = this;
            p.Show();
        }

        private void Open_wOffers_Click(object sender, RoutedEventArgs e)
        {
            var o = new wOffer();
            o.Owner = this;
            o.Show();
        }
        private async void Open_wCustomers_Click(object sender, RoutedEventArgs e)
        {
            var p = new wCustomer();
            p.Owner = this;
            p.Show();
        }

        private async void Open_wComment_Click(object sender, RoutedEventArgs e)
        {
            var p = new wComment();
            p.Owner = this;
            p.Show();
        }
    }
}
