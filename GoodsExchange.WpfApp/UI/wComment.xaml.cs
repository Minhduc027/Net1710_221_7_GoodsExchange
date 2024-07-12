using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using GoodsExchange.business;
using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;

namespace GoodsExchange.WpfApp.UI
{
    public partial class wComment : Window
    {
        private readonly ICommentBusiness _commentBusiness;
        private readonly IPostBusiness _postBusiness;
        private readonly ICustomerBusiness _customerBusiness;

        public List<Post> AvailablePosts { get; set; }
        public List<Customer> AvailableCustomers { get; set; }

        public wComment()
        {
            _commentBusiness = new CommentBusiness();
            _postBusiness = new PostBusiness(); 
            _customerBusiness = new CustomerBusiness(); 
            InitializeComponent();
            DataContext = this;
            LoadGrd();
            LoadCombos();
        }

        private async void LoadCombos()
        {
            var postResult = await _postBusiness.GetAll();
            if (postResult.Data != null)
            {
                AvailablePosts = postResult.Data as List<Post>;
            }
            else
            {
                AvailablePosts = new List<Post>();
            }

            var customerResult = await _customerBusiness.GetAllCustomer();
            if (customerResult.Data != null)
            {
                AvailableCustomers = customerResult.Data as List<Customer>;
            }
            else
            {
                AvailableCustomers = new List<Customer>();
            }

            cmbPostId.ItemsSource = AvailablePosts;
            cmbCustomerId.ItemsSource = AvailableCustomers;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Comment comment = new Comment
                {
                    Title = txtTitle.Text,
                    Content = txtContent.Text,
                    DateTime = DateTime.Now,
                    CustomerId = (int)cmbCustomerId.SelectedValue,
                    PostId = (int)cmbPostId.SelectedValue,
                };
                var createResult = await _commentBusiness.CreateComment(comment);
                if (createResult.Status > 0)
                {
                    MessageBox.Show("Create Successfully!", "Save");
                }
                else
                {
                    MessageBox.Show(createResult.Message, "Save Failed");
                }

                ClearForm();
                LoadGrd();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid values for CustomerId and PostId.", "Invalid Input");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ClearForm()
        {
            txtCommentId.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtContent.Text = string.Empty;
            cmbPostId.SelectedValue = null;
            cmbCustomerId.SelectedValue = null;
        }

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            ButtonUpdate.IsEnabled = false;
            ButtonSave.IsEnabled = true;
        }

        private async void grdPost_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            var selectedComment = grdPost.SelectedItem as Comment;
            if (selectedComment != null)
            {
                txtCommentId.Text = selectedComment.CommentId.ToString();
                txtTitle.Text = selectedComment.Title;
                txtContent.Text = selectedComment.Content;
                cmbPostId.SelectedValue = selectedComment.PostId;
                cmbCustomerId.SelectedValue = selectedComment.CustomerId;

                ButtonUpdate.IsEnabled = true;
                ButtonSave.IsEnabled = false;
            }
        }

        private async void grdPost_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                int postId = (int)button.CommandParameter;
                var result = await _commentBusiness.DeleteConmment(postId);
                if (result.Data != null)
                {
                    MessageBox.Show(result.Message, "Delete!");
                    LoadGrd();
                }
            }
            LoadGrd();
        }

        private async void LoadGrd()
        {
            var result = await _commentBusiness.GetAllComments();
            if (result.Data != null)
            {
                grdComment.ItemsSource = result.Data as List<Comment>;
            }
            else
            {
                grdComment.ItemsSource = new List<Comment>();
            }
        }

        private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = await _commentBusiness.GetById(int.Parse(txtCommentId.Text));
                if (item.Data != null)
                {
                    var comment = item.Data as Comment;
                    comment.Content = txtContent.Text;
                    comment.Title = txtTitle.Text;
                    comment.PostId = (int)cmbPostId.SelectedValue;
                    comment.CustomerId = (int)cmbCustomerId.SelectedValue;

                    var result = await _commentBusiness.UpdateComment(comment);
                    MessageBox.Show(result.Message, "Update");

                    ClearForm();

                    ButtonUpdate.IsEnabled = false;
                    ButtonSave.IsEnabled = true;

                    LoadGrd();
                }
                else
                {
                    MessageBox.Show("Comment not found!", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
