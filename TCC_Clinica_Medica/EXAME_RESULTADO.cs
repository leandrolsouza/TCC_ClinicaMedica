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
    
    public partial class EXAME_RESULTADO
    {
        public int ID { get; set; }
        public string DESCRICAO { get; set; }
        public int ID_EXAMES_SOLICITADO { get; set; }
        public System.DateTime DATA_CRIACAO { get; set; }
        public Nullable<bool> ENTREGUE_PACIENTE { get; set; }
        public Nullable<System.Guid> GUID { get; set; }
    
        public virtual EXAMES_SOLICITADOS EXAMES_SOLICITADOS { get; set; }
    }
}
