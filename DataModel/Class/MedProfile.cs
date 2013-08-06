using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Class
{
	public class MedProfile : ICloneable
	{
		[Key]
		public long ID { get; set; }

		//Current Conditions
		public bool HeartDisease { get; set; }
		public bool Diabetes { get; set; }
		public bool Cancer { get; set; }
		public bool HighBloodPressure { get; set; }
		//PastConditions
		public bool PHeartDisease { get; set; }
		public bool PDiabetes { get; set; }
		public bool PCancer { get; set; }
		public bool PHighBloodPressure { get; set; }

		public List<string> CurrentMeds { get; set; }
		public List<string> MedAllergies { get; set; }

		//Constructor
		public MedProfile() 
		{
			HeartDisease = false;
			Diabetes = false;
			Cancer = false;
			HighBloodPressure = false;
			PHeartDisease = false;
			PDiabetes = false;
			PCancer = false;
			PHighBloodPressure = false;
		}
		
		/// <summary>
		/// Note: does not clone Patient reference.
		/// Not used currently. 
		/// </summary>
		public object Clone()
		{
			return new MedProfile()
			{
				
				
			};
		}
	}
}
