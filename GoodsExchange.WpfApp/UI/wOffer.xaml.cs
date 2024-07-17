using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using GoodsExchange.business;
using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;

namespace GoodsExchange.WpfApp.UI
{
    public partial class wOffer : Window
    {
        private readonly IOfferBusiness _offerBusiness;

        public wOffer()
        {
            _offerBusiness = new OfferBusiness();
            InitializeComponent();
            this.LoadGrd();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Offer offer;

                if (!string.IsNullOrWhiteSpace(txtOfferId.Text))
                {
                    var item = await _offerBusiness.GetById(int.Parse(txtOfferId.Text));
                    if (item.Data != null)
                    {
                        offer = item.Data as Offer;
                        offer.CustomerId = int.Parse(txtCustomerId.Text);
                        offer.IsApproved = chkIsApproved.IsChecked ?? false;
                        offer.OfferDate = DateTime.Now;

                        var result = await _offerBusiness.Update(offer);
                        MessageBox.Show(result.Message, "Update");
                    }
                    else
                    {
                        offer = new Offer
                        {
                            CustomerId = int.Parse(txtCustomerId.Text),
                            IsApproved = chkIsApproved.IsChecked ?? false,
                            OfferDate = DateTime.Now
                        };
                        var result = await _offerBusiness.Create(offer);
                        MessageBox.Show(result.Message, "Save");
                    }
                }
                else
                {
                    offer = new Offer
                    {
                        CustomerId = int.Parse(txtCustomerId.Text),
                        IsApproved = chkIsApproved.IsChecked ?? false,
                        OfferDate = DateTime.Now
                    };
                    var result = await _offerBusiness.Create(offer);
                    MessageBox.Show(result.Message, "Save");
                }

                txtCustomerId.Text = string.Empty;
                txtOfferId.Text = string.Empty;
                chkIsApproved.IsChecked = false;

                ButtonUpdate.IsEnabled = false;
                ButtonSave.IsEnabled = true;

                this.LoadGrd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtOfferId.Text))
                {
                    var item = await _offerBusiness.GetById(int.Parse(txtOfferId.Text));
                    if (item.Data != null)
                    {
                        var offer = item.Data as Offer;
                        offer.CustomerId = int.Parse(txtCustomerId.Text);
                        offer.IsApproved = chkIsApproved.IsChecked ?? false;
                        offer.OfferDate = DateTime.Now;

                        var result = await _offerBusiness.Update(offer);
                        MessageBox.Show(result.Message, "Update");

                        txtCustomerId.Text = string.Empty;
                        txtOfferId.Text = string.Empty;
                        chkIsApproved.IsChecked = false;

                        ButtonUpdate.IsEnabled = false;
                        ButtonSave.IsEnabled = true;

                        this.LoadGrd();
                    }
                    else
                    {
                        MessageBox.Show("Offer not found!", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void grdOffer_MouseDouble_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var offer = grdOffer.SelectedItem as Offer;
            if (offer != null)
            {
                var item = await _offerBusiness.GetById(offer.OfferId);
                if (item.Data != null)
                {
                    txtOfferId.Text = offer.OfferId.ToString();
                    txtCustomerId.Text = offer.CustomerId.ToString();
                    chkIsApproved.IsChecked = offer.IsApproved;
                }
                else
                {
                    MessageBox.Show("Offer not found!", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void grdOffer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                int offerId = (int)button.CommandParameter;
                var result = await _offerBusiness.Delete(offerId);
                if (result.Status >= 0)
                {
                    MessageBox.Show(result.Message, "Delete!");
                    this.LoadGrd();
                }
                else
                {
                    MessageBox.Show("Error: " + result.Message, "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void LoadGrd()
        {
            var result = await _offerBusiness.GetAll();
            if (result.Status >= 0 && result.Data != null)
            {
                grdOffer.ItemsSource = result.Data as List<Offer>;
            }
            else
            {
                grdOffer.ItemsSource = new List<Offer>();
            }
        }

        private async void grdOffer_ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                int offerId = (int)button.CommandParameter;
                var item = await _offerBusiness.GetById(offerId);
                if (item.Data != null)
                {
                    var offer = item.Data as Offer;
                    if (offer != null)
                    {
                        txtOfferId.Text = offer.OfferId.ToString();
                        txtCustomerId.Text = offer.CustomerId.ToString();
                        chkIsApproved.IsChecked = offer.IsApproved;

                        ButtonUpdate.IsEnabled = true;
                        ButtonSave.IsEnabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Offer not found!", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
