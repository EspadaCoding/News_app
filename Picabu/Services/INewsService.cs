using Picabu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picabu.Services
{
    public interface INewsService
    {
        Task<News> FindNewsByIdAsync(int Id);
        Task<NewsResult> GetNewsAsync(int page = 1);
        Task<NewsResult> GetNewsAsync(int CategoryId, int page = 1); 
        Task AddNewsAsync(News news);
        Task EditNewsAsync(News news);
        Task DeleteNewsAsync(int Id);
        Task AddCategoryAsync(Category category);
        Task AddCommentAsync(Comment comment);

        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Comment>> GetComentariesAsync();
        Task<IEnumerable<Tag>> GetTagsAsync();
        Task UpdateTagsInNews(int newsId, int[] tags);
    }
}
