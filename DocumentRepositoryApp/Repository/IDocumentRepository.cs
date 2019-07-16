using DocumentRepositoryApp.Models;
using System;
using System.Collections.Generic;

namespace DocumentRepositoryApp.Repository
{
    public interface IDocumentRepository : IDisposable
    {
        void Add(int authorId, string fileName, string filePath);
        IEnumerable<Document> Find(string data);
        IEnumerable<Document> GetAll();
    }
}