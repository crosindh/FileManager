using FileManager.DataAccess;
using FileManager.Utility;
using System;
using PagedList;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using FileManager.Models;
using System.IO;
using System.Data.Entity;

namespace FileManager.Website.Controllers
{
   
    public class Dakk_DataController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly StaticData staticData = new StaticData();
        //public static void SendEmail(string emailbody)
        //{
        //    //Specify the from and to email address
        //    MailMessage mailMessage = new MailMessage
        //        ("centralrecordofficesindh@gmail.com", "centralrecordofficesindh@gmail.com");
        //    //Specify the email body
        //    mailMessage.Body = emailbody + Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        //    //Specify the email Subject
        //    mailMessage.Subject = "Dakks Status";

        //    //No need to specify the SMTP settings as these
        //    // are already specified in web.config
        //    SmtpClient smtpClient = new SmtpClient();
        //    //Finall send the email message using Send() method
        //    smtpClient.Send(mailMessage);
        //}

        // GET: Dakk_Data
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(string SearchString, string uploadDate, string DateonDakk, string P_StatusString, int? pageNumber)
        {

            //string mailbodymessage;
            //mailbodymessage = "You Have " + ViewBag.Pending_Status + " Dakk Pending" + Environment.NewLine + "You Have Seen " + ViewBag.Seen_Status + " Dakks " + Environment.NewLine + "You Have " + ViewBag.Urgent_Status + " Urgent Dakks";
            //SendEmail(mailbodymessage);
            SetStatusOnDashboard();

            if (!String.IsNullOrEmpty(uploadDate))
            {
                uploadDate = Convert.ToDateTime(uploadDate).Date.ToString("dd-MM-yyyy");
            }
            if (!String.IsNullOrEmpty(DateonDakk))
            {
                DateonDakk = Convert.ToDateTime(DateonDakk).Date.ToString("dd-MM-yyyy");
            }

            if ((User.IsInRole("Admin") || User.Identity.Name == "secretary" || User.Identity.Name == "A.secretary" || User.Identity.Name == "D.secretary" || User.Identity.Name == "chairman") && SearchString != null)
            {
                return View(db.Dakk_Data.Where(x => (((String.IsNullOrEmpty(SearchString) || x.Number.Contains(SearchString) || x.Department.Contains(SearchString)
                                                                     || x.Sectionoforigin.Contains(SearchString) || x.Givennumber.Contains(SearchString) || x.Status.Contains(SearchString) ||
                                                                     x.Subject.Contains(SearchString) || x.ForwardTo.Contains(SearchString)) || x.Receivedby.Contains(SearchString))
                                                                     && (String.IsNullOrEmpty(DateonDakk) || x.DateOnLetter.Contains(DateonDakk))
                                                                     && (String.IsNullOrEmpty(uploadDate) || x.UploadTime.Contains(uploadDate)))).ToList().ToPagedList(pageNumber ?? 1, 10));
            }
            if (User.IsInRole("Admin") || User.Identity.Name == "secretary" || User.Identity.Name == "A.secretary" || User.Identity.Name == "D.secretary" || User.Identity.Name == "chairman")
            {
                return View(db.Dakk_Data.ToList().ToPagedList(pageNumber ?? 1, 10));
            }

            if (SearchString != null)
            {

                return View(db.Dakk_Data.Where(x => ((String.IsNullOrEmpty(SearchString) || x.Number.Contains(SearchString) || x.Department.Contains(SearchString)
                                                                  || x.Sectionoforigin.Contains(SearchString) || x.Givennumber.Contains(SearchString) || x.Status.Contains(SearchString) ||
                                                                  x.Subject.Contains(SearchString) || x.ForwardTo.Contains(SearchString))
                                                                  && (x.Receivedby.Contains(User.Identity.Name))
                                                                  && (String.IsNullOrEmpty(DateonDakk) || x.DateOnLetter.Contains(DateonDakk))
                                                                  && (String.IsNullOrEmpty(uploadDate) || x.UploadTime.Contains(uploadDate)))).ToList().ToPagedList(pageNumber ?? 1, 10));

            }

            else
            {
                return View(db.Dakk_Data.Where(x => x.Receivedby.Contains(User.Identity.Name) || x.ForwardTo.Contains(User.Identity.Name)).ToList().ToPagedList(pageNumber ?? 1, 10));
            }

        }

