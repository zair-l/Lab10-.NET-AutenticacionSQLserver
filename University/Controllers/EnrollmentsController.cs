using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly UniversityDBEntities db = new UniversityDBEntities();
        public ActionResult Index()
        {
            return View(db.Enrollments.Include("Course").Include("Student")
                .OrderBy(e => e.Student.LastName).ThenBy(e => e.Course.Title).ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Enrollment enrollment = db.Enrollments.Include("Course").Include("Student").SingleOrDefault(e => e.EnrollmentID == id);
            return enrollment == null ? (ActionResult)HttpNotFound() : View(enrollment);
        }
        public ActionResult Create() { PopulateLists(); return View(); }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,StudentID,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid) { db.Enrollments.AddObject(enrollment); db.SaveChanges(); return RedirectToAction("Index"); }
            PopulateLists(enrollment.CourseID, enrollment.StudentID); return View(enrollment);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Enrollment enrollment = db.Enrollments.SingleOrDefault(e => e.EnrollmentID == id);
            if (enrollment == null) return HttpNotFound();
            PopulateLists(enrollment.CourseID, enrollment.StudentID); return View(enrollment);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Attach(enrollment); db.ObjectStateManager.ChangeObjectState(enrollment, EntityState.Modified);
                db.SaveChanges(); return RedirectToAction("Index");
            }
            PopulateLists(enrollment.CourseID, enrollment.StudentID); return View(enrollment);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Enrollment enrollment = db.Enrollments.Include("Course").Include("Student").SingleOrDefault(e => e.EnrollmentID == id);
            return enrollment == null ? (ActionResult)HttpNotFound() : View(enrollment);
        }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.SingleOrDefault(e => e.EnrollmentID == id);
            if (enrollment != null) { db.Enrollments.DeleteObject(enrollment); db.SaveChanges(); }
            return RedirectToAction("Index");
        }
        private void PopulateLists(object selectedCourse = null, object selectedStudent = null)
        {
            ViewBag.CourseID = new SelectList(db.Courses.OrderBy(c => c.Title), "CourseID", "Title", selectedCourse);
            ViewBag.StudentID = new SelectList(db.Students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
                .Select(s => new { s.StudentID, FullName = s.LastName + ", " + s.FirstName }), "StudentID", "FullName", selectedStudent);
        }
        protected override void Dispose(bool disposing) { if (disposing) db.Dispose(); base.Dispose(disposing); }
    }
}
