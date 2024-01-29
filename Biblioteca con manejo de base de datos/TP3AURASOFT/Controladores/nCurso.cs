
using LibreriaAURASOFT;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3AURASOFT.Entidades;

namespace TP3AURASOFT.Controladores
{
    internal class nCurso
    {
        //public static List<nCurso> cursos = new List<nCurso>();
        //Parece que es incorrecto, deberia tener una lista de objetos 
        //una lista de objetos Curso 

        //public static List<pEvaluacion> evaluacions = pEvaluacion.getAll();
        public static void Imprimir()
        {
            Console.WriteLine();
            foreach (Curso c in Program.cursos)
            {
                Console.WriteLine($"{c.Id} - {c.Materia}");
            }
        }

        public static int Seleccionar()
        {
            Console.WriteLine();
            Imprimir();
            Console.Write("Selccione un curso: ");
            int s = Herramientas.IngresoEntero(1, Program.cursos.Count);
            return s - 1;
        }


        public static void AgregarEvaluacion(List<Nota> notas)
        {
            Console.WriteLine();
            Console.WriteLine("Lista de cursos disponibles:");

            foreach (Curso curso in Program.cursos)
            {
                Console.WriteLine(curso.Id + " - " + curso.Materia + " - " + curso.Año);
            }

            Console.WriteLine("Ingrese el ID del curso: ");
            int cursoId = Herramientas.IngresoEntero();
            Curso cursoSeleccionado = Program.cursos.Find(c => c.Id == cursoId);

            if (cursoSeleccionado != null)
            {
                Console.WriteLine("Curso seleccionado: " + cursoSeleccionado.Materia + " - " + cursoSeleccionado.Año);
                Console.WriteLine("===============================");

                // Listar evaluaciones existentes
                //List<Evaluacion> evaluaciones = pEvaluacion.getAll();
                List<Evaluacion> evaluaciones = cursoSeleccionado.Evaluaciones;

                if (evaluaciones.Count > 0)
                {
                    Console.WriteLine("Evaluaciones existentes:");
                    foreach (Evaluacion evaluacion in evaluaciones)
                    {
                        Console.WriteLine($"ID: {evaluacion.Id}");
                        Console.WriteLine($"Título: {evaluacion.Titulo}");
                        Console.WriteLine($"Fecha: {evaluacion.Fecha:dd/MM/yyyy}");
                        Console.WriteLine($"Tipo: {evaluacion.Tipo}");
                        Console.WriteLine($"Ponderación: {evaluacion.Ponderacion}%");
                        Console.WriteLine("===============================");
                    }
                }
                else
                {
                    Console.WriteLine("No existen evaluaciones registradas en este curso.");
                }

                Evaluacion e = new Evaluacion();
                Console.WriteLine();

                e.Id = -1;

                Console.Write("Ingrese el Título de la Evaluación: ");
                e.Titulo = Herramientas.StringNoNulo();
                Console.WriteLine();

                Console.WriteLine("Ingrese la fecha de la evaluación");
                e.Fecha = Herramientas.IngresarFecha();   
                Console.WriteLine();

                Console.Write("Ingrese tipo de Evaluación: ");
                e.Tipo = Herramientas.StringNoNulo();
                Console.WriteLine();

                Console.Write($"Ingrese ponderación de la Evaluación (ponderación total del curso: % {cursoSeleccionado.Evaluaciones.Sum(ev => ev.Ponderacion)}): ");
                e.Ponderacion = Herramientas.IngresoEntero(1, 100);
                Console.WriteLine();

                pEvaluacion.Save(e, cursoSeleccionado);
                Program.cursos = pCurso.getAll();
            }
            else
            {
                Console.WriteLine("El ID ingresado no corresponde a un curso válido.");
            }
        }

        public static void CargarNotasEvaluacion(List<Curso> cursos, List<Evaluacion> evaluaciones, List<Nota> notas)
        {

            Console.WriteLine("Elija el número de curso para cargar notas: ");
            foreach (Curso curso in cursos)
            {
                Console.WriteLine($"{curso.Id} - {curso.Materia} - {curso.Año}");
            }
            int cursoId = Herramientas.IngresoEntero();
            Curso cursoSeleccionado = cursos.Find(c => c.Id == cursoId);

            if (cursoSeleccionado != null)
            {
                Console.WriteLine("Elija el número de evaluación para cargar notas: ");
                foreach (Evaluacion evaluacion in cursoSeleccionado.Evaluaciones)
                {
                    Console.WriteLine($"{evaluacion.Id} - {evaluacion.Titulo}");
                }
                int idEvaluacion = Herramientas.IngresoEntero();
                Evaluacion evaluacionSeleccionada = cursoSeleccionado.Evaluaciones.Find(e => e.Id == idEvaluacion);

                if (evaluacionSeleccionada != null)
                {
                    foreach (Alumno alumno in cursoSeleccionado.Alumnos)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Ingrese la nota de {alumno.Nombre} {alumno.Apellido} para la evaluación {evaluacionSeleccionada.Titulo} (del 1 al 10): ");
                        double nota = Herramientas.IngresoDouble();
                        Nota nuevaNota = new Nota(alumno.Id, evaluacionSeleccionada.Id, nota); 
                        pNota.Save(nuevaNota);
                        Program.cursos = pCurso.getAll();
                    }
                }
                else
                {
                    Console.WriteLine("El ID de la evaluación ingresado no corresponde a una evaluación válida.");
                }
            }
            else
            {
                Console.WriteLine("El ID del curso ingresado no corresponde a un curso válido.");
            }
        }

