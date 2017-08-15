using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentAttendanceSystem.Controllers
{
    public class HomeController : Controller
    {

        StudentAttendanceEntities1 db = new StudentAttendanceEntities1();
        public ActionResult Index()
        {
            var Students = db.Students.ToList();
            ViewBag.Students = Students;
            return View();
        }
        [HttpPost]
        public ActionResult Index(int[] markedStudentId, DateTime attendanceDate)
        {

            int[] mark = markedStudentId;
            var checkdate = db.Attendances.Where(c => c.Date == attendanceDate).FirstOrDefault();
            if (checkdate == null)
            {
                foreach (var id in mark)
                {
                    Attendance at = new Attendance();
                    at.StudentId = id;
                    at.Date = attendanceDate;
                    db.Attendances.Add(at);
                    db.SaveChanges();
                    TempData["message"] = "Attendance marked for " + attendanceDate.ToString("d");
                }
            }
            else
            {
                TempData["message"] = "Sorry Attendance already marked for " + attendanceDate.ToString("d");
            }

            var Students = db.Students.ToList();
            ViewBag.Students = Students;

            return View();
        }

    }
}
