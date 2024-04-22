using AutoStrongDesk.Dto;
using AutoStrongDesk.Interfaces;
using AutoStrongDesk.Models;
using AutoStrongDesk.Utils;
using System.Windows.Media.Imaging;

namespace AutoStrongDesk.Services
{
    public class PostService(IPostsClient postsClient) : IPostService
    {
        public async Task CreatePost(BitmapImage image, string text)
        {
            PostDto postDto = new()
            {
                Image = await ImageUtils.ImageToBytes(image),
                Text = text
            };

            await postsClient.CreatePost(postDto);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await postsClient.GetPosts();
        }
    }
}
