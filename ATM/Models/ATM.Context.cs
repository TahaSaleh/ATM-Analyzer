﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ATM.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ATMEntities : DbContext
    {
        public ATMEntities()
            : base("name=ATMEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Trunsaction> Trunsactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ATM> ATMs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}