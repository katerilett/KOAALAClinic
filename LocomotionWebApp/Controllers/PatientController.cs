using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel.Class;
using LocomotionEngines;
using LocomotionWebApp.Models.ViewModels;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Data.Entity.Validation;
using System.Json;

namespace LocomotionWebApp.Controllers
{
	public class PatientController : Controller
	{
		//
		// GET: /Patient/
		[Authorize]
		public ActionResult Index()
		{
			var nvm = new PatientListViewModel();

			using(var c = new DataModelContext())
			{
				nvm.Patients = c.Patients.Include("Therapist").Where(n => n.LastName != null).ToList();
			}

			object alertObject;
			if(TempData.TryGetValue("Alert", out alertObject))
			{
				ViewBag.Alert = alertObject;
			}
			
			return View(nvm);
		}

		[Authorize]
		public ActionResult View(long id)
		{
			var nvm = new PatientViewModel();

			using(var c = new DataModelContext())
			{
				var patient = c.Patients.Find(id);
				if(patient == null)
				{
					TempData["Alert"] = "The selected patient does not exist.";
					return RedirectToAction("Index");
				}								

				nvm.ID = patient.ID;
				nvm.FirstName = patient.FirstName;
				nvm.LastName = patient.LastName;
				nvm.Therapist = patient.Therapist;
				nvm.Doctor = patient.Doctor;
				nvm.LastUpdate = patient.LastUpdate;
				nvm.Start = patient.Start;
				nvm.Age = patient.Age;
				nvm.Birthday = patient.Birthday;
				nvm.Gender = patient.Gender;
				nvm.Height = patient.Height;
				nvm.Weight = patient.Weight;
				nvm.ArthritisType = patient.ArthritisType;
				nvm.AffectedExtremity = patient.AffectedExtremity;
				nvm.Deformity = patient.Deformity;
				nvm.ShankLength = patient.ShankLength;
				nvm.ThighLength = patient.ThighLength;
				nvm.Email = patient.Email;
				nvm.PhoneNumber = patient.PhoneNumber;
				nvm.City = patient.City;
				nvm.ContactName = patient.ContactName;
				nvm.ContactRelation = patient.ContactRelation;
				nvm.ContactPhoneNumber = patient.ContactPhoneNumber;
				nvm.ContactEmail = patient.ContactEmail;

				nvm.Report = patient.ReportResult;
				nvm.MedProfile = patient.MedProfile;

				DateTime dateOnly = nvm.Birthday.Date;
				nvm.BirthdayString = dateOnly.ToString("d");

				DateTime dateOnly2 = nvm.Start.Date;
				nvm.StartString = dateOnly2.ToString("d");

				DateTime dateOnly3 = nvm.Birthday.Date;
				nvm.BirthdayHtml = dateOnly3.ToString("yyyy-MM-dd");

				//var nameNet = network;
				//while(nameNet.Parent != null && nameNet.Name == null)
				//	nameNet = nameNet.Parent;
				//nvm.Name = nameNet.Name;

				if(patient.ReportResult == null)
				{
					nvm.OutOfDate = false;
					//nvm.Optimized = false;
				}
				else
				{
					nvm.OutOfDate = patient.ReportResult.OutOfDate;
					//nvm.Optimized = true;
				}
			}

			return View("View", nvm);
		}

		[Authorize]
		public ActionResult Report(long id)
		{
			var nvm = new PatientViewModel();

			using (var c = new DataModelContext())
			{
				User currentUser = UserDataEngine.getInstance().GetCurrentUser(c, HttpContext);
				
				var patient = c.Patients.Find(id);
				if (patient == null)
				{
					TempData["Alert"] = "The selected patient does not exist.";
					return RedirectToAction("Index");
				}

				nvm.ID = patient.ID;
				nvm.FirstName = patient.FirstName;
				nvm.LastName = patient.LastName;
				nvm.Therapist = patient.Therapist;
				nvm.Doctor = patient.Doctor;
				nvm.LastUpdate = patient.LastUpdate;
				nvm.ArthritisType = patient.ArthritisType;
				nvm.AffectedExtremity = patient.AffectedExtremity;
				nvm.Deformity = patient.Deformity;
				nvm.ShankLength = patient.ShankLength;
				nvm.ThighLength = patient.ThighLength;
				nvm.Report = patient.ReportResult;
				nvm.MedProfile = patient.MedProfile;

			}
			return View("Report", nvm);

		}


