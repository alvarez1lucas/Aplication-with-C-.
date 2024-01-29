using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using TP3AURASOFT.Entidades;

namespace TP3AURASOFT.Controladores
{
    internal class pAlumno
    {
        public static List<Alumno> getAll()
        {
            List<Alumno> alumnos = new List<Alumno>();
            SQLiteCommand cmd = new SQLiteCommand("select IdAlumno, nombre, apellido from Alumno");
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();

            while (obdr.Read())
            {
                Alumno alumno = new Alumno();
                alumno.Id = obdr.GetInt32(0);
                alumno.Nombre = obdr.GetString(1);
                alumno.Apellido = obdr.GetString(2);
                alumnos.Add(alumno);
            }
            return alumnos;
        }

        public static Alumno getById(int id)
        {
            Alumno v = new Alumno();
            SQLiteCommand cmd = new SQLiteCommand("select IdAlumno, Nombre, Apellido from Alumno where IdAlumno = @IdAlumno");
            cmd.Parameters.Add(new SQLiteParameter("@IdAlumno", id));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                v.Id = obdr.GetInt32(0);
                v.Nombre = obdr.GetString(1);
                v.Apellido = obdr.GetString(2);

            }
            return v;
        }
        public static void Save(Alumno v)
        {
            SQLiteCommand cmd = new SQLiteCommand("insert into Alumno(nombre, apellido) values(@nombre, @apellido)");
            cmd.Parameters.Add(new SQLiteParameter("@nombre", v.Nombre));
            cmd.Parameters.Add(new SQLiteParameter("@apellido", v.Apellido));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }
        public static void Delete(Alumno v)
        {
            Console.WriteLine("Se va a eliminar al alumno con id: " + v.Id);
            Console.ReadKey(true);
            SQLiteCommand cmd = new SQLiteCommand("delete from Alumno where IdAlumno = @IdAlumno");
            cmd.Parameters.Add(new SQLiteParameter("@IdAlumno", v.Id));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }
        public static void Update(Alumno v)
        {
            SQLiteCommand cmd = new SQLiteCommand("UPDATE alumno SET nombre = @nombre, apellido = @apellido WHERE IdAlumno = @IdAlumno");
            cmd.Parameters.Add(new SQLiteParameter("@IdAlumno", v.Id));
            cmd.Parameters.Add(new SQLiteParameter("@nombre", v.Nombre));
            cmd.Parameters.Add(new SQLiteParameter("@apellido", v.Apellido));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }

        public static List <Alumno> getAllCurso(int idCurso)
        {
            List<Alumno> alumnos = new List<Alumno>();
            SQLiteCommand cmd = new SQLiteCommand("Select IdAlumno, IdCurso From Alumno Where IdCurso = @IdCurso");
            cmd.Parameters.Add(new SQLiteParameter("@IdCurso", idCurso));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                alumnos.Add(getById(obdr.GetInt32(0)));
            }
            return alumnos;
        }
    }
}
