using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Class
{
	
	public class Patient
	{
		[Key]
		public long ID { get; set; }

		// Change tracking variables
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public virtual User Therapist { get; set; }

		public string Doctor { get; set; }

		public DateTime LastUpdate { get; set; } //Currently being treated as Start

		public string ArthritisType { get; set; }

		public string AffectedExtremity { get; set; }

		public string Deformity { get; set; }

		public double ShankLength { get; set; }

		public double ThighLength { get; set; }

		public int Age { get; set; }

		public DateTime Birthday { get; set; }

		public string Gender { get; set; }

		public double Height { get; set; }

		public double Weight { get; set; }

		public DateTime Start { get; set; } //When Therapists adds the patient

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public string State { get; set; }

		public string City { get; set; }

		public string ContactName { get; set; }

		public string ContactRelation { get; set; }

		public string ContactPhoneNumber { get; set; }

		public string ContactEmail { get; set; }

		//Constructor (probably unncessary)
		public Patient() { }

		/// <summary>
		/// If a summary has been calculated, it is referenced here.
		/// </summary>
		public virtual Report ReportResult { get; set; }
		public virtual MedProfile MedProfile { get; set; }

		/// <summary>
		/// Clones this patient
		/// Does NOT copy the ID
		/// This method would be used if we need to do any temporary editing.
		/// </summary>
		public object Clone()
		{
			Patient net = new Patient()
			{
				Therapist = Therapist,
			};

			return net;
		}
	}
}
