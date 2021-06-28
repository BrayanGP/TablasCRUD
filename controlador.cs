using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TablasCRUD
{
    class controlador
    {
        modelo m = new modelo();
        panelGeneral uno = new panelGeneral();
        
        public void MostrarTodos()
        {
            

            List<estadoAlumno> estatus = m.ConsultarLst();

            foreach (var esta in estatus)
            {
                Console.WriteLine($"\t{esta.id}\t {esta.clave}\t{esta.nombre}\t");
                
            }
        }
    }
}
