using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3AURASOFT.Entidades
{
    internal class Evaluacion
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Fecha { get; set; }
        public string Tipo { get; set; }
        public int Ponderacion { get; set; }
        //public string Tipo { get; set; }
        //public List<Alumno> Notas { get; set; }
        public List<Nota> Notas { get; set; }
        public Evaluacion() {
            Notas = new List<Nota>();
        }
        public Evaluacion(int id, string titulo, string fecha, string tipo, int ponderacion)
        {
            Id = id;
            Titulo = titulo;
            Fecha = fecha;
            Tipo = tipo;
            Ponderacion = ponderacion;
            Notas = new List<Nota>();
        }
    }
}
