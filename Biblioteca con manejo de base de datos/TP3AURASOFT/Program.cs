

using System.ComponentModel.Design;
using TP3AURASOFT.Entidades;
using LibreriaAURASOFT;
using TP3AURASOFT.Controladores;
using System.Data.SQLite;

namespace TP3AURASOFT
{
    internal class Program
    {
        public static List<Alumno> alumnos;
        public static List<Curso> cursos;
        public static List<Evaluacion> evaluaciones;
        public static List<Nota> nota;
        static void Main(string[] args)
        {
            nota = new List<Nota>();
            evaluaciones = new List<Evaluacion>();
            cursos = new List<Curso>();
            alumnos = new List<Alumno>();

            Conexion.OpenConexion();
            Datos();
            Menu();
            Conexion.CloseConexion();
            
            
        }

        public static void Menu() {
            Console.Clear();
            Console.WriteLine("Bienvenido a la administración de playas de cursado de materias");
            string[] opciones = new string[] { "Agregar Evaluación","Alumnos", "Generar reporte", "Cargar notas","Salir" };
            Herramientas.DibujarMenu("Menú Principal", opciones);
            Console.Write("Seleccione Opción: ");
            int seleccion = Herramientas.IngresoEntero(1, opciones.Length);
            switch (seleccion)
            {
                case 1: nCurso.AgregarEvaluacion(nota); Menu(); break;
                case 2: nAlumno.Menu(); Menu(); break;
                case 3: nCurso.GenerarReporte(); Menu(); break;
                case 4: nCurso.CargarNotasEvaluacion(Program.cursos, Program.evaluaciones, nota); Menu(); break;
                case 5: break;
            }
        }
        public static void Datos()
        {
            cursos = pCurso.getAll();        
        }
    }
}