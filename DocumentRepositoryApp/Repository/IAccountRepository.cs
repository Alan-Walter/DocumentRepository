using DocumentRepositoryApp.Models;
using System;

namespace DocumentRepositoryApp.Repository
{
    public interface IAccountRepository : IDisposable
    {
        void Add(Account item);
        bool Contains(Account item);
        void Remove(Account item);
        int Count();
        Account GetByLogin(string login);
    }
}