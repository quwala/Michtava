﻿namespace Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dal.Repositories.Interfaces;
    using Entities.Models;
    using Services.Interfaces;
    using Common;

    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository teacherRepository;

        private readonly IApplicationUserRepository userRepository;

        public TeacherService(ITeacherRepository teacherRepository, IApplicationUserRepository userRepository)
        {
            this.teacherRepository = teacherRepository;
            this.userRepository = userRepository;
        }

        public IApplicationUserRepository UserRepository
        {
            get { return this.userRepository; }
        }

        public IQueryable<Teacher> All()
        {
            return this.teacherRepository.All();
        }


        public Task<Teacher> GetByUserName(string username)
        {
            return this.teacherRepository.GetByUserName(username);
        }

        public MichtavaResult Add(Teacher teacher)
        {
            this.teacherRepository.Add(teacher);
            this.teacherRepository.SaveChanges();
            return new MichtavaSuccess();
        }

        public MichtavaResult Update(Teacher teacher)
        {
            this.teacherRepository.Update(teacher);
            this.teacherRepository.SaveChanges();
            return new MichtavaSuccess();

        }

        public MichtavaResult Delete(Teacher teacher)
        {
            teacher.ApplicationUser.DeletedBy = teacher.DeletedBy;

            this.userRepository.Delete(teacher.ApplicationUser);
            this.teacherRepository.Delete(teacher);

            this.teacherRepository.SaveChanges();
            return new MichtavaSuccess();

        }

        public IQueryable<Teacher> SearchByName(string searchString)
        {
            return this.teacherRepository.SearchByName(searchString);
        }

        public bool IsUserNameUniqueOnEdit(Teacher teacher, string username)
        {
            return this.teacherRepository.IsUserNameUniqueOnEdit(teacher, username);
        }

        public Teacher GetById(Guid id)
        {
            return this.teacherRepository.GetById(id);
        }
    }
}
