using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Class;
using System.IO;

namespace LocomotionEngines
{
	
	public sealed class ReportEngine
	{
		//Implement as a singleton. (Should refactor other engines like this.)
		private static ReportEngine instance;
		public static ReportEngine getInstance()
		{
			if(instance == null)
				instance = new ReportEngine();
			return instance;
		}
		private ReportEngine() {}

		public Report GenerateReport(Patient pat)
		{
			Report report = new Report();

			report.LStrideNumber = 0;
			report.RStrideNumber = 0;
			report.AverageGaitSpeed = 0;
			report.LeftStrideLength = 0;
			report.RightStrideLength = 0;
			report.RStancePercent = 0;
			report.RSwingPercent = 0;
			report.RSingleLimbStancePercent = 0;
			report.LStancePercent = 0;
			report.LSwingPercent = 0;
			report.LSingleLimbStancePercent = 0;
			report.Candence = 0;

			return report;
		}


		public Report GenerateReport(Patient pat, string path)
		{
			Report report = new Report();
			//Use ExcelAnalyzer object to get the values.
			ExcelAnalyzer doc = new ExcelAnalyzer();

			//Open Excel file
			doc.excel_init(path);

			#region ExcelPull
			string LStrideNumber = doc.excel_getValue("B5");
			if (LStrideNumber != "")
			{
				report.LStrideNumber = Math.Round(Convert.ToDouble(LStrideNumber), 2);
			}
			else
			{
				report.LStrideNumber = 0;
			}
			string LStancePercent = doc.excel_getValue("B6");
			if (LStancePercent != "")
			{
				report.LStancePercent = Math.Round(Convert.ToDouble(LStancePercent), 3) * 100;
			}
			else
			{
				report.LStancePercent = 0;
			}
			string LSwingPercent = doc.excel_getValue("B7");
			if (LSwingPercent != "")
			{
				report.LSwingPercent = Math.Round(Convert.ToDouble(LSwingPercent), 3) * 100;
			}
			else
			{
				report.LSwingPercent = 0;
			}
			string LSingleLimbStancePercent = doc.excel_getValue("B8");
			if (LSingleLimbStancePercent != "")
			{
				report.LSingleLimbStancePercent = Math.Round(Convert.ToDouble(LSingleLimbStancePercent), 3) * 100;
			}
			else
			{
				report.LSingleLimbStancePercent = 0;
			}
			string RStrideNumber = doc.excel_getValue("B11");
			if (RStrideNumber != "")
			{
				report.RStrideNumber = Math.Round(Convert.ToDouble(RStrideNumber), 2);
			}
			else
			{
				report.RStrideNumber = 0;
			}
			string RStancePercent = doc.excel_getValue("B12");
			if (RStancePercent != "")
			{
				report.RStancePercent = Math.Round(Convert.ToDouble(RStancePercent), 3) * 100;
			}
			else
			{
				report.RStancePercent = 0;
			}
			string RSwingPercent = doc.excel_getValue("B13");
			if (RSwingPercent != "")
			{
				report.RSwingPercent = Math.Round(Convert.ToDouble(RSwingPercent), 3) * 100;
			}
			else
			{
				report.RSwingPercent = 0;
			}
			string RSingleLimbStancePercent = doc.excel_getValue("B14");
			if (RSingleLimbStancePercent != "")
			{
				report.RSingleLimbStancePercent = Math.Round(Convert.ToDouble(RSingleLimbStancePercent), 3) * 100;
			}
			else
			{
				report.RSingleLimbStancePercent = 0;
			}
			string AverageGaitSpeed = doc.excel_getValue("B15");
			if (AverageGaitSpeed != "")
			{
				report.AverageGaitSpeed = Math.Round(Convert.ToDouble(AverageGaitSpeed), 3) * 100;
			}
			else
			{
				report.AverageGaitSpeed = 0;
			}
			string LeftStrideLength = doc.excel_getValue("B15");
			if (LeftStrideLength != "")
			{
				report.LeftStrideLength = Math.Round(Convert.ToDouble(LeftStrideLength), 2);
			}
			else
			{
				report.LeftStrideLength = 0;
			}
			string RightStrideLength = doc.excel_getValue("B15");
			if (RightStrideLength != "")
			{
				report.RightStrideLength = Math.Round(Convert.ToDouble(RightStrideLength), 2);
			}
			else
			{
				report.RightStrideLength = 0;
			}
			string Candence = doc.excel_getValue("B15");
			if (Candence != "")
			{
				report.Candence = Math.Round(Convert.ToDouble(Candence), 2);
			}
			else
			{
				report.Candence = 0;
			}
			#endregion 

			//Close Excel
			doc.excel_close();

			return report;
		}
	}
}
