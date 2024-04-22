using AutoStrongDesk.Interfaces;
using AutoStrongDesk.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AutoStrongDesk.ViewModels
{
    public class PostsViewModel : ObservableObject
    {
        private readonly IPostService postsService;

        private ObservableCollection<Post> _posts = [];
        public ObservableCollection<Post> Posts
        {
            get => _posts;
            set => SetProperty(ref _posts, value);
        }

        private BitmapImage? _image;
        public BitmapImage? Image
        {
            get => _image;
            set
            {
                SetProperty(ref _image, value);
                RefreshCanSavePost();
            }
        }

        private string? _text;
        public string? Text
        {
            get => _text;
            set
            {
                SetProperty(ref _text, value);
                RefreshCanSavePost();
            }
        }

        private bool _canSavePost;
        public bool CanSavePost
        {
            get => _canSavePost;
            set => SetProperty(ref _canSavePost, value);
        }

        public IRelayCommand UploadImageCommand { get; }

        public IAsyncRelayCommand SavePostCommand { get; }

        public PostsViewModel()
        {
            postsService = App.Current.Services.GetService<IPostService>()!;
            UploadImageCommand = new RelayCommand(UploadImage);
            SavePostCommand = new AsyncRelayCommand(SaveImage);
        }

        public async Task LoadPosts()
        {
            var posts = await postsService.GetPosts();
            Posts = new ObservableCollection<Post>(posts);
        }

        private void UploadImage()
        {
            OpenFileDialog fileDialog = new()
            {
                Title = "Select a image",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "Portable Network Graphic (*.png)|*.png"
            };

            var dialogOpened = fileDialog.ShowDialog();
            if (!dialogOpened.HasValue || !dialogOpened.Value)
            {
                return;
            }

            FileInfo imageInfo = new(fileDialog.FileName);
            _ = int.TryParse(ConfigurationManager.AppSettings["MaxFileSize"], out int maxFileSize);
            if (imageInfo.Length > maxFileSize)
            {
                MessageBox.Show("Max file size is 5 MB");
                return;
            }

            Image = new BitmapImage(new Uri(fileDialog.FileName));
        }

        private async Task SaveImage()
        {
            StringBuilder messageBuilder = new();
            if (Image == null)
            {
                messageBuilder.AppendLine("You should specify file.");
            }

            if (string.IsNullOrWhiteSpace(Text))
            {
                messageBuilder.AppendLine("You should type text.");
            }

            var message = messageBuilder.ToString();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
            }

            await postsService.CreatePost(Image!, Text!);
            await LoadPosts();
        }

        private void RefreshCanSavePost()
        {
            CanSavePost = Image != null && !string.IsNullOrEmpty(Text) && Text.Length > 0;
        }
    }
}
