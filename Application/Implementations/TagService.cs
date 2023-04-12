using BE.Application.Interfaces;
using System;

namespace BE.Application.Implementations
{
    public class TagService : ITagService
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}