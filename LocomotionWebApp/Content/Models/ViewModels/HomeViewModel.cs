using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Class;

namespace LocomotionWebApp.Models.ViewModels
{
	public class HomeViewModel
	{
		public ICollection<Patient> Patients { get; set; }
	}
}