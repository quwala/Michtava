﻿namespace Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SchoolClass : DeletableEntity
    {
        private List<Student> students;
        private List<Teacher> teachers;

        public SchoolClass()
        {
            this.students = new List<Student>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ClassLetter { get; set; }

        public virtual Grade Grade { get; set; }

        public int GradeId { get; set; }


        public virtual List<Student> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }

        public virtual List<Teacher> Teachers
        {
            get { return this.teachers; }
            set { this.teachers = value; }
        }
    }
}
