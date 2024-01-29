using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3AURASOFT.Entidades
{
    internal class Nota
    {
        //public List<Alumno> AlumnoId { get; set; }
        public int IdAlumno { get; set; }
        //public List<Evaluacion> EvaluacionId { get; set; }
        public int IdEvaluacion { get; set; }
        public double Valor { get; set; }

        public Nota()
        {
          
        }

        public Nota(int alumnoId, int evaluacionId, double valor)
        {
            Valor = valor;
            IdAlumno = alumnoId;
            IdEvaluacion = evaluacionId;
        }
    }
}
