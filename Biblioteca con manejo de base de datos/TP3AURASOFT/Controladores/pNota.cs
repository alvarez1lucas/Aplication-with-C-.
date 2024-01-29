using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3AURASOFT.Entidades;

namespace TP3AURASOFT.Controladores
{
    internal class pNota
    {
        public static List<Nota> getAll()
        {
            List<Nota> notas = new List<Nota>();
            SQLiteCommand cmd = new SQLiteCommand("select IdAlumno, IdEvaluacion, Valor  from Nota"); // selecciono las columas de la tabla Nota
            cmd.Connection = Conexion.Connection; // para que la consulta se haga en la base de datos
            SQLiteDataReader obdr = cmd.ExecuteReader(); // se ejecuta la consulta con esto, lo que devuelve un Objeto SQLiteDataReader

            while (obdr.Read())
            {
                Nota nota = new Nota();
                nota.IdAlumno = obdr.GetInt32(0);
                nota.IdEvaluacion = obdr.GetInt32(1);
                nota.Valor = obdr.GetDouble(2);
                notas.Add(nota);
            }
            return notas;
        }
        public static Nota getById(int IdAlumno, int IdEvaluacion)
        {
            Nota v = new Nota();
            SQLiteCommand cmd = new SQLiteCommand("SELECT IdAlumno, IdEvaluacion, Valor FROM Nota WHERE IdAlumno = @IdAlumno AND IdEvaluacion = @IdEvaluacion");
            cmd.Parameters.Add(new SQLiteParameter("@IdAlumno", IdAlumno));
            cmd.Parameters.Add(new SQLiteParameter("@IdEvaluacion", IdEvaluacion));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                v.IdAlumno = obdr.GetInt32(0);
                v.IdEvaluacion = obdr.GetInt32(1);
                v.Valor = obdr.GetDouble(2);
            }
            return v;
        }
        public static void Save(Nota v)
        {
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Nota (IdEvaluacion, IdAlumno, Valor) VALUES (@IdEvaluacion, @IdAlumno, @Valor)");
            cmd.Parameters.Add(new SQLiteParameter("@IdEvaluacion", v.IdEvaluacion));
            cmd.Parameters.Add(new SQLiteParameter("@IdAlumno", v.IdAlumno));
            cmd.Parameters.Add(new SQLiteParameter("@Valor", v.Valor));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }
        public static void Delete(Nota v) 
        {
            Console.WriteLine("Se va a eliminar al Nota con IdAlumno: " + v.IdAlumno + "y IdEvaluacion " +  v.IdEvaluacion);
            Console.ReadKey(true);
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Nota WHERE IdAlumno = @IdAlumno AND IdEvaluacion = @IdEvaluacion");
            cmd.Parameters.Add(new SQLiteParameter("@IdAlumno", v.IdAlumno));
            cmd.Parameters.Add(new SQLiteParameter("@IdEvaluacion", v.IdEvaluacion));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }
        public static void Update(Nota v)
        {
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Nota SET Valor = @Valor  WHERE IdAlumno = @IdAlumno AND IdEvaluacion = @IdEvaluacion");
            cmd.Parameters.Add(new SQLiteParameter("@IdAlumno", v.IdAlumno));
            cmd.Parameters.Add(new SQLiteParameter("@IdEvaluacion", v.IdEvaluacion));
            cmd.Parameters.Add(new SQLiteParameter("@Valor", v.Valor));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }

        public static List<Nota> getAllEvaluaciones (int idEvaluaciones)
        {
            List<Nota> notas = new List<Nota>();
            SQLiteCommand cmd = new SQLiteCommand("Select IdAlumno, IdEvaluacion From Nota Where IdEvaluacion = @IdEvaluacion ");
            cmd.Parameters.Add(new SQLiteParameter("@IdEvaluacion", idEvaluaciones));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                notas.Add(getById(obdr.GetInt32(0), obdr.GetInt32(1)));
            }
            return notas;
        }
    }
}
