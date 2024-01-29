using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3AURASOFT.Entidades;

namespace TP3AURASOFT.Controladores
{
    internal class nEvaluacion
    {
        public static void Imprimir(Curso curso)
        {
            Console.WriteLine();
            foreach (Evaluacion e in curso.Evaluaciones)
            {
                Console.WriteLine($"{e.Id} - {e.Titulo}");
            }
        }
    }
}