		[Authorize]
		public ActionResult Delete(long id)
		{
			using(var c = new DataModelContext())
			{
				var patient = c.Patients.Find(id);
				patient.LastName = null;
				c.SaveChanges();
			}
			return RedirectToAction("Index");
		}

		//Network History
		//[Authorize]
		//public ActionResult NetworkHistory(long id)
		//{
		//	var nvm = new NetworkListViewModel();
		//	nvm.Networks = new List<Network>();

		//	using(var c = new DataModelContext())
		//	{
		//		var network = c.Networks
		//			.Include("Author")
		//			.Include("Parent")
		//			.SingleOrDefault(n => n.ID == id);
		//		while(true)
		//		{
		//			nvm.Networks.Add(network);
		//			if (network.Parent == null)
		//				break;
		//			network = c.Networks.Include("Author").Include("Parent")
		//				.SingleOrDefault(n => n.ID == network.Parent.ID);
		//		}
		//	}

		//	return View(nvm);
		//}

		[Authorize]
		public ActionResult TempEdit(long id, int clickTab)
		{
			//Patient newPatient;
			int tab = 0;
			tab = clickTab;

			long patientID = id;
			/*
			using(var c = new DataModelContext())
			{
				var originalPatient = c.Patients.Find(id);

				newPatient = (Patient)originalPatient.Clone();
				newPatient.Therapist = UserDataEngine.getInstance().GetCurrentUser(c, HttpContext);
				newPatient.LastUpdate = DateTime.Now;
				//newPatient.Parent = originalNetwork;
				//newPatient.Revision = originalNetwork.Revision + 1;

				c.Patients.Add(newPatient);
				c.SaveChanges();
			}*/

			//ViewBag.ID = newPatient.ID;

			return RedirectToAction("View", new { id = patientID, tab = tab });
		}

		//OptimizationSidebar
		//[Authorize]
		//public ActionResult OptimizationSidebar(long id, int startTab = 1)
		//{
		//	var osvm = new OptimizationSidebarViewModel();
		//	osvm.StartTab = startTab;
		//	using(var c = new DataModelContext())
		//	{
		//		var network = c.Networks
		//			.Include("OptimizationResult")
		//			.Include("OptimizationResult.Nodes")
		//			.Include("OptimizationResult.Nodes.Node")
		//			.Include("OptimizationResult.Links")
		//			.Include("OptimizationResult.Links.Link")
		//			.FirstOrDefault(n => n.ID == id);
		//		osvm.Optimization = network.OptimizationResult;
		//	}
		//	return View(osvm);
		//}

		//[Authorize]
		//public JsonResult GetNetwork(long id)
		//{
			//using(var c = new DataModelContext())
			//{
			//	Network net = c.Networks.Find(id);
			//	Optimization opt = net.OptimizationResult;


			//	if(net == null)
			//	{
			//		return Json(new {
			//				failure=true, failureMessage="The selected network does not exist."
			//			},
			//			JsonRequestBehavior.AllowGet);
			//	}

			//	if(opt == null)
			//	{
			//		return Json(new
			//		{		
			//	MaxCarsPerTrain = net.MaxCarsPerTrain,
			//	NonFuelCostPerMile = net.NonFuelCostPerMile,
			//	FuelCostPerMile = net.FuelCostPerMile,
			//	CarCostPerMile = net.CarCostPerMile,