        private void SetStatusOnDashboard()
        {


            if (User.IsInRole(StaticData.AdminRole) || User.Identity.Name == StaticData.SecretaryRole)
            {
                ViewBag.Pending_Status = db.Dakk_Data.Where(x => x.Status == StaticData.Status1).Count();

                ViewBag.Seen_Status = db.Dakk_Data.Where(x => x.Status == StaticData.Status2).Count();

                ViewBag.Urgent_Status = db.Dakk_Data.Where(x => x.Status == StaticData.Status3).Count();

                ViewBag.Objection_Status = db.Dakk_Data.Where(x => x.Status == StaticData.Status4).Count();
            }
            else
            {

                ViewBag.Pending_Status = ((db.Dakk_Data.Where(x => x.Status == StaticData.Status1
                                        && (x.Receivedby.Contains(User.Identity.Name))).Count()) +
                                        (db.Dakk_Data.Where(x => x.Status == StaticData.Status1
                                        && (x.ForwardTo.Contains(User.Identity.Name))).Count()));

                ViewBag.Seen_Status = ((db.Dakk_Data.Where(x => x.Status == StaticData.Status2
                                        && (x.Receivedby.Contains(User.Identity.Name))).Count()) +
                                        (db.Dakk_Data.Where(x => x.Status == StaticData.Status2
                                        && (x.ForwardTo.Contains(User.Identity.Name))).Count()));

                ViewBag.Urgent_Status = ((db.Dakk_Data.Where(x => x.Status == StaticData.Status3
                                        && (x.Receivedby.Contains(User.Identity.Name))).Count()) +
                                        (db.Dakk_Data.Where(x => x.Status == StaticData.Status3
                                        && (x.ForwardTo.Contains(User.Identity.Name))).Count()));

                ViewBag.Objection_Status = ((db.Dakk_Data.Where(x => x.Status == StaticData.Status4
                                        && (x.Receivedby.Contains(User.Identity.Name))).Count()) +
                                        (db.Dakk_Data.Where(x => x.Status == StaticData.Status4
                                        && (x.ForwardTo.Contains(User.Identity.Name))).Count()));
            }

        }

