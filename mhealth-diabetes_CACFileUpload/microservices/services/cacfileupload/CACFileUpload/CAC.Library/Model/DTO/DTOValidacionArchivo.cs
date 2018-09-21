using System;
using System.Runtime.Serialization;

namespace CAC.Library.Model.DTO
{
    [DataContract]
    public class DTOValidacionArchivo
    {
        [DataMember]
        public string Fila { get; set; }
        [DataMember]
        public string Columna { get; set; }
        [DataMember]
        public string Celda { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string FechaCreacion { get; set; }
        [DataMember]
        public string Valor { get; set; }

        public override string ToString()
        {
            return $"{FechaCreacion}@{Fila}@{Columna}@{Celda}@{Valor}@{Descripcion}";
        }

    }
}