						//nodes = net.Nodes.ToDictionary(
						//	n => n.ID.ToString(),
						//	n => new
						//	{
						//	id = n.ID,
						//		name = n.Name,
						//		stationcode = n.StationCode,
						//		longitude = n.Location.Longitude,
						//		latitude = n.Location.Latitude,
						//		carCapacity = n.CarCapacity,
						//	}
						//),
					//nodesArray = net.Nodes.Select(n => new
					//{
					//	id = n.ID,
					//	name = n.Name,
					//	stationcode = n.StationCode,
					//	longitude = n.Location.Longitude,
					//	latitude = n.Location.Latitude,
					//	carCapacity = n.CarCapacity,
					//}),
					//	links = net.Links.Select(l => new {
					//		id = l.ID,
					//		fromid = l.From.ID,
					//		toid = l.To.ID,
					//		distance = l.Distance,
					//		maxTrains = l.MaxTrains,
					//		fuelAdjustment = l.FuelAdjustment
					//	}),

					//	orders = net.Orders.Select(l => new
					//	{
					//		id = l.ID,
					//		xmlID = l.XMLOrderID,
					//		fromid = l.Origin.ID,
					//		toid = l.Destination.ID,
					//		cars = l.Cars,
					//		revenue = l.Revenue
					//	})

				//	}, JsonRequestBehavior.AllowGet);
				//}

				//return Json(new
				//{
				//	MaxCarsPerTrain = net.MaxCarsPerTrain,
				//	NonFuelCostPerMile = net.NonFuelCostPerMile,
				//	FuelCostPerMile = net.FuelCostPerMile,
				//	CarCostPerMile = net.CarCostPerMile,

					//nodes = net.Nodes.ToDictionary(
					//	n => n.ID.ToString(),
					//	n => new
					//	{   id = n.ID,
					//		name = n.Name,
					//		stationcode = n.StationCode,
					//		longitude = n.Location.Longitude,
					//		latitude = n.Location.Latitude,
					//		carCapacity = n.CarCapacity,
					//	}
					//),
					//nodesArray = net.Nodes.Select(n => new
					//{   name = n.Name,
					//	id = n.ID,
					//	stationcode = n.StationCode,
					//	longitude = n.Location.Longitude,
					//	latitude = n.Location.Latitude,
					//	carCapacity = n.CarCapacity,
					//}),
					//links = net.Links.Select(l => new {
					//	id = l.ID,
					//	fromid = l.From.ID,
					//	toid = l.To.ID,
					//	distance = l.Distance,
					//	maxTrains = l.MaxTrains,
					//	fuelAdjustment = l.FuelAdjustment
					//}),

					//orders = net.Orders.Select(l => new
					//{
					//	id = l.ID,
					//	xmlID = l.XMLOrderID,
					//	fromid = l.Origin.ID,
					//	toid = l.Destination.ID,
					//	cars = l.Cars,
					//	revenue = l.Revenue
					//}),


					//optimizedNodes = opt.Nodes.ToDictionary(
					//	n => n.Node.ID.ToString(),
					//	n => new
					//	{
					//		inFlow = n.FlowIn,
					//		outFlow = n.FlowOut,
					//	}
					//),
					//optimizedLinks = opt.Links.ToDictionary(
					//	l => l.Link.ID.ToString(),
					//	l => new
					//	{
					//		currentTrains = l.CurrentTrains,
					//		currentFlow = l.Flow,
					//		supscription = l.TrainSubscription()
					//	}
					//)

		//		}, JsonRequestBehavior.AllowGet);
		//	}
		//}

		[Authorize]
		public ActionResult EditPatient (DateTime PatientBirthday, string PatientGender, double PatientHeight, 
			double PatientWeight, string PatientDoctor,	string PatientCity, string PatientEmail, string PatientPhoneNumber, 
			PatientViewModel model)
		{
			var pvm = new PatientViewModel();
			long patientID = 0;

			using (var c = new DataModelContext())
			{
				var patient = c.Patients.Find(model.ID);
				patientID = patient.ID;

				patient.Birthday = PatientBirthday;
				patient.Gender = PatientGender;
				patient.Height = PatientHeight;
				patient.Weight = PatientWeight;
				patient.Doctor = PatientDoctor;
				patient.City = PatientCity;
				patient.Email = PatientEmail;
				patient.PhoneNumber = PatientPhoneNumber;

				DateTime today = DateTime.Today;
				int age = today.Year - PatientBirthday.Year;
				if (PatientBirthday > today.AddYears(-age)) age--;
				patient.Age = age;
				
				c.SaveChanges();
				
			}

			ViewBag.Alert = "Profile update successful";
			ViewBag.AlertClass = "alert-success";
			//return View("View", pvm);
			return RedirectToAction("View", new { id = patientID});
		}

