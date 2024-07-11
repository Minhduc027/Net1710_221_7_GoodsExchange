using System.Drawing.Design;
using System.Windows;
using System.Windows.Controls;
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
        private readonly ICategoryBusiness _categoryBusiness;
        public wPost()
        {
            _categoryBusiness = new CategoryBusiness();
            _postBusiness = new PostBusiness();
            InitializeComponent();
            this.LoadGrd();

        }
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Post post = new Post
                {
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    Address = txtAddress.Text,
                    CategoryId = int.Parse(txtCategoryId.Text),
                    PostOwnerId = int.Parse(txtPostOwnerId.Text),
                    CreateDate = DateTime.Now
                };

                if (!string.IsNullOrWhiteSpace(txtPostId.Text))
                {
                    post.PostId = int.Parse(txtPostId.Text);
                    var updateResult = await _postBusiness.Update(post);
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
                    var createResult = await _postBusiness.Create(post);
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
                MessageBox.Show("Please enter valid values for CategoryId and PostOwnerId.", "Invalid Input");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void ClearForm()
        {
            txtTitle.Text = string.Empty;
            txtPostOwnerId.Text = string.Empty;
            txtPostId.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtCategoryId.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }
        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            this.Close();
        }
        private async void grdPost_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            string CategoryName = null;
            var selectedPost = grdPost.SelectedItem as Post;
            var category = await _categoryBusiness.GetAllCategory();
            var categoryList = (List<Category>) category.Data;
            foreach(var item in categoryList) {
                if(item.CategoryId == selectedPost.CategoryId)
                {
                    CategoryName = item.CategoryName;
                }
            }
            if (selectedPost != null)
            {
                // Cập nhật các trường tương ứng
                txtPostId.Text = selectedPost.PostId.ToString();
                txtTitle.Text = selectedPost.Title;
                txtDescription.Text = selectedPost.Description;
                txtCategoryId.Text = selectedPost.CategoryId.ToString(); //CategoryName; //
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
