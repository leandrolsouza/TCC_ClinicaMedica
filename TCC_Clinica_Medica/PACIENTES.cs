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
    
    public partial class PACIENTES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PACIENTES()
        {
            this.CONSULTAS = new HashSet<CONSULTAS>();
        }
    
        public int ID { get; set; }
        public string ENDERECO { get; set; }
        public Nullable<int> ID_PLANO_SAUDE { get; set; }
        public Nullable<int> ID_USUARIO { get; set; }
        public string TELEFONE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONSULTAS> CONSULTAS { get; set; }
        public virtual PLANO_SAUDE PLANO_SAUDE { get; set; }
        public virtual USUARIOS USUARIOS { get; set; }
    }
}