		[Authorize]
		public ActionResult EditContact(string PatientContactName, string PatientContactRelation, 
			string PatientContactPhoneNumber, string PatientContactEmail, PatientViewModel model)
		{
			var pvm = new PatientViewModel();
			long patientID = 0;

			using (var c = new DataModelContext())
			{
				var patient = c.Patients.Find(model.ID);
				patientID = patient.ID;
				patient.ContactName = PatientContactName;
				patient.ContactRelation = PatientContactRelation;
				patient.ContactPhoneNumber = PatientContactPhoneNumber;
				patient.ContactEmail = PatientContactEmail;

				c.SaveChanges();

			}

			ViewBag.Alert = "Profile update successful";
			ViewBag.AlertClass = "alert-success";
			//return View("View", pvm);
			return RedirectToAction("View", new { id = patientID });
		}

		[Authorize]
		public ActionResult EditMedical(string PatientArthritisType, string PatientAffectedExtremity,
			string PatientDeformity, PatientViewModel model)
		{
			var pvm = new PatientViewModel();
			long patientID = 0;

			using (var c = new DataModelContext())
			{
				var patient = c.Patients.Find(model.ID);
				patientID = patient.ID;
				patient.ArthritisType = PatientArthritisType;
				patient.AffectedExtremity = PatientAffectedExtremity;
				patient.Deformity = PatientDeformity;

				c.SaveChanges();

			}

			ViewBag.Alert = "Profile update successful";
			ViewBag.AlertClass = "alert-success";
			//return View("View", pvm);
			return RedirectToAction("View", new { id = patientID });
		}

		[Authorize]
		public ActionResult EditMedicalProfile(string PatientCurrentMeds, string PatientHeartDisease, string PPatientHeartDisease,
			string PatientDiabetes, string PPatientDiabetes, string PatientCancer, 
			string PPatientCancer, string PatientHighBloodPressure, string PPatientHighBloodPressure,
			PatientViewModel model)
		{
			var pvm = new PatientViewModel();
			long patientID = 0;

			using (var c = new DataModelContext())
			{
				var patient = c.Patients.Find(model.ID);
				patientID = patient.ID;

				patient.MedProfile.CurrentMeds = new List<string>(
					PatientCurrentMeds.Split(new string[] { "\r\n" },
					StringSplitOptions.RemoveEmptyEntries));

				//Setting Current Conditions
				if (PatientHeartDisease == "Current")
				{
					patient.MedProfile.HeartDisease = true;
				}
				else
				{
					patient.MedProfile.HeartDisease = false;
				}
				if (PatientDiabetes == "Current")
				{
					patient.MedProfile.Diabetes = true;
				}
				else
				{
					patient.MedProfile.Diabetes = false;
				}
				if (PatientCancer == "Current")
				{
					patient.MedProfile.Cancer = true;
				}
				else
				{
					patient.MedProfile.Cancer = false;
				}
				if (PatientHighBloodPressure == "Current")
				{
					patient.MedProfile.HighBloodPressure = true;
				}
				else
				{
					patient.MedProfile.HighBloodPressure = false;
				}
				//Setting Past Conditions
				if (PPatientHeartDisease == "Past")
				{
					patient.MedProfile.PHeartDisease = true;
				}
				else
				{
					patient.MedProfile.PHeartDisease = false;
				}
				if (PPatientDiabetes == "Past")
				{
					patient.MedProfile.PDiabetes = true;
				}
				else
				{
					patient.MedProfile.PDiabetes = false;
				}
				if (PPatientCancer == "Past")
				{
					patient.MedProfile.PCancer = true;
				}
				else
				{
					patient.MedProfile.PCancer = false;
				}
				if (PPatientHighBloodPressure == "Past")
				{
					patient.MedProfile.PHighBloodPressure = true;
				}
				else
				{
					patient.MedProfile.PHighBloodPressure = false;
				}
				
				c.SaveChanges();

			}

			ViewBag.Alert = "Profile update successful";
			ViewBag.AlertClass = "alert-success";
			//return View("View", pvm);
			return RedirectToAction("View", new { id = patientID });
		}

