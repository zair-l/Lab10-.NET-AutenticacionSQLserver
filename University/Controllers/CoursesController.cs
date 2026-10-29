using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UniversityDBEntities db = new UniversityDBEntities();
        public ActionResult Index() => View(db.Courses.OrderBy(c => c.Title).ToList());
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Course course = db.Courses.SingleOrDefault(c => c.CourseID == id);
            return course == null ? (ActionResult)HttpNotFound() : View(course);
        }
        public ActionResult Create() => View();
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Credits")] Course course)
        {
            if (!ModelState.IsValid) return View(course);
            db.Courses.AddObject(course); db.SaveChanges(); return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Course course = db.Courses.SingleOrDefault(c => c.CourseID == id);
            return course == null ? (ActionResult)HttpNotFound() : View(course);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,Title,Credits")] Course course)
        {
            if (!ModelState.IsValid) return View(course);
            db.Courses.Attach(course); db.ObjectStateManager.ChangeObjectState(course, EntityState.Modified);
            db.SaveChanges(); return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Course course = db.Courses.SingleOrDefault(c => c.CourseID == id);
            return course == null ? (ActionResult)HttpNotFound() : View(course);
        }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.SingleOrDefault(c => c.CourseID == id);
            if (course == null) return HttpNotFound();
            db.Courses.DeleteObject(course);
            try { db.SaveChanges(); return RedirectToAction("Index"); }
            catch (UpdateException) { ModelState.AddModelError("", "No se puede eliminar el curso porque tiene matrículas asociadas."); return View("Delete", course); }
        }
        protected override void Dispose(bool disposing) { if (disposing) db.Dispose(); base.Dispose(disposing); }
    }
}
