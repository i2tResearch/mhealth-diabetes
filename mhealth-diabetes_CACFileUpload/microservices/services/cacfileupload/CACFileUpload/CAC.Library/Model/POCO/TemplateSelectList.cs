using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CAC.Library.Model.POCO
{
    [DataContract]
    public class TemplateSelectList
    {
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Value} {Description}";
        }
    }
}