		[Authorize]
		public ActionResult EditRequired(double PatientShankLength, double PatientThighLength, PatientViewModel model)
		{
			var pvm = new PatientViewModel();
			long patientID = 0;

			using (var c = new DataModelContext())
			{
				var patient = c.Patients.Find(model.ID);
				patientID = patient.ID;
				patient.ShankLength = PatientShankLength;
				patient.ThighLength = PatientThighLength;				

				c.SaveChanges();

			}

			ViewBag.Alert = "Profile update successful";
			ViewBag.AlertClass = "alert-success";
			//return View("View", pvm);
			return RedirectToAction("Report", new { id = patientID });
		}

		[Authorize]
		public ActionResult CreateBlank(string PatientFirstName, string PatientLastName, 
			DateTime PatientBirthday, string PatientGender, int PatientFeet, int PatientInches,
			double PatientWeight, string PatientDoctor, string PatientArthritisType, 
			string PatientAffectedExtremity, string PatientDeformity, string PatientCity,
			string PatientPhoneNumber, string PatientEmail)
		{
			var nvm = new PatientListViewModel();

			if(PatientFirstName == "")
			{
				ViewBag.UploadAlert = "Enter the patient's first name";

				using (var c = new DataModelContext())
				{
					nvm.Patients = c.Patients.Include("Therapist").Where(n => n.LastName != null).ToList();
				}
				return View("Index", nvm);
			}

			if (PatientLastName == "")
			{
				ViewBag.UploadAlert = "Enter the patient's last name";

				using (var c = new DataModelContext())
				{
					nvm.Patients = c.Patients.Include("Therapist").Where(n => n.LastName != null).ToList();
				}
				return View("Index", nvm);
			}

			using(var c = new DataModelContext())
			{
				var patient = new Patient();
				patient.ReportResult = ReportEngine.getInstance().GenerateReport(patient);
				patient.MedProfile = new MedProfile();

				patient.FirstName = PatientFirstName;
				patient.LastName = PatientLastName;
				patient.Therapist = UserDataEngine.getInstance().GetCurrentUser(c, HttpContext);
				patient.LastUpdate = DateTime.Now;
				patient.Start = DateTime.Now;
				patient.Birthday = PatientBirthday;
				patient.Gender = PatientGender;
				patient.Height = (PatientFeet * 12) + PatientInches;
				patient.Weight = PatientWeight;
				patient.Doctor = PatientDoctor;
				patient.ArthritisType = PatientArthritisType;
				patient.AffectedExtremity = PatientAffectedExtremity;
				patient.Deformity = PatientDeformity;
				patient.ShankLength = 0;
				patient.ThighLength = 0;
				patient.PhoneNumber = PatientPhoneNumber;
				patient.Email = PatientEmail;
				patient.City = PatientCity;
				patient.ContactName = "Not entered";
				patient.ContactRelation = "Not entered";
				patient.ContactPhoneNumber = "Not entered";
				patient.ContactEmail = "Not entered";

				DateTime today = DateTime.Today;
				int age = today.Year - PatientBirthday.Year;
				if (PatientBirthday > today.AddYears(-age)) age--;
				patient.Age = age;

				c.Patients.Add(patient);

				try
				{
					c.SaveChanges();
				}
				catch(DbEntityValidationException e)
				{
					foreach(var i in e.EntityValidationErrors)
					{
						Console.WriteLine(i.ValidationErrors);
					}
					throw e;
				}
				nvm.Patients = c.Patients.Include("Therapist").Where(n => n.LastName != null).ToList();

				ViewBag.NewPatientID = patient.ID;
			}

			ViewBag.Alert = "Patient upload successful";
			ViewBag.AlertClass = "alert-success";
			return View("Index", nvm);
		}

