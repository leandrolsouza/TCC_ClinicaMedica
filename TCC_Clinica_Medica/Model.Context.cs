﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TCC_CLINICA_MEDICAEntities : DbContext
    {
        public TCC_CLINICA_MEDICAEntities()
            : base("name=TCC_CLINICA_MEDICAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ANAMNESE> ANAMNESE { get; set; }
        public virtual DbSet<CLINICAS> CLINICAS { get; set; }
        public virtual DbSet<CONSULTA_DOENCA> CONSULTA_DOENCA { get; set; }
        public virtual DbSet<CONSULTAS> CONSULTAS { get; set; }
        public virtual DbSet<DOENCAS> DOENCAS { get; set; }
        public virtual DbSet<ESPECIALIDADES> ESPECIALIDADES { get; set; }
        public virtual DbSet<EXAME_RESULTADO> EXAME_RESULTADO { get; set; }
        public virtual DbSet<EXAMES> EXAMES { get; set; }
        public virtual DbSet<EXAMES_SOLICITADOS> EXAMES_SOLICITADOS { get; set; }
        public virtual DbSet<HORARIOS_ATENDIMENTO> HORARIOS_ATENDIMENTO { get; set; }
        public virtual DbSet<MEDICAMENTOS> MEDICAMENTOS { get; set; }
        public virtual DbSet<MEDICO_CLINICA> MEDICO_CLINICA { get; set; }
        public virtual DbSet<MEDICO_ESPECIALIDADE> MEDICO_ESPECIALIDADE { get; set; }
        public virtual DbSet<MEDICO_HORARIO_ATENDIMENTO> MEDICO_HORARIO_ATENDIMENTO { get; set; }
        public virtual DbSet<MEDICOS> MEDICOS { get; set; }
        public virtual DbSet<PACIENTES> PACIENTES { get; set; }
        public virtual DbSet<PLANO_SAUDE> PLANO_SAUDE { get; set; }
        public virtual DbSet<RECEITAS> RECEITAS { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<USUARIOS> USUARIOS { get; set; }
    }
}
