using DocumentRepositoryApp.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentRepositoryApp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        ISession Session => MvcApplication.SessionFactory.GetCurrentSession();

        public void Add(Account item)
        {
            using (var transtaction = Session.BeginTransaction())
            {
                Session.Save(item);
                transtaction.Commit();
            }
        }

        public bool Contains(Account item)
        {
            using (var transtaction = Session.BeginTransaction())
            {
                bool result = Session.Contains(item);
                transtaction.Commit();
                return result;
            }
        }

        public int Count()
        {
            using (var transtaction = Session.BeginTransaction())
            {
                int result = Session.QueryOver<Account>().RowCount();
                transtaction.Commit();
                return result;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Account GetByLogin(string login)
        {
            using (var transtaction = Session.BeginTransaction())
            {
                var result = Session.QueryOver<Account>().Where(x => x.Login == login).SingleOrDefault();
                transtaction.Commit();
                return result;
            }
        }

        public void Remove(Account item)
        {
            using (var transtaction = Session.BeginTransaction())
            {
                Session.Delete(item);
                transtaction.Commit();
            }
        }
    }
}