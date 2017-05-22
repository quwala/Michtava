﻿namespace Services
{
    using System;
    using System.Linq;
    using Dal.Repositories.Interfaces;
    using Entities.Models;
    using Services.Interfaces;
    using Common;

    public class HomeworkService : IHomeworkService
    {
        private readonly IHomeworkRepository homeworkRepository;

        public HomeworkService(IHomeworkRepository homeworkRepository)
        {
            this.homeworkRepository = homeworkRepository;
        }

        Homework IHomeworkService.GetById(Guid id)
        {
            return this.homeworkRepository.GetById(id);
        }

        IQueryable<Homework> IRepositoryService<Homework>.All()
        {
            return this.homeworkRepository.All();
        }

        public MichtavaResult Add(Homework homework)
        {
            this.homeworkRepository.Add(homework);
            this.homeworkRepository.SaveChanges();
            return new MichtavaSuccess();

        }

        public MichtavaResult Update(Homework homework)
        {
            this.homeworkRepository.Update(homework);
            this.homeworkRepository.SaveChanges();
            return new MichtavaSuccess();

        }

        public MichtavaResult Delete(Homework homework)
        {

            this.homeworkRepository.Delete(homework);
            this.homeworkRepository.SaveChanges();
            return new MichtavaSuccess();

        }
    }
}
