using Picabu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picabu.Services
{
    public interface ILikeService
    {
        Task AddLikeAsync(Like like);
        Task EditLikeAsync(Like like);
        Task DeleteLikeAsync(News news);
        bool PutLikeOnPost(int newsId,string userId);
    }
}
