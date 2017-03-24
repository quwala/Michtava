﻿namespace Services.Interfaces
{
    using System;
    using System.Linq;

    public interface IRepositoryService<T>
    {
        IQueryable<T> All();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}