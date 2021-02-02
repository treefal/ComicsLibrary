using ComicsLibrary.Data;
using ComicsLibrary.Data.Models;
using ComicsLibrary.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComicsLibrary.Services
{
    public class ComicService : IComicService
    {
        private ComicsLibraryContext _context;

        public ComicService(ComicsLibraryContext context)
        {
            _context = context;
        }

        public void Add(Comic newComic)
        {
            _context.Add(newComic);
            _context.SaveChanges();
        }

        public IEnumerable<Comic> GetAll()
        {
            return _context.Comics
                .Include(comic => comic.Status)
                .Include(comic => comic.Location);
        }

        public Comic GetById(int id)
        {
            return GetAll()
                .FirstOrDefault(comic => comic.Id == id);
        }

        public LibraryLocation GetComicLocation(int id)
        {
            return GetById(id).Location;
        }

        public string GetEditor(int id)
        {
            return GetComic(id).Editor;
        }

        public string GetGcin(int id)
        {
            if (_context.Comics.Any(comic => comic.Id == id))
            {
                return GetComic(id).GCIN;
            }

            else return string.Empty;
        }

        public string GetIssue(int id)
        {
            return GetComic(id).Issue;
        }

        public string GetPublisher(int id)
        {
            return GetComic(id).Publisher;
        }

        public string GetTitle(int id)
        {
            return GetComic(id).Title;
        }

        private Comic GetComic(int id)
        {
            return _context.Comics.FirstOrDefault(comic => comic.Id == id);
        }
    }
}
