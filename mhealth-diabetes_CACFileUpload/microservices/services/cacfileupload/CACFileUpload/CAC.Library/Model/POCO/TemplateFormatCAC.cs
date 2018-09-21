using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CAC.Library.Model.POCO
{
    [DataContract]
    public class TemplateFormatCAC
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public bool Nullable { get; set; }
        [DataMember]
        public string Format { get; set; }
        [DataMember]
        public string DefaultValue { get; set; }
        [DataMember]
        public bool Duplicate { get; set; }
        [DataMember]
        public List<TemplateSelectList> SelectList { get; set; }
        public override string ToString()
        {
            return $"{Name} {Type} {Nullable} {Format} {DefaultValue} {Duplicate}";
        }
    }
}