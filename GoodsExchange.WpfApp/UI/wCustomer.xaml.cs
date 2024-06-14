using System.Drawing.Design;
using System.Windows;
using System.Windows.Controls;
using GoodsExchange.business;
using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;

namespace GoodsExchange.WpfApp.UI
{
    public partial class wCustomer : Window
    {
        private readonly ICustomerBusiness _customerBusiness;

        public wCustomer()
        {
            _customerBusiness = new CustomerBusiness();
            InitializeComponent();
            this.LoadGrd();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = new Customer
                {
                    Name = txtName.Text,
                    Address = txtAddress.Text,
                    Dob = DateOnly.FromDateTime(datePickerDob.SelectedDate.Value),
                    Phone = txtPhone.Text,
                    Gender = Convert.ToInt32(cmbGender.SelectedValue),
                    Email = txtEmail.Text
                };

                if (!string.IsNullOrWhiteSpace(txtCustomerId.Text))
                {
                    customer.CustomerId = int.Parse(txtCustomerId.Text);
                    var updateResult = await _customerBusiness.UpdateCustomer(customer);
                    if (updateResult.Status > 0)
                    {
                        MessageBox.Show("Update Successfully!", "Update");
                    }
                    else
                    {
                        MessageBox.Show(updateResult.Message, "Update Failed");
                    }
                }
                else
                {
                    var createResult = await _customerBusiness.CreateCustomer(customer);
                    if (createResult.Status > 0)
                    {
                        MessageBox.Show("Create Successfully!", "Save");
                    }
                    else
                    {
                        MessageBox.Show(createResult.Message, "Save Failed");
                    }
                }

                ClearForm();
                this.LoadGrd();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid values.", "Invalid Input");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ClearForm()
        {
            txtCustomerId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            datePickerDob.SelectedDate = null;
            txtPhone.Text = string.Empty;
            cmbGender.SelectedIndex = -1;
            txtEmail.Text = string.Empty;
        }

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            this.Close();
        }

        private async void grdCustomer_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            var selectedCustomer = grdCustomer.SelectedItem as Customer;

            if (selectedCustomer != null)
            {
                txtCustomerId.Text = selectedCustomer.CustomerId.ToString();
                txtName.Text = selectedCustomer.Name;
                txtAddress.Text = selectedCustomer.Address;
                datePickerDob.SelectedDate = selectedCustomer.Dob.ToDateTime(TimeOnly.MinValue);
                txtPhone.Text = selectedCustomer.Phone;
                cmbGender.SelectedValue = selectedCustomer.Gender;
                txtEmail.Text = selectedCustomer.Email;
            }
        }

        private async void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                int customerId = (int)button.CommandParameter;
                var result = await _customerBusiness.DeleteCustomer(customerId);
                if (result.Data != null)
                {
                    MessageBox.Show(result.Message, "Delete!");
                    this.LoadGrd();
                }
            }
        }

        private async void LoadGrd()
        {
            var result = await _customerBusiness.GetAllCustomer();

            if (result.Status > 0 && result.Data != null)
            {
                grdCustomer.ItemsSource = result.Data as List<Customer>;
            }
            else
            {
                grdCustomer.ItemsSource = new List<Customer>();
            }
        }
    }
}
