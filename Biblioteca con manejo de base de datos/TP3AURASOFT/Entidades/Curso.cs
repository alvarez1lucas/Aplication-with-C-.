using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3AURASOFT.Entidades
{
    internal class Curso
    {
        public int Id { get; set; }
        public string Materia { get; set; }
        public int Año { get; set; }
        public List<Alumno> Alumnos { get; set; }
        public List<Evaluacion> Evaluaciones { get; set; }

        public Curso() {
            Alumnos = new List<Alumno>();
            Evaluaciones = new List<Evaluacion>();
        }

        public Curso(int id, string materia, int año)
        {
            Id = id;
            Materia = materia;
            Año = año;
            Alumnos = new List<Alumno>();
            Evaluaciones = new List<Evaluacion>();
        }
    }
}
