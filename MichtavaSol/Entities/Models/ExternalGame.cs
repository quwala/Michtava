﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Entities.Models
{
    public class ExternalGame : HasId
    {

        public ExternalGame(string url, string name)
        {
            this.Url = url;
            this.Name = name;
        }

        

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }


    }
}