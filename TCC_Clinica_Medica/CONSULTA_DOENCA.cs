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
    
    public partial class CONSULTA_DOENCA
    {
        public int ID { get; set; }
        public Nullable<int> ID_CONSULTA { get; set; }
        public Nullable<int> ID_DOENCA { get; set; }
    
        public virtual CONSULTAS CONSULTAS { get; set; }
        public virtual DOENCAS DOENCAS { get; set; }
    }
}