        // GET: Dakk_Data/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dakk_Data dakk_Data = db.Dakk_Data.Find(id);
            if (dakk_Data == null)
            {
                return HttpNotFound();
            }
            return View(dakk_Data);
        }

        // GET: Dakk_Data/Create
        [Authorize(Users = StaticData.DataEntryUser)]
        public ActionResult Create()
        {
            CreateStaticList();
            return View();
        }

        // POST: Dakk_Data/Create
        [Authorize(Users = StaticData.DataEntryUser)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateOnLetter,DateReceived,Department,Subject,Givennumber,Pages,Addressee,Sectionoforigin,Receivedby,Pdfdirectory,Name,Number,UploadTime,Status,CurrentLocation,ForwardTo,ForwardTime")] Dakk_Data dakk_Data,
                                    HttpPostedFileBase file, string DakkDate, string ReceivedDate)
        {
            dakk_Data.CurrentLocation = User.Identity.Name + StaticData.StaticNavigationArrow + dakk_Data.ForwardTo;
            dakk_Data.ForwardTime = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

            if (ModelState.IsValid)
            {
                db.Dakk_Data.Add(dakk_Data);

                UploadDakk(dakk_Data, file, DakkDate, ReceivedDate);

                db.SaveChanges();
                TempData["success"] = "Dakk Added Successfully";
                return RedirectToAction("Index");
            }
            CreateStaticList();
            return View();
        }

        private void UploadDakk(Dakk_Data dakk_Data, HttpPostedFileBase file, string DakkDate, string ReceivedDate)
        {
            dakk_Data.DateOnLetter = Convert.ToDateTime(DakkDate).Date.ToString("dd-MM-yyyy");
            dakk_Data.DateReceived = Convert.ToDateTime(ReceivedDate).Date.ToString("dd-MM-yyyy");

            dakk_Data.UploadTime =  DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

            int getid = 0;
            try
            {
                if (file.ContentLength > 0)
                {
                    getid = db.Dakk_Data.Select(x => x.ID).DefaultIfEmpty().Max();
                    string _FileName = Path.GetFileName(file.FileName);
                    getid++;

                    string extension = Path.GetExtension(file.FileName);
                    _FileName = getid + "_" + dakk_Data.Sectionoforigin + "_" + dakk_Data.Status + "_" + dakk_Data.DateOnLetter + extension;
                    string _path = Path.Combine(Server.MapPath("~/Downloads/Dakk"), _FileName);
                    string uploadedfilename;
                    uploadedfilename = _path;
                    dakk_Data.Pdfdirectory = _FileName;

                    file.SaveAs(uploadedfilename);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        // GET: Dakk_Data/Edit/5
        public ActionResult Edit(int? id)
        {
            CreateStaticList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dakk_Data dakk_Data = db.Dakk_Data.Find(id);
            if (dakk_Data == null)
            {
                return HttpNotFound();
            }
            return View(dakk_Data);
        }

        // POST: Dakk_Data/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateOnLetter,DateReceived,Department,Subject,Givennumber,Pages,Addressee,Sectionoforigin,Pdfdirectory,Receivedby,Number,UploadTime,Status,ForwardTo,Comments,CurrentLocation,ForwardTime")] Dakk_Data dakk_Data)
        {
            var currentlocation = dakk_Data.CurrentLocation;
            var forwardtime = dakk_Data.ForwardTime;

            if (User.Identity.Name != "R&I")
            {
                var date = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
                dakk_Data.CurrentLocation = currentlocation + StaticData.StaticNavigationArrow + dakk_Data.ForwardTo;
                dakk_Data.ForwardTime = forwardtime + StaticData.StaticNavigationArrow + date;
            }
            else
            {
                var date = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
                dakk_Data.CurrentLocation = User.Identity.Name + StaticData.StaticNavigationArrow + dakk_Data.ForwardTo;
                dakk_Data.ForwardTime = date;
            }

            if (ModelState.IsValid)
            {

                db.Entry(dakk_Data).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Dakk Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(dakk_Data);
        }

        // GET: Dakk_Data/Delete/5
        [Authorize(Roles = StaticData.AdminRole)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dakk_Data dakk_Data = db.Dakk_Data.Find(id);
            if (dakk_Data == null)
            {
                return HttpNotFound();
            }
            return View(dakk_Data);
        }

        // POST: Dakk_Data/Delete/5
        [Authorize(Roles = StaticData.AdminRole)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dakk_Data dakk_Data = db.Dakk_Data.Find(id);
            db.Dakk_Data.Remove(dakk_Data);
            db.SaveChanges();
            TempData["success"] = "Dakk Deleted Successfully";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [AllowAnonymous]
        public ActionResult Download(string dakkname)
        {
            string path = Server.MapPath("~/Downloads/Dakk");

            byte[] fileBytes = System.IO.File.ReadAllBytes(path + @"\" + dakkname);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, dakkname);
        }
        private void CreateStaticList()
        {
            ViewBag.list = staticData.Deparment_list;
            ViewBag.list2 = staticData.StatusList;
            ViewBag.list3 = staticData.ForwardToList;
        }
     
    }
}
