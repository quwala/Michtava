﻿namespace Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SchoolClass : DeletableEntity
    {
        

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ClassLetter { get; set; }

        public int GradeYear { get; set; }

        public virtual List<Student> Students
        {
            get { return this.Students; }
            set { this.Students = value; }
        }

        public virtual List<Subject> Subjects
        {
            get { return this.Subjects; }
            set { this.Subjects = value; }
        }

        public virtual List<Homework> ActiveHomeworks
        {
            get { return this.ActiveHomeworks; }
            set { this.ActiveHomeworks = value; }
        }

    }
}
