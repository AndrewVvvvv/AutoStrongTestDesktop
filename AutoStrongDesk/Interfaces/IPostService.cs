using AutoStrongDesk.Dto;
using AutoStrongDesk.Models;
using System.Windows.Media.Imaging;

namespace AutoStrongDesk.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPosts();

        Task CreatePost(BitmapImage bitmapImage, string text);
    }
}
