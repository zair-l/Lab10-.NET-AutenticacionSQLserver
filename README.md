# Semana 10 — University

Aplicación ASP.NET MVC 5 para el laboratorio de mantenimiento con Entity Framework 6.5.2. Incluye CRUD de `Course`, `Student` y `Enrollment`. `Student` incorpora `Email` en alta, edición, detalle, lista y eliminación, con validación por Data Annotations.

## Abrir y ejecutar

1. Copia `University/ConnectionStrings.config.example` como `University/ConnectionStrings.config`, reemplaza el marcador por la contraseña local de `usrUniversity`, y abre `University.sln` en Visual Studio para ejecutar `University` con IIS Express. El archivo con la contraseña local está excluido de Git.
2. Ejecuta `Database/UniversityDB.sql` en SQL Server Management Studio para crear `UniversityDB`, las tres tablas y los datos iniciales, incluido `Email`.
3. En la instancia `(localdb)\MSSQLLocalDB`, ya está creado el login SQL `usrUniversity` con la contraseña de la guía, rol de servidor `sysadmin` y rol `db_owner` en `UniversityDB`. Para otra instancia, ejecuta `Database/SetupSqlLogin.sql.example` después de reemplazar el marcador de contraseña.
4. `University/Web.config` carga la conexión Entity Framework desde `University/ConnectionStrings.config`, que usa Autenticación de SQL Server con `usrUniversity`. Si cambias la contraseña o instancia, actualiza ese archivo local.
5. Si partes de una base anterior que aún no tiene la columna `Email`, ejecuta `Database/AddStudentEmail.sql`. No hace falta para una base nueva creada con `UniversityDB.sql`.
6. Si `UniversityDB` ya existía antes del cambio de precisión de las notas, ejecuta `Database/UpgradeEnrollmentGradePrecision.sql` para permitir notas de hasta 20.00.

## Modelo

`Models/UniversityModel.edmx` y los artefactos de Entity Framework se generaron desde `UniversityDB`. Las clases generadas mapean los tipos y nulabilidad del esquema suministrado.

## Capturas

Las capturas del navegador están en `Capturas/`:

- `01-Cursos.png` — lista de cursos.
- `02-Estudiantes.png` — estudiantes y correo electrónico.
- `03-Nuevo-estudiante.png` — formulario de estudiante.
- `04-Editar-estudiante-email.png`, `05-Detalle-estudiante-email.png`, `06-Eliminar-estudiante.png` — vistas de mantenimiento de estudiantes.
- `07-Validacion-correo.png` — validación de correo.
- `08-Nuevo-curso.png` — formulario de curso.
- `09-Matriculas.png`, `10-Nueva-matricula.png` — lista y formulario de matrículas.


