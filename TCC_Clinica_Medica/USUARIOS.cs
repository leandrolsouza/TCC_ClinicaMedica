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
    
    public partial class USUARIOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIOS()
        {
            this.MEDICOS = new HashSet<MEDICOS>();
            this.PACIENTES = new HashSet<PACIENTES>();
        }
    
        public int ID { get; set; }
        public string NOME { get; set; }
        public string EMAIL { get; set; }
        public string CPF { get; set; }
        public string SENHA { get; set; }
        public Nullable<short> TIPO_ACESSO { get; set; }
        public byte[] FOTO { get; set; }
        public bool ATIVO { get; set; }
        public System.DateTime DATA_CRIACAO { get; set; }
        public Nullable<System.DateTime> DATA_MODIFICACAO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEDICOS> MEDICOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PACIENTES> PACIENTES { get; set; }
    }
}
