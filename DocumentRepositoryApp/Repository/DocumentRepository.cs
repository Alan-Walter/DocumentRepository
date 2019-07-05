using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentRepositoryApp.Models;
using NHibernate;

namespace DocumentRepositoryApp.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        ISession Session => MvcApplication.SessionFactory.GetCurrentSession();

        public void Add(Document item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Document> GetAll()
        {
            using (var transtaction = Session.BeginTransaction())
            {
                var result = Session.QueryOver<Document>().Fetch(i=> i.Author).Eager.List();
                transtaction.Commit();
                return result;
            }
        }
    }
}