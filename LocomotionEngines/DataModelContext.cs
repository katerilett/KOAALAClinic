using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataModel.Class;

namespace LocomotionEngines
{
	/// <summary>
	/// Connects to a database, allowing access and modification of the model within.
	/// </summary>
	public class DataModelContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<Patient> Patients { get; set; }

		public DataModelContext()
			: base("Server=koaaladb.c7v1hrkwxvoj.us-east-1.rds.amazonaws.com,1433;Database=koaaladb;User Id=katerilett;password=koaalaclinic")
		{ }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			#region User
			// Make both username and id keys, with id primary.
			modelBuilder.Entity<User>().HasKey(u => new { ID=u.ID, u.Username });
			#endregion

		}
	}
}
