namespace Indicadores.Library.Model.DTO
{
    public class DTOIndicadorTotales
    {
        public int Mes { get; set; }
        public int PacientesEstadios { get; set; }
        public int PacientesEstudiados { get; set; }
        public int PacientesControlados { get; set; }
        public int NoMedidos { get; set; }
        public int VigentesControlados { get; set; }
        public int VigentesDescontrolados { get; set; }
    }
}
