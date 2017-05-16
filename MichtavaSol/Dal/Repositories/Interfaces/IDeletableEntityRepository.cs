﻿namespace Dal.Repositories.Interfaces
{
    using Common;
    using System.Linq;

    public interface IDeletableEntityRepository<T> : IGenericRepository<T> where T : class
    {
        IQueryable<T> AllWithDeleted();

        void HardDelete(T entity);


        T GetByTestID(int Id);
    }
}
