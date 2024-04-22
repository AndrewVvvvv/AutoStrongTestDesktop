using AutoStrongDesk.Dto;
using AutoStrongDesk.Models;
using Refit;

namespace AutoStrongDesk.Interfaces
{
    public interface IPostsClient
    {
        [Get("/posts")]
        Task<IEnumerable<Post>> GetPosts();

        [Post("/post")]
        Task CreatePost([Body] PostDto postDto);
    }
}
