using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FileManager.DataAccess;
using FileManager.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using FileManager.Utility;

namespace FileManager.Website.Controllers
{
    [Authorize]
    public class FileInformationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private StaticData staticData = new StaticData();


        // GET: FileInformation
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(string SearchString, string uploadDate, string P_StatusString, int? pageNumber,string SearchSection)
        {
        
           
             
            if (!String.IsNullOrEmpty(uploadDate))
            {
                uploadDate = Convert.ToDateTime(uploadDate).Date.ToString("dd-MM-yyyy");
            }

            if ((User.IsInRole("Admin") || User.Identity.Name == "secretary" || User.Identity.Name == "A.secretary" || User.Identity.Name == "D.secretary" || User.Identity.Name == "chairman") && SearchString != null)
            {
                //return View(db.FileInformations.Where(x => (((String.IsNullOrEmpty(SearchString) || x.Filename.Contains(SearchString) || x.Filenumber.Contains(SearchString)
                //                                                     || x.Sectionoforigin.Contains(SearchString) || x.Addressee.Contains(SearchString) || x.Status.Contains(SearchString) ||
                //                                                     x.Subject.Contains(SearchString) || x.Receivedby.Contains(SearchString))
                //                                                     && (String.IsNullOrEmpty(uploadDate) || x.UploadDate.ToString().Contains(uploadDate))))).ToList().ToPagedList(pageNumber ?? 1, 10));

                return View(db.FileInformations.Where(x => (((String.IsNullOrEmpty(SearchString) || x.Filename.Contains(SearchString) || x.Filenumber.Contains(SearchString)
                                                     || x.Sectionoforigin.Contains(SearchString) || x.Addressee.Contains(SearchString) || x.Status.Contains(SearchString) || x.Subject.Contains(SearchString) 
                                                     || x.Type.Contains(SearchString))                
                
                                                     && (x.Receivedby.Contains(SearchSection))
                                                     && (String.IsNullOrEmpty(uploadDate) || x.UploadDate.ToString().Contains(uploadDate))))).ToList().ToPagedList(pageNumber ?? 1, 10));


            }

            if (User.IsInRole("Admin") || User.Identity.Name == "secretary" || User.Identity.Name == "A.secretary" || User.Identity.Name == "D.secretary" || User.Identity.Name == "chairman")
            {
                return View(db.FileInformations.ToList().ToPagedList(pageNumber ?? 1, 10));
            }

            if (SearchString != null)
            {

                return View(db.FileInformations.Where(x => ((String.IsNullOrEmpty(SearchString) || x.Filename.Contains(SearchString) || x.Filenumber.Contains(SearchString)
                                                                  || x.Sectionoforigin.Contains(SearchString) || x.Addressee.Contains(SearchString) || x.Status.Contains(SearchString) || x.Type.Contains(SearchString)
                                                                  || x.Subject.Contains(SearchString))
                                                                  && (x.Receivedby.Contains(User.Identity.Name))
                                                                  && (String.IsNullOrEmpty(uploadDate) || x.UploadDate.ToString().Contains(uploadDate)))).ToList().ToPagedList(pageNumber ?? 1, 10));

            }

            else
            {
                return View(db.FileInformations.Where(x => x.Receivedby.Contains(User.Identity.Name)).ToList().ToPagedList(pageNumber ?? 1, 10));
            }

        }
        // GET: FileInformation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileInformation fileInformation = db.FileInformations.Find(id);
            if (fileInformation == null)
            {
                return HttpNotFound();
            }
            return View(fileInformation);
        }

        // GET: FileInformation/Create
        public ActionResult Create()
        {
            ViewBag.departmentList = staticData.Deparment_list;
            ViewBag.Type = staticData.Type_List;
            return View();
        }

        // POST: FileInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UploadDate,Filename,Filenumber,Subject,Department,Type,Pages,Addressee,Sectionoforigin,Receivedby,Status,Pdfdirectory")] FileInformation fileInformation,
                                    HttpPostedFileBase file)
        {
            ViewBag.departmentList = staticData.Deparment_list;

            if (ModelState.IsValid)
            {
                db.FileInformations.Add(fileInformation);

                UploadFile(fileInformation, file);

                db.SaveChanges();
                TempData["success"] = "File Added Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        private void UploadFile(FileInformation fileInformation, HttpPostedFileBase file)
        {

            fileInformation.UploadDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

            int getid = 0;
            try
            {
                if (file.ContentLength > 0)
                {
                    getid = db.FileInformations.Select(x => x.Id).DefaultIfEmpty().Max();
                    string _FileName = Path.GetFileName(file.FileName);
                    getid++;

                    string extension = Path.GetExtension(file.FileName);
                    _FileName = getid + "_" + fileInformation.Filename + "_" + fileInformation.Status + "_"  + extension;
                    string _path = Path.Combine(Server.MapPath("~/Downloads/File"), _FileName);
                    string uploadedfilename;
                    uploadedfilename = _path;
                    fileInformation.Pdfdirectory = _FileName;

                    file.SaveAs(uploadedfilename);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        // GET: FileInformation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileInformation fileInformation = db.FileInformations.Find(id);
            if (fileInformation == null)
            {
                return HttpNotFound();
            }
            return View(fileInformation);
        }

        // POST: FileInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UploadDate,Filename,Filenumber,Subject,Department,Type,Pages,Addressee,Sectionoforigin,Receivedby,Status,Pdfdirectory")] FileInformation fileInformation, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileInformation).State = EntityState.Modified;
                ReUpload(fileInformation, File);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fileInformation);
        }
        private void ReUpload(FileInformation fileInformation, HttpPostedFileBase file)
        {

            int getid = 0;
            try
            {
                if (file.ContentLength > 0)
                {
                    getid = db.FileInformations.Select(x => x.Id).DefaultIfEmpty().Max();
                    string _FileName = Path.GetFileName(file.FileName);
                    getid++;

                    string extension = Path.GetExtension(file.FileName);
                    _FileName = getid + "_" + fileInformation.Filename + "_" + fileInformation.Status + "_" + extension;
                    string _path = Path.Combine(Server.MapPath("~/Downloads/File"), _FileName);
                    string uploadedfilename;
                    uploadedfilename = _path;
                    fileInformation.Pdfdirectory = _FileName;

                    file.SaveAs(uploadedfilename);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        // GET: FileInformation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileInformation fileInformation = db.FileInformations.Find(id);
            if (fileInformation == null)
            {
                return HttpNotFound();
            }
            return View(fileInformation);
        }

        // POST: FileInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileInformation fileInformation = db.FileInformations.Find(id);
            db.FileInformations.Remove(fileInformation);
            db.SaveChanges();
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

        public ActionResult Download(string dakkname)
        {
            string path = Server.MapPath("~/Downloads/File");

            byte[] fileBytes = System.IO.File.ReadAllBytes(path + @"\" + dakkname);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, dakkname);
        }
        public ActionResult Summary(string uploadDate, string uploadMonth)
        {
            if (!String.IsNullOrEmpty(uploadDate))
            {
                uploadDate = Convert.ToDateTime(uploadDate).Date.ToString("dd-MM-yyyy");
                return View(db.FileInformations.Where(x => (x.UploadDate.ToString().Contains(uploadDate))).ToList());
            }
            else if(!String.IsNullOrEmpty(uploadMonth))
            {
                uploadMonth = Convert.ToDateTime(uploadMonth).Date.ToString("MM-yyyy");
                return View(db.FileInformations.Where(x => (x.UploadDate.ToString().Contains(uploadMonth))).ToList());
            }
            else
            {
                return View(db.FileInformations.ToList());
            }
        }
       
    }
}
