using System;
using System.Collections.Generic;
using System.Linq;
using DocumentRepositoryApp.Models;
using NHibernate;
using NHibernate.Linq;

namespace DocumentRepositoryApp.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        ISession Session => MvcApplication.SessionFactory.GetCurrentSession();

        public void Add(int authorId, string fileName, string filePath)
        {
            Session.CreateSQLQuery("exec AddDocument :authorId, :fileName, :filePath")
                .AddEntity(typeof(Document))
                .SetInt32("authorId", authorId)
                .SetString("fileName", fileName)
                .SetString("filePath", filePath)
                .ExecuteUpdate();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Document> Find(string data)
        {
            using (var transtaction = Session.BeginTransaction())
            {
                var result = Session.Query<Document>()
                    .Where(i => i.FileName.Contains(data)
                            || i.Author.Login.Contains(data)
                            || i.Date.ToString().Contains(data)).ToList();
                transtaction.Commit();
                return result;
            }
        }

        public IEnumerable<Document> GetAll()
        {
            using (var transtaction = Session.BeginTransaction())
            {
                var result = Session.Query<Document>().ToList();
                transtaction.Commit();
                return result;
            }
        }
    }
}