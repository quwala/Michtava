﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities.Models
{
    public class Answer
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime Date_Submitted { get; set; }

        public Homework Answer_To { get; set; }

        public Student Submitted_By { get; set; }

    }
}