		[Authorize]
		public ActionResult Upload(IEnumerable<HttpPostedFileBase> files, PatientViewModel model)
		{			
			var pvm = new PatientViewModel();
			long patientID = 0;

			//HttpFileCollection someFiles = files;
			//IEnumerable<HttpPostedFileBase> someFiles = files;			

			//try
			//{
			//	patientDoc = XDocument.Load(Request.Files["PatientFile"].InputStream);
			//}
			//catch (Exception e)
			//{
			//	Console.WriteLine(e.Message);
			//	ViewBag.UploadAlert = "You must select a valid file";
			//}

			using(var c = new DataModelContext())
			{
				var patient = c.Patients.Find(model.ID);
				patientID = patient.ID;

				HttpPostedFileBase patientDoc = Request.Files["PatientFile"];
				var fileName = Path.GetFileName(patientDoc.FileName);
				var path = Path.Combine(Server.MapPath("~/Content/excelfiles"), fileName);
				patientDoc.SaveAs(path);

				var ReportE = ReportEngine.getInstance();
				pvm.Report = ReportE.GenerateReport(patient, path);
				patient.ReportResult = pvm.Report;

				try
				{
					c.SaveChanges();
				}
				catch(DbEntityValidationException e)
				{
					foreach(var i in e.EntityValidationErrors)
					{
						Console.WriteLine(i.ValidationErrors);
					}
					throw e;
				}
				//nvm.Patients = c.Patients.Include("Therapist").Where(n => n.Name != null).ToList();

				//ViewBag.NewNetworkID = xmlnetwork.ID;
				
			}

			ViewBag.Alert = "Upload successful";
			ViewBag.AlertClass = "alert-success";

			return RedirectToAction("Report", new { id = patientID });
			//return View("Report", pvm);
		}

		#region URLUpload
		//[Authorize]
		//public ActionResult UploadUrl(string NetworkName, string NetworkUrl)
		//{
		//	var nvm = new NetworkListViewModel();

		//	//Network net;

		//	if(NetworkName == "")
		//	{
		//		ViewBag.UrlUploadAlert = "Enter a network name";

		//		using (var c = new DataModelContext())
		//		{
		//			nvm.Networks = c.Networks.Include("Author").Where(n => n.Name != null).ToList();
		//		}
		//		return View("Index", nvm);
		//	}

		//	try
		//	{
		//		//net = new XmlEngine().UrlDownloadToNetwork(new Uri(NetworkUrl));
		//	}
		//	catch (Exception e)
		//	{
		//		Console.WriteLine(e.Message);
		//		ViewBag.UrlUploadAlert = "Could not find valid XML file at specified URL";

		//		using (var c = new DataModelContext())
		//		{
		//			nvm.Networks = c.Networks.Include("Author").Where(n => n.Name != null).ToList();
		//		}
		//		return View("Index", nvm);
		//	}

		//	using(var c = new DataModelContext())
		//	{
		//		//net.Name = NetworkName;
		//		//net.Author = UserDataEngine.getInstance().GetCurrentUser(c, HttpContext);
		//		//net.LastEdit = DateTime.Now;
		//		//c.Networks.Add(net);

		//		try
		//		{
		//			c.SaveChanges();
		//		}
		//		catch(DbEntityValidationException e)
		//		{
		//			foreach(var i in e.EntityValidationErrors)
		//			{
		//				Console.WriteLine(i.ValidationErrors);
		//			}
		//			throw e;
		//		}
		//		nvm.Networks = c.Networks.Include("Author").Where(n => n.Name != null).ToList();

		//		//ViewBag.NewNetworkID = net.ID;
		//	}

		//	ViewBag.Alert = "Network upload successful";
		//	ViewBag.AlertClass = "alert-success";
		//	return View("Index", nvm);
		//}
		#endregion

		//[Authorize]
		//public ActionResult SaveNetwork(NetworkViewModel model)
		//{
		//	using (var c = new DataModelContext())
		//	{
		//		var net = c.Networks.Find(model.ID);
		//		net.LastEdit = DateTime.Now;

		//		net.Name = net.Parent.Name;
		//		net.Parent.Name = null;

		//		c.SaveChanges();
		//	}
		//	return TempEdit(model.ID, 0);
		//}

