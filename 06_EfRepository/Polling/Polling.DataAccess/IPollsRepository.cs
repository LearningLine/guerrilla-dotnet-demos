using System;
using System.Collections.Generic;
using Polling.Entities;

namespace Polling.DataAccess
{
    public interface IPollsRepository : IRepository<Poll>
    {
        void AddVote(Vote vote);
    }

    public interface IRepository<T> : IDisposable
    {
        void Add(T item);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Remove(T poll);
        void Save();
    }
}