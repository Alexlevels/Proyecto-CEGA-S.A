using System.ComponentModel;

namespace SistemaReportesIncapacidades.Models
{
    public enum TipoIncapacidad
    {
        [Description("Enfermedad General")]
        Enfermedad = 1,

        [Description("Accidente Laboral")]
        AccidenteLaboral = 2,

        [Description("Accidente de Tránsito")]
        AccidenteTransito = 3,

        [Description("Licencia de Maternidad")]
        Maternidad = 4,

        [Description("Licencia de Paternidad")]
        Paternidad = 5,

        [Description("Enfermedad Profesional")]
        EnfermedadProfesional = 6,

        [Description("Riesgo de Embarazo")]
        RiesgoEmbarazo = 7,

        [Description("Otros")]
        Otros = 99
    }
}