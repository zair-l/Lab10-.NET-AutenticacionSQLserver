using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UniversityDBEntities db = new UniversityDBEntities();
        public ActionResult Index() => View(db.Students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList());
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Student student = db.Students.SingleOrDefault(s => s.StudentID == id);
            return student == null ? (ActionResult)HttpNotFound() : View(student);
        }
        public ActionResult Create() => View(new Student { EnrollmentDate = System.DateTime.Today });
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstName,EnrollmentDate,Email")] Student student)
        {
            if (!ModelState.IsValid) return View(student);
            db.Students.AddObject(student); db.SaveChanges(); return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Student student = db.Students.SingleOrDefault(s => s.StudentID == id);
            return student == null ? (ActionResult)HttpNotFound() : View(student);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,LastName,FirstName,EnrollmentDate,Email")] Student student)
        {
            if (!ModelState.IsValid) return View(student);
            db.Students.Attach(student); db.ObjectStateManager.ChangeObjectState(student, EntityState.Modified);
            db.SaveChanges(); return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Student student = db.Students.SingleOrDefault(s => s.StudentID == id);
            return student == null ? (ActionResult)HttpNotFound() : View(student);
        }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.SingleOrDefault(s => s.StudentID == id);
            if (student == null) return HttpNotFound();
            db.Students.DeleteObject(student);
            try { db.SaveChanges(); return RedirectToAction("Index"); }
            catch (UpdateException) { ModelState.AddModelError("", "No se puede eliminar el estudiante porque tiene matrículas asociadas."); return View("Delete", student); }
        }
        protected override void Dispose(bool disposing) { if (disposing) db.Dispose(); base.Dispose(disposing); }
    }
}
