using ComicsLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsLibrary.Services.Interfaces
{
    public interface IComicService
    {
        IEnumerable<Comic> GetAll();
        Comic GetById(int id);

        void Add(Comic newComic);
        string GetTitle(int id);
        string GetIssue(int id);
        string GetPublisher(int id);
        string GetEditor(int id);
        string GetGcin(int id);

        LibraryLocation GetComicLocation(int id);
    }
}
