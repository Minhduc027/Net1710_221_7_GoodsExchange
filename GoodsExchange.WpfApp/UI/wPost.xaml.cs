using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using GoodsExchange.business;
using GoodsExchange.business.Interface;
using GoodsExchange.data.Models;
namespace GoodsExchange.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wPost.xaml
    /// </summary>
    public partial class wPost : Window
    {
        private readonly IPostBusiness _postBusiness;
        public wPost()
        {
            _postBusiness = new PostBusiness();
            InitializeComponent();
            this.LoadGrd();

        }
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tmpId = -1;
                Post post;

                if (!string.IsNullOrWhiteSpace(txtPostId.Text))
                {
                    var item = await _postBusiness.GetById(int.Parse(txtPostId.Text));
                    if (item.Data != null)
                    {
                        post = item.Data as Post;
                        post.Title = txtTitle.Text;
                        post.Description = txtDescription.Text;
                        post.Address = txtAddress.Text;
                        post.CategoryId = int.Parse(txtCategoryId.Text);
                        post.PostOwnerId = int.Parse(txtPostOwnerId.Text);
                        post.CreateDate = DateTime.Now;

                        var result = await _postBusiness.Update(post);
                        MessageBox.Show(result.Message, "Update");
                    }
                    else
                    {
                        post = new Post
                        {
                            Title = txtTitle.Text,
                            Description = txtDescription.Text,
                            Address = txtAddress.Text,
                            CategoryId = int.Parse(txtCategoryId.Text),
                            PostOwnerId = int.Parse(txtPostOwnerId.Text),
                            CreateDate = DateTime.Now
                        };
                        var result = await _postBusiness.Create(post);
                        MessageBox.Show(result.Message, "Save");
                    }
                }
                else
                {
                    post = new Post
                    {
                        Title = txtTitle.Text,
                        Description = txtDescription.Text,
                        Address = txtAddress.Text,
                        CategoryId = int.Parse(txtCategoryId.Text),
                        PostOwnerId = int.Parse(txtPostOwnerId.Text),
                        CreateDate = DateTime.Now
                    };
                    var result = await _postBusiness.Create(post);
                    MessageBox.Show(result.Message, "Save");
                }

                txtTitle.Text = string.Empty;
                txtPostOwnerId.Text = string.Empty;
                txtPostId.Text = string.Empty;
                txtDescription.Text = string.Empty;
                txtCategoryId.Text = string.Empty;
                txtAddress.Text = string.Empty;
                this.LoadGrd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = string.Empty;
            txtPostOwnerId.Text = string.Empty;
            txtPostId.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtCategoryId.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }
        private async void grdPost_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            var selectedPost = grdPost.SelectedItem as Post;

            if (selectedPost != null)
            {
                // Cập nhật các trường tương ứng
                txtPostId.Text = selectedPost.PostId.ToString();
                txtTitle.Text = selectedPost.Title;
                txtDescription.Text = selectedPost.Description;
                txtCategoryId.Text = selectedPost.CategoryId.ToString();
                txtPostOwnerId.Text = selectedPost.PostOwnerId.ToString();
                txtAddress.Text = selectedPost.Address;
            }
        }
        private async void grdPost_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                int postId = (int) button.CommandParameter;
                var result = await _postBusiness.Delete(postId);
                if(result.Data != null)
                {
                    MessageBox.Show(result.Message, "Delete!");
                    this.LoadGrd();
                }
            }
        }
        private async void LoadGrd()
        {
            var result = await _postBusiness.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdPost.ItemsSource = result.Data as List<Post>;
            }
            else
            {
                grdPost.ItemsSource = new List<Post>();
            }
        }
    }
}
