﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Dal
{
    public class CustomerDal : DbContext    //inheret dbContext to use entityFramework
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder) //this connect between the db tables
                                                                             //and the models we got **if they dont
                                                                             //have the same name..
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("tblCustomers");         //here we connect them..
        }
        public DbSet<Customer> Customers { get; set; }

    }
}