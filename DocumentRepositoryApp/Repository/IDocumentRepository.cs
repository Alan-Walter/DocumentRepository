using DocumentRepositoryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentRepositoryApp.Repository
{
    public interface IDocumentRepository : IDisposable
    {
        void Add(int authorId, string fileName, string filePath);
        IEnumerable<Document> GetAll();
    }
}