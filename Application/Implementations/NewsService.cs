using BE.Application.Interfaces;
using BE.Data.Entities;
using BE.Data.IRepositories;
using BE.Infrastructure.Interfaces;
using BE.Ultilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BE.Application.Implementations
{
    public class NewsService : INewsService
    {
        private INewsRepository _newsRepository;
        private IUnitOfWork _unitOfWork;

        public NewsService(INewsRepository newsRepository, IUnitOfWork unitOfWork)
        {
            _newsRepository = newsRepository;
            _unitOfWork = unitOfWork;
        }

        public News Add(News news)
        {
            _newsRepository.Add(news);
            _unitOfWork.Commit();
            return news;
        }

        public void Delete(int id)
        {
            _newsRepository.Remove(id);
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<News> GetAll()
        {
            return _newsRepository.FindAll(x => x.NewsImages, x => x.User).ToList();
        }

        public PagedResult<News> GetAllPaging(string keyWord, int page, int pageSize)
        {
            var query = _newsRepository.FindAll(x => x.User);
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x => x.Title.Contains(keyWord) || x.Content.Contains(keyWord));
            }

            int totalRow = query.Count();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var data = query.ToList();

            var paginationSet = new PagedResult<News>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public News GetById(int id)
        {
            return _newsRepository.FindById(id, x => x.User);
        }

        public List<News> GetLastestNews(int top)
        {
            var data = _newsRepository.FindAll(x => x.NewsImages, x => x.User).Take(top).ToList();
            return data;
        }

        public void Update(News news)
        {
            _newsRepository.Update(news);
            _unitOfWork.Commit();
        }
    }
}