using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaPresentacion.Models;

namespace CapaPresentacion.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ContabilidadController : Controller
    {
        // Simulación de base de datos en memoria
        private static List<MovimientoViewModel> movimientos = new List<MovimientoViewModel>();

        public ActionResult Index()
        {
            var model = new ContabilidadViewModel
            {
                Movimientos = movimientos
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AgregarIngreso(decimal MontoIngreso, DateTime FechaIngreso, string DescripcionIngreso)
        {
            movimientos.Add(new MovimientoViewModel
            {
                Tipo = "Ingreso",
                Monto = MontoIngreso,
                Fecha = FechaIngreso,
                Descripcion = DescripcionIngreso
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AgregarEgreso(decimal MontoEgreso, DateTime FechaEgreso, string DescripcionEgreso)
        {
            movimientos.Add(new MovimientoViewModel
            {
                Tipo = "Egreso",
                Monto = MontoEgreso,
                Fecha = FechaEgreso,
                Descripcion = DescripcionEgreso
            });

            return RedirectToAction("Index");
        }

        public ActionResult CalculoSalarios()
        {
            var empleados = ObtenerEmpleados(); // Simulado
            var lista = new List<SalarioCalculadoViewModel>();

            foreach (var emp in empleados)
            {
                // Solo tomamos las incapacidades aceptadas
                int diasNoTrabajados = emp.Incapacidades != null
                    ? emp.Incapacidades.Count(i => i.Estado == "Aceptada")
                    : 0;

                decimal diario = emp.Salario / 30;
                decimal descuento = diario * diasNoTrabajados;
                decimal salarioNeto = emp.Salario - descuento;

                lista.Add(new SalarioCalculadoViewModel
                {
                    NombreEmpleado = emp.Nombre,
                    SalarioBase = emp.Salario,
                    DiasNoTrabajados = diasNoTrabajados,
                    Descuento = descuento,
                    SalarioNeto = salarioNeto
                });
            }

            return View(lista);
        }

        // Simulación: Empleados con salarios y posibles incapacidades
        private List<Empleado> ObtenerEmpleados()
        {
            return new List<Empleado>
            {
                new Empleado
                {
                    Nombre = "Alex",
                    Salario = 500000,
                    Incapacidades = new List<Incapacidad>
                    {
                        new Incapacidad { Fecha = DateTime.Today.AddDays(-3), Estado = "Aceptada" },
                        new Incapacidad { Fecha = DateTime.Today.AddDays(-1), Estado = "Pendiente" }
                    }
                },
                
            };
        }
    }
}
