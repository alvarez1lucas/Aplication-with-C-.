
using LibreriaAURASOFT;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3AURASOFT.Entidades;

namespace TP3AURASOFT.Controladores
{
    internal class nAlumno
    {
        public static void ListarAlumnosPorCurso()
        {
            Console.WriteLine("Lista de Cursos Disponibles:");
            List<Curso> cursos = pCurso.getAll();

            foreach (Curso curso in cursos)
            {
                Console.WriteLine($"ID: {curso.Id}, Materia: {curso.Materia}, Año: {curso.Año}");
            }

            Console.Write("Ingresa el ID del curso para listar los alumnos inscritos: ");
            if (int.TryParse(Console.ReadLine(), out int selectedCursoId))
            {
                List<Alumno> alumnosInscritos = pAlumno.getAllCurso(selectedCursoId);

                Console.WriteLine($"Alumnos inscritos en el curso con ID {selectedCursoId}:");
                foreach (Alumno alumno in alumnosInscritos)
                {
                    Console.WriteLine($"ID ALUMNO: {alumno.Id}, NOMBRE: {alumno.Nombre}, APELLIDO: {alumno.Apellido}");
                }

                if (alumnosInscritos.Count == 0)
                {
                    Console.WriteLine("No hay alumnos inscritos en este curso.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Debe ingresar un número válido como ID de curso.");
            }

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public static void Menu()
        {
            string[] opciones = new string[] { "Listar Alumnos por curso", "Volver" };
            Console.Clear();
            Herramientas.DibujarMenu("LISTAR ALUMNOS", opciones);
            Console.Write("Seleccione una Opción: ");
            int seleccion = Herramientas.IngresoEntero(1, 3);


            Console.WriteLine();
            switch (seleccion)
            {
                case 1: ListarAlumnosPorCurso(); Menu(); break;
                case 2: break;
            }
        }
    }
}
