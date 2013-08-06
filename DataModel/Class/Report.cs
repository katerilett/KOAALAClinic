using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Class
{
	public class Report : ICloneable
	{
		[Key]
		public long ID { get; set; }

		public bool OutOfDate { get; set; }

		//Gait Characteristis
		public double LStrideNumber { get; set; }
		public double RStrideNumber { get; set; }
		public double AverageGaitSpeed { get; set; }
		public double LeftStrideLength { get; set; }
		public double RightStrideLength { get; set; }
		public double RStancePercent { get; set; }
		public double RSwingPercent { get; set; }
		public double RSingleLimbStancePercent { get; set; }
		public double LStancePercent { get; set; }
		public double LSwingPercent { get; set; }
		public double LSingleLimbStancePercent { get; set; }
		public double Candence { get; set; }

		//Constructor
		public Report() 
		{
			LStrideNumber = 0;
			RStrideNumber = 0;
			AverageGaitSpeed = 0;
			LeftStrideLength = 0;
			RightStrideLength = 0;
			RStancePercent = 0;
			RSwingPercent = 0;
			RSingleLimbStancePercent = 0;
			LStancePercent = 0;
			LSwingPercent = 0;
			LSingleLimbStancePercent = 0;
			Candence = 0;		
		}
		
		/// <summary>
		/// Note: does not clone Patient reference.
		/// Not used currently. 
		/// </summary>
		public object Clone()
		{
			return new Report()
			{
				OutOfDate = OutOfDate,
				
			};
		}
	}
}
