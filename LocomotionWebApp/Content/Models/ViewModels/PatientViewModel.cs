using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel.Class;

namespace LocomotionWebApp.Models.ViewModels
{
	public class PatientViewModel
	{
		public long ID { get; set; }
		
		public String FirstName { get; set; }

		public String LastName { get; set; }
		
		public bool OutOfDate { get; set; }

		public virtual User Therapist { get; set; }

		public string Doctor { get; set; }

		public DateTime LastUpdate { get; set; } //Currently being treated as LastEdit i.e. Start

		public string ArthritisType { get; set; }

		public string AffectedExtremity { get; set; }

		public string Deformity { get; set; }

		public double ShankLength { get; set; }

		public double ThighLength { get; set; }

		public int Age { get; set; }

		public DateTime Birthday { get; set; }

		public string BirthdayString { get; set; }

		public string BirthdayHtml { get; set; }

		public string Gender { get; set; }

		public double Height { get; set; }

		public double Weight { get; set; }

		public DateTime Start { get; set; } //When Therapists adds the patient

		public string StartString { get; set; }

		public string PhoneNumber { get; set; }

		public string Email { get; set; }

		public string City { get; set; }

		public string ContactName { get; set; }

		public string ContactRelation { get; set; }

		public string ContactPhoneNumber { get; set; }

		public string ContactEmail { get; set; }

		public Report Report { get; set; }

		public MedProfile MedProfile { get; set; }

	}
}