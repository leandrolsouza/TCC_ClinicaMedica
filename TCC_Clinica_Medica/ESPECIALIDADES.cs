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
    
    public partial class ESPECIALIDADES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ESPECIALIDADES()
        {
            this.MEDICO_ESPECIALIDADE = new HashSet<MEDICO_ESPECIALIDADE>();
        }
    
        public int ID { get; set; }
        public string DESCRICAO { get; set; }
        public bool ATIVO { get; set; }
        public System.DateTime DATA_CRIACAO { get; set; }
        public Nullable<System.DateTime> DATA_MODIFICACAO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEDICO_ESPECIALIDADE> MEDICO_ESPECIALIDADE { get; set; }
    }
}
