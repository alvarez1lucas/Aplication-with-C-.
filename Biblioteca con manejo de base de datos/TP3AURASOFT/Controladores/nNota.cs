using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3AURASOFT.Entidades;

namespace TP3AURASOFT.Controladores
{
    internal class nNota
    {
        public static double NotaEvaluacionAlumno(Evaluacion e, int idAlumno)
        {
            Nota nota = e.Notas.Find(n => n.IdAlumno == idAlumno);
            
            if(nota == null)
            {
                return -1;
            }

            else
            {
                return nota.Valor;
            }
        }
    }
}