		//[Authorize]
		//public ActionResult SaveNetworkAs(NetworkViewModel model)
		//{
		//	using (var c = new DataModelContext())
		//	{
		//		var net = c.Networks.Find(model.ID);
		//		net.Name = model.Name;
		//		net.LastEdit = DateTime.Now;
		//		c.SaveChanges();
		//	}
		//	return TempEdit(model.ID, 0);
		//}

		public JsonResult FindSteps()
		{
			List<long[]> data = new List<long[]>();
			StreamReader reader = new StreamReader("C:\\Users\\Kate\\Desktop\\Data\\steps.txt");
			string line;
			char[] delimiterChars = { ',', '[', ']' };

			while ((line = reader.ReadLine()) != null)
			{
				string[] stringInput = line.Split(delimiterChars);
				long[] array = new long[2];
				array[0] = (long)Convert.ToDouble(stringInput[1]);
				array[1] = (long)Convert.ToDouble(stringInput[2]);
				data.Add(array);				
			}

			reader.Close();
			var jsonData = Json(data, JsonRequestBehavior.AllowGet);
			return (jsonData);
			
		}

		public JsonResult FindDistance()
		{
			List<float[]> data = new List<float[]>();
			StreamReader reader = new StreamReader("C:\\Users\\Kate\\Desktop\\Data\\distance1.txt");
			string line;
			char[] delimiterChars = { ',', '[', ']' };

			while ((line = reader.ReadLine()) != null)
			{
				string[] stringInput = line.Split(delimiterChars);
				float[] array = new float[2];
				array[0] = (float)Convert.ToDouble(stringInput[1]);
				array[1] = (float)Convert.ToDouble(stringInput[2]);
				data.Add(array);
			}

			reader.Close();
			var jsonData = Json(data, JsonRequestBehavior.AllowGet);
			return (jsonData);
		}

		public JsonResult FindWalkTestDistance()
		{
			List<float[]> data = new List<float[]>();
			StreamReader reader = new StreamReader("C:\\Users\\Kate\\Desktop\\Data\\distance2.txt");
			string line;
			char[] delimiterChars = { ',', '[', ']' };

			while ((line = reader.ReadLine()) != null)
			{
				string[] stringInput = line.Split(delimiterChars);
				float[] array = new float[2];
				array[0] = (float)Convert.ToDouble(stringInput[1]);
				array[1] = (float)Convert.ToDouble(stringInput[2]);
				data.Add(array);
			}

			reader.Close();
			var jsonData = Json(data, JsonRequestBehavior.AllowGet);
			return (jsonData);
		}

		public JsonResult FindStairSteps()
		{
			List<long[]> data = new List<long[]>();
			StreamReader reader = new StreamReader("C:\\Users\\Kate\\Desktop\\Data\\stairsteps.txt");
			string line;
			char[] delimiterChars = { ',', '[', ']' };

			while ((line = reader.ReadLine()) != null)
			{
				string[] stringInput = line.Split(delimiterChars);
				long[] array = new long[2];
				array[0] = (long)Convert.ToDouble(stringInput[1]);
				array[1] = (long)Convert.ToDouble(stringInput[2]);
				data.Add(array);
			}

			reader.Close();
			var jsonData = Json(data, JsonRequestBehavior.AllowGet);
			return (jsonData);
		}

		public JsonResult FindMaxVelocity()
		{
			List<float[]> data = new List<float[]>();
			StreamReader reader = new StreamReader("C:\\Users\\Kate\\Desktop\\Data\\maxvelocity.txt");
			string line;
			char[] delimiterChars = { ',', '[', ']' };

			while ((line = reader.ReadLine()) != null)
			{
				string[] stringInput = line.Split(delimiterChars);
				float[] array = new float[2];
				array[0] = (float)Convert.ToDouble(stringInput[1]);
				array[1] = (float)Convert.ToDouble(stringInput[2]);
				data.Add(array);
			}

			reader.Close();
			var jsonData = Json(data, JsonRequestBehavior.AllowGet);
			return (jsonData);
		}

