//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TCC_Clinica_Medica
{
    using System;
    using System.Collections.Generic;
    
    public partial class MEDICO_ESPECIALIDADE
    {
        public int ID { get; set; }
        public int ID_MEDICO { get; set; }
        public int ID_ESPECIALIDADE { get; set; }
    
        public virtual ESPECIALIDADES ESPECIALIDADES { get; set; }
        public virtual MEDICOS MEDICOS { get; set; }
    }
}
