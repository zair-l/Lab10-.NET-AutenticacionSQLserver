using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    [MetadataType(typeof(CourseMetadata))]
    public partial class Course { }
    internal sealed class CourseMetadata
    {
        [Key] public int CourseID { get; set; }
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(50)] [Display(Name = "Curso")]
        public string Title { get; set; }
        [Range(1, 6, ErrorMessage = "Los créditos deben estar entre 1 y 6.")]
        [Display(Name = "Créditos")]
        public int? Credits { get; set; }
    }

    [MetadataType(typeof(StudentMetadata))]
    public partial class Student { }
    internal sealed class StudentMetadata
    {
        [Key] public int StudentID { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50)] [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50)] [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "La fecha de matrícula es obligatoria.")]
        [DataType(DataType.Date)] [Display(Name = "Fecha de matrícula")]
        public DateTime? EnrollmentDate { get; set; }
        [EmailAddress(ErrorMessage = "Ingresa un correo electrónico válido.")]
        [StringLength(50)] [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }

    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment { }
    internal sealed class EnrollmentMetadata
    {
        [Key] public int EnrollmentID { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Selecciona un curso.")]
        [Display(Name = "Curso")]
        public int CourseID { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Selecciona un estudiante.")]
        [Display(Name = "Estudiante")]
        public int StudentID { get; set; }
        [Range(typeof(decimal), "0", "20", ErrorMessage = "La nota debe estar entre 0 y 20.")]
        [Display(Name = "Nota")]
        public decimal? Grade { get; set; }
    }
}

