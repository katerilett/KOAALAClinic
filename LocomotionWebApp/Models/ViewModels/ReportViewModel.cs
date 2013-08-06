using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Class;
using LocomotionEngines;

namespace LocomotionWebApp.Models.ViewModels
{
	public class ReportViewModel
	{
		public long ID { get; set; }

		public String FirstName { get; set; }

		public String LastName { get; set; }

		public DateTime LastUpdate { get; set; }

		public int ShankLength { get; set; }

		public int ThighLength { get; set; }

		public Report Report { get; set; }
	}
}