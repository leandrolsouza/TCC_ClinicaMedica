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
    
    public partial class CLINICAS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLINICAS()
        {
            this.MEDICO_CLINICA = new HashSet<MEDICO_CLINICA>();
        }
    
        public int ID { get; set; }
        public string NOME { get; set; }
        public string CNPJ { get; set; }
        public System.DateTime DATA_CRIACAO { get; set; }
        public Nullable<System.DateTime> DATA_MODIFICACAO { get; set; }
        public bool ATIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEDICO_CLINICA> MEDICO_CLINICA { get; set; }
    }
}