        public static void GenerarReporte()
        {
            // Seleccionamos un curso
            Curso curso = Program.cursos[Seleccionar()];

            // Creamos una matriz con la cantidad de filas definida por la cantidad de alumnos que hayan
            // en el curso elegido y la cantidad de columnas según la información que se pide por cada uno 

            string[,] reporte = new string[curso.Alumnos.Count, 4 + (curso.Evaluaciones.Count)];

            // Creamos una lista para los titulos de la tabla
            List<string> titulos = new List<string> { };

            titulos.Add("ID");
            titulos.Add("Apellido");
            titulos.Add("Nombre");

            foreach (Evaluacion e in curso.Evaluaciones)
            {
                titulos.Add($"{e.Titulo} ({e.Ponderacion}%)");
            }

            titulos.Add("Nota final");
            string[] titulosArray = titulos.ToArray();

            // Bucle para ir agregando la información necesaria a la matriz del reporte
            for (int i = 0; i < reporte.GetLength(0); i++)
            {
                // Vamos recorriendo la matriz del reporte horizontalmente con la variable j, agregando la información 
                // de cada alumno.

                int j = 0;

                reporte[i, j] = curso.Alumnos[i].Id.ToString();
                reporte[i, j + 1] = curso.Alumnos[i].Apellido;
                reporte[i, j + 2] = curso.Alumnos[i].Nombre;

                // Fijamos el valor de j en 3, despues de guardar los primeros 3 datos, para ingresar al bucle que recorre 
                // la lista de evaluaciones del curso
                j += 3;

                for (int k = 0; k < curso.Evaluaciones.Count; k++)
                {
                    // El valor fijo de j lo vamos sumando a k para seguir avanzando por las columnas de la tabla, y a k sola
                    // la usamos para recorrer la lista de evaluaciones. 
                    // Conseguimos la nota mediante el método NotaEvaluacionAlumno. Si no hay nota, este devolverá -1. Guardamos este resultado
                    // en una variable y hacemos las comparaciones necesarias antes de guardarla.

                    double nota = nNota.NotaEvaluacionAlumno(curso.Evaluaciones[k], curso.Alumnos[i].Id);

                    if (nota == -1)
                    {
                        reporte[i, j + k] = "N/D";
                    }

                    else
                    {
                        reporte[i, j + k] = nota.ToString();
                    }
                }

                // Al valor de j se le suma a su valor (que no se modifico dentro del anterior bucle) la cantidad de evaluaciones 
                // del curso.

                j += curso.Evaluaciones.Count;

                // Y se guarda en la ultima columna la nota final, que se obtiene con el método CalcularNotaFinal()

                //reporte[i, j] = "N/D";

                double notaFinal = CalcularNotaFinal(curso.Evaluaciones, curso.Alumnos[i]);

                if (notaFinal == -1 || notaFinal == 0)
                {
                    reporte[i, j] = "N/D";
                }

                else
                {
                    reporte[i, j] = notaFinal.ToString();
                }
            }

            Herramientas.ImprimirTabla(titulosArray, reporte);
            Console.ReadKey(true);

        }
        public static double CalcularNotaFinal(List<Evaluacion> evaluaciones, Alumno alumno)
        {
            // Primero se comprueba que hayan evaluaciones en el curso
            if (evaluaciones.Count > 0)
            {
                double notaFinal = 0;
                foreach (Evaluacion e in evaluaciones)
                {
                    // Recorremos la lista de evaluaciones del curso elegido y
                    // buscamos la nota que le corresponde al alumno. La agregamos a la
                    // nota final con su ponderación corrspondiente.

                    Nota nota = e.Notas.Find(n => n.IdAlumno == alumno.Id);

                    if (nota != null)
                    {
                        notaFinal += nota.Valor * (e.Ponderacion / 100.0);
                    }
                }
                return Math.Round(notaFinal, 2);
            }
            else
            {
                //Si no hay evaluaciones, se devuelve un valor por defecto
                return -1;
            }
        }
    }
}


