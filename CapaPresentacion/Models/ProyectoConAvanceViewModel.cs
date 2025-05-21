using System;
using System.Collections.Generic;

namespace CapaPresentacion.Models
{
    public class ProyectoConAvanceViewModel
    {
        public Proyecto Proyecto { get; set; }
        public int Porcentaje { get; set; }  // % de avance según tareas completadas
    }
}