		public JsonResult FindAverageVelocity()
		{
			List<float[]> data = new List<float[]>();
			StreamReader reader = new StreamReader("C:\\Users\\Kate\\Desktop\\Data\\avgvelocity.txt");
			string line;
			char[] delimiterChars = { ',', '[', ']' };

			while ((line = reader.ReadLine()) != null)
			{
				string[] stringInput = line.Split(delimiterChars);
				float[] array = new float[2];
				array[0] = (float)Convert.ToDouble(stringInput[1]);
				array[1] = (float)Convert.ToDouble(stringInput[2]);
				data.Add(array);
			}

			reader.Close();
			var jsonData = Json(data, JsonRequestBehavior.AllowGet);
			return (jsonData);
		}

		public JsonResult FindStancePercent()
		{
			List<float[]> data = new List<float[]>();
			StreamReader reader = new StreamReader("C:\\Users\\Kate\\Desktop\\Data\\stancepercent.txt");
			string line;
			char[] delimiterChars = { ',', '[', ']' };

			while ((line = reader.ReadLine()) != null)
			{
				string[] stringInput = line.Split(delimiterChars);
				float[] array = new float[2];
				array[0] = (float)Convert.ToDouble(stringInput[1]);
				array[1] = (float)Convert.ToDouble(stringInput[2]);
				data.Add(array);
			}

			reader.Close();
			var jsonData = Json(data, JsonRequestBehavior.AllowGet);
			return (jsonData);
		}

		public JsonResult FindSwingPercent()
		{
			List<float[]> data = new List<float[]>();
			StreamReader reader = new StreamReader("C:\\Users\\Kate\\Desktop\\Data\\swingpercent.txt");
			string line;
			char[] delimiterChars = { ',', '[', ']' };

			while ((line = reader.ReadLine()) != null)
			{
				string[] stringInput = line.Split(delimiterChars);
				float[] array = new float[2];
				array[0] = (float)Convert.ToDouble(stringInput[1]);
				array[1] = (float)Convert.ToDouble(stringInput[2]);
				data.Add(array);
			}

			reader.Close();
			var jsonData = Json(data, JsonRequestBehavior.AllowGet);
			return (jsonData);
		}

		public JsonResult FindSinglePercent()
		{
			List<float[]> data = new List<float[]>();
			StreamReader reader = new StreamReader("C:\\Users\\Kate\\Desktop\\Data\\singlepercent.txt");
			string line;
			char[] delimiterChars = { ',', '[', ']' };

			while ((line = reader.ReadLine()) != null)
			{
				string[] stringInput = line.Split(delimiterChars);
				float[] array = new float[2];
				array[0] = (float)Convert.ToDouble(stringInput[1]);
				array[1] = (float)Convert.ToDouble(stringInput[2]);
				data.Add(array);
			}

			reader.Close();
			var jsonData = Json(data, JsonRequestBehavior.AllowGet);
			return (jsonData);
		}

		public JsonResult FindStrideLength()
		{
			List<float[]> data = new List<float[]>();
			StreamReader reader = new StreamReader("C:\\Users\\Kate\\Desktop\\Data\\stridelength.txt");
			string line;
			char[] delimiterChars = { ',', '[', ']' };

			while ((line = reader.ReadLine()) != null)
			{
				string[] stringInput = line.Split(delimiterChars);
				float[] array = new float[2];
				array[0] = (float)Convert.ToDouble(stringInput[1]);
				array[1] = (float)Convert.ToDouble(stringInput[2]);
				data.Add(array);
			}

			reader.Close();
			var jsonData = Json(data, JsonRequestBehavior.AllowGet);
			return (jsonData);
		}

		public JsonResult FindStepLength()
		{
			List<float[]> data = new List<float[]>();
			StreamReader reader = new StreamReader("C:\\Users\\Kate\\Desktop\\Data\\steplength.txt");
			string line;
			char[] delimiterChars = { ',', '[', ']' };

			while ((line = reader.ReadLine()) != null)
			{
				string[] stringInput = line.Split(delimiterChars);
				float[] array = new float[2];
				array[0] = (float)Convert.ToDouble(stringInput[1]);
				array[1] = (float)Convert.ToDouble(stringInput[2]);
				data.Add(array);
			}

			reader.Close();
			var jsonData = Json(data, JsonRequestBehavior.AllowGet);
			return (jsonData);
		}

	}
}
