using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TP3AURASOFT.Entidades;

namespace TP3AURASOFT.Controladores
{
    internal class pEvaluacion
    {
        public static List<Evaluacion> getAll()
        {
            List<Evaluacion> evaluaciones = new List<Evaluacion>();
            SQLiteCommand cmd = new SQLiteCommand("select IdEvaluacion, Titulo, Fecha, TipoEvaluacion, Ponderacion from Evaluacion");
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                Evaluacion evaluacion = new Evaluacion();
                evaluacion.Id = obdr.GetInt32(0);
                evaluacion.Titulo = obdr.GetString(1);
                evaluacion.Fecha = obdr.GetString(2);
                evaluacion.Tipo = obdr.GetString(3);
                evaluacion.Ponderacion = obdr.GetInt32(4);
                evaluacion.Notas = pNota.getAllEvaluaciones(evaluacion.Id);
                evaluaciones.Add(evaluacion);
            }
            return evaluaciones;
        }

        public static Evaluacion getById(int id)
        {
            Evaluacion v = new Evaluacion();
            SQLiteCommand cmd = new SQLiteCommand("select IdEvaluacion, Titulo, Fecha, TipoEvaluacion, Ponderacion from Evaluacion where IdEvaluacion = @IdEvaluacion");
            cmd.Parameters.Add(new SQLiteParameter("@IdEvaluacion", id));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                v.Id = obdr.GetInt32(0);
                v.Titulo = obdr.GetString(1);
                v.Fecha = obdr.GetString(2); 
                v.Tipo = obdr.GetString(3);
                v.Ponderacion = obdr.GetInt32(4);
            }
            return v;
        }

        public static void Save(Evaluacion v, Curso c)
        {
            SQLiteCommand cmd = new SQLiteCommand("insert into Evaluacion(Titulo, Fecha, TipoEvaluacion, Ponderacion, IdCurso) values(@Titulo, @Fecha, @TipoEvaluacion, @Ponderacion, @IdCurso)");
            cmd.Parameters.Add(new SQLiteParameter("@Titulo", v.Titulo));
            cmd.Parameters.Add(new SQLiteParameter("@Fecha", v.Fecha));
            cmd.Parameters.Add(new SQLiteParameter("@TipoEvaluacion", v.Tipo));
            cmd.Parameters.Add(new SQLiteParameter("@Ponderacion", v.Ponderacion));
            cmd.Parameters.Add(new SQLiteParameter("@IdCurso", c.Id)); // CLAVE FORANEA 
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }

        public static void Delete(Evaluacion v)
        {
            Console.WriteLine("Se va a eliminar al Evaluacion con id: " + v.Id);
            Console.ReadKey(true);
            SQLiteCommand cmd = new SQLiteCommand("delete from Evaluacion where IdEvaluacion = @IdEvaluacion");
            cmd.Parameters.Add(new SQLiteParameter("@IdEvaluacion", v.Id));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }

        public static void Update(Evaluacion v)
        {
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Evaluacion SET titulo = @titulo, fecha = @fecha, TipoEvaluacion = @TipoEvaluacion, Ponderacion = @Ponderacion WHERE IdEvaluacion = @IdEvaluacion");
            cmd.Parameters.Add(new SQLiteParameter("@IdEvaluacion", v.Id));
            cmd.Parameters.Add(new SQLiteParameter("@Titulo", v.Titulo));
            cmd.Parameters.Add(new SQLiteParameter("@Fecha", v.Fecha));
            cmd.Parameters.Add(new SQLiteParameter("@TipoEvaluacion", v.Tipo));
            cmd.Parameters.Add(new SQLiteParameter("@Ponderacion", v.Ponderacion));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }
        public static List<Evaluacion> getAllCurso(int idCurso)
        {
            List<Evaluacion> evaluaciones = new List<Evaluacion>();
            SQLiteCommand cmd = new SQLiteCommand("Select IdEvaluacion, IdCurso From Evaluacion Where IdCurso = @IdCurso");
            cmd.Parameters.Add(new SQLiteParameter("@IdCurso", idCurso));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                Evaluacion evaluacion = getById(obdr.GetInt32(0));
                evaluacion.Notas = pNota.getAllEvaluaciones(evaluacion.Id);
                evaluaciones.Add(evaluacion);
            }
            return evaluaciones;
        }

    }
}
