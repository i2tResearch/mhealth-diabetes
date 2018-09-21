using System;

namespace CAC.Library.Model.POCO
{
    /// <summary>
    /// Clase que representa el archivo JSON de reglas de pacientes prioritarios
    /// </summary>
    public class TemplateRulesPriorityPatient
    {

        public string Name { get; set; }

        public string Type { get; set; }

        public string AllowValues { get; set; }

        public string NotApply { get; set; }

        public string UnknowValue { get; set; }

        public bool ValidateOutdated { get; set; }

        public int MonthOutdated { get; set; }

        public RangeMin[] LstRangeMin { get; set; }

        public RangeHigher[] LstRangeHigher { get; set; }

        public RangeEq[] LstRangeEq { get; set; }

        public RangeDif[] LstRangeDif { get; set; }

        public override string ToString()
        {
            return $"{Name}@{Type}@{AllowValues}@{UnknowValue}@{ValidateOutdated}@{MonthOutdated}";
        }

    }

    public class RangeEq
    {
        public String Equal { get; set; }

        public int Column { get; set; }

        public String ValueColumnEq { get; set; }

        public String Different { get; set; }

        public String HigEq { get; set; }

        public string MinEq { get; set; }

        public override string ToString()
        {
            return $"{Equal}@{Column}@{ValueColumnEq}@{Different}@{HigEq}@{MinEq}";
        }
    }

    public class RangeMin
    {
        public string MinEq { get; set; }

        public int Column { get; set; }

        public String ValueColumnEq { get; set; }

        public override string ToString()
        {
            return $"{Column}@{ValueColumnEq}@{MinEq}";
        }
    }

    public class RangeHigher
    {
        public string HigEq { get; set; }

        public int Column { get; set; }

        public String ValueColumnEq { get; set; }

        public override string ToString()
        {
            return $"{HigEq}@{Column}@{ValueColumnEq}";
        }
    }

    public class RangeDif
    {
        public String Dif { get; set; }

        public String Column { get; set; }

        public String ValueColumnEq { get; set; }

        public String Different { get; set; }

        public String HigEq { get; set; }

        public string MinEq { get; set; }

        public override string ToString()
        {
            return $"{Dif}@{Column}@{ValueColumnEq}@{Different}@{HigEq}@{MinEq}";
        }
    }

    public class TempRulesPriorityPatientCollection
    {
        public TemplateRulesPriorityPatient[] collection { get; set; }
    }
}
