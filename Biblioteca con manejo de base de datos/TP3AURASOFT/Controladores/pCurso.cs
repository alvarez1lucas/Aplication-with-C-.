using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3AURASOFT.Entidades;

namespace TP3AURASOFT.Controladores
{
    internal class pCurso
    {
        public static List<Curso> getAll()
        {
            List<Curso> Cursos = new List<Curso>();
            SQLiteCommand cmd = new SQLiteCommand("select IdCurso, Materia, Año from Curso");
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();

            while (obdr.Read())
            {
                Curso curso = new Curso();
                curso.Id = obdr.GetInt32(0);
                curso.Materia = obdr.GetString(1);
                curso.Año = obdr.GetInt32(2);
                curso.Alumnos = pAlumno.getAllCurso(curso.Id);
                curso.Evaluaciones = pEvaluacion.getAllCurso(curso.Id);
                Cursos.Add(curso);
            }
            return Cursos;
        }
        public static Curso getById(int id)
        {
            Curso v = new Curso();
            SQLiteCommand cmd = new SQLiteCommand("select IdCurso, materia, año from Curso where IdCurso = @IdCurso");
            cmd.Parameters.Add(new SQLiteParameter("@IdCurso", id));
            //cmd.Parameters.Add(new SQLiteParameter("@materia", v.Materia));
            //cmd.Parameters.Add(new SQLiteParameter("@año", v.Año));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                v.Id = obdr.GetInt32(0);
                v.Materia = obdr.GetString(1);
                v.Año = obdr.GetInt32(2);

            }
            return v;
        }
        public static void Save(Curso v)
        {
            SQLiteCommand cmd = new SQLiteCommand("insert into Curso(Materia, Año) values(@Materia, @Año)");
            cmd.Parameters.Add(new SQLiteParameter("@Materia", v.Materia));
            cmd.Parameters.Add(new SQLiteParameter("@Año", v.Año));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }

        public static void Delete(Curso v)
        {
            Console.WriteLine("Se va a eliminar el curso con id: " + v.Id);
            Console.ReadKey(true);
            SQLiteCommand cmd = new SQLiteCommand("delete from curso where id = @id");
            cmd.Parameters.Add(new SQLiteParameter("@id", v.Id));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }
        public static void Update(Curso v)
        {
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Curso SET Materia = @materia, Año = @año");
            //cmd.Parameters.Add(new SQLiteParameter("@id", v.Id));
            cmd.Parameters.Add(new SQLiteParameter("@materia", v.Materia));
            cmd.Parameters.Add(new SQLiteParameter("@año", v.Año));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }
    }
}
