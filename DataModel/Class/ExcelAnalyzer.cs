using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace DataModel.Class
{
	public class ExcelAnalyzer
	{
		[Key]
		public long ID { get; set; }

		public static Microsoft.Office.Interop.Excel.Application appExcel;
		public static Workbook newWorkbook = null;
		public static _Worksheet objsheet = null;

		//Test Commit comment2

		public ExcelAnalyzer() {}

		//Method to initialize opening Excel
		public void excel_init(string path)
		{
			appExcel = new Microsoft.Office.Interop.Excel.Application();
			
			if (System.IO.File.Exists(path))
			{
				// then go and load this into excel
				newWorkbook = appExcel.Workbooks.Open(path, true, true);
				objsheet = (_Worksheet)appExcel.ActiveWorkbook.ActiveSheet;
			}

			else
			{
				System.Runtime.InteropServices.Marshal.ReleaseComObject(appExcel);
				appExcel = null;
			}

		}

		//Method to get value; cellname is A1,A2, or B1,B2 etc...in excel.
		public string excel_getValue(string cellname)
		{
			string value = string.Empty;
			try
			{
				value = objsheet.get_Range(cellname).get_Value().ToString();
			}
			catch
			{
				value = "";
			}

			return value;
		}

		//Method to close excel connection
		public void excel_close()
		{
			if (appExcel != null)
			{
				try
				{
					newWorkbook.Close();
					System.Runtime.InteropServices.Marshal.ReleaseComObject(appExcel);
					appExcel = null;
					objsheet = null;
				}
				catch (Exception ex)
				{
					appExcel = null;
				}
				finally
				{
					GC.Collect();
				}
			}
		}

	}
}
