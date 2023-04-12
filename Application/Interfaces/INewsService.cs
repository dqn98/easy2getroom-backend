using BE.Data.Entities;
using BE.Ultilities.Dtos;
using System;
using System.Collections.Generic;

namespace BE.Application.Interfaces
{
    public interface INewsService : IDisposable
    {
        List<News> GetAll();

        News GetById(int id);

        PagedResult<News> GetAllPaging(string keyWord, int page, int pageSize);

        List<News> GetLastestNews(int top);

        News Add(News news);

        void Update(News news);

        void Delete(int id);
    }
}