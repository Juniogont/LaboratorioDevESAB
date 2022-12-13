using Microsoft.EntityFrameworkCore;
using SGE.Models;

namespace SGE.Data
{
    public class SGEMvcContext : DbContext
    {
        public SGEMvcContext(DbContextOptions<SGEMvcContext> options)
            : base(options)
        {
        }

        public SGEMvcContext()
        : base()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseMySql(Constantes.connectionString,
                opt => opt.MaxBatchSize(50));

        public DbSet<SGE.Models.Pais> Pais { get; set; }
        public DbSet<SGE.Models.Estado> Estado { get; set; }
        public DbSet<SGE.Models.Cidade> Cidade { get; set; }
        public DbSet<SGE.Models.Entidade> Entidade { get; set; }
        public DbSet<SGE.Models.Endereco> Endereco { get; set; }
        public DbSet<SGE.Models.Instrumento> Instrumento { get; set; }
        public DbSet<SGE.Models.Usuario> Usuario { get; set; }
        public DbSet<SGE.Models.DespesaViagem> DespesaViagem { get; set; }
        public DbSet<SGE.Models.FormaPagamento> FormaPagamento { get; set; }
        public DbSet<SGE.Models.Equipamento> Equipamento { get; set; }
        public DbSet<SGE.Models.Case> Case { get; set; }
        public DbSet<SGE.Models.CaseEquipamentos> CaseEquipamentos { get; set; }
        public DbSet<SGE.Models.Sistema> Sistema { get; set; }
        public DbSet<SGE.Models.SistemaCases> SistemaCases { get; set; }
        public DbSet<SGE.Models.ModeloOrcamento> ModeloOrcamento { get; set; }
        public DbSet<SGE.Models.ModeloContrato> ModeloContrato { get; set; }
        public DbSet<SGE.Models.Montagem> Montagem { get; set; }
        public DbSet<SGE.Models.LocalEvento> LocalEvento { get; set; }
        public DbSet<SGE.Models.Orcamento> Orcamento { get; set; }
        public DbSet<SGE.Models.OrcamentoItem> OrcamentoItem { get; set; }
        public DbSet<SGE.Models.Funcionario> Funcionario { get; set; }
        public DbSet<SGE.Models.Evento> Evento { get; set; }
        public DbSet<SGE.Models.EventoItem> EventoItem { get; set; }
        public DbSet<SGE.Models.PlanoContas> PlanoContas { get; set; }
        public DbSet<SGE.Models.ContaCorrente> ContaCorrente { get; set; }
        public DbSet<SGE.Models.MovimentoFinanceiro> MovimentoFinanceiro { get; set; }
        public DbSet<SGE.Models.PosicaoAlmoxarifado> PosicaoAlmoxarifado { get; set; }
        public DbSet<SGE.Models.Almoxarifado> Almoxarifado { get; set; }
        public DbSet<SGE.Models.TabelaPreco> TabelaPreco { get; set; }
        public DbSet<SGE.Models.Contrato> Contrato { get; set; }
        public DbSet<SGE.Models.DespesaEvento> DespesaEvento { get; set; }
        public DbSet<SGE.Models.Empresa> Empresa { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DespesaViagem>().Property(p => p.Almoco).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<DespesaViagem>().Property(p => p.CoeficienteKM).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<DespesaViagem>().Property(p => p.CoeficienteNF).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<DespesaViagem>().Property(p => p.Estadia).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<DespesaViagem>().Property(p => p.Jantar).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<DespesaViagem>().Property(p => p.Lanche).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<MovimentoFinanceiro>().Property(p => p.Valor).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Orcamento>().Property(p => p.ValorTotal).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Orcamento>().Property(p => p.ValorDesconto).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Evento>().Property(p => p.ValorTotal).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Evento>().Property(p => p.ValorDesconto).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<MovimentoFinanceiro>().Property(p => p.Valor).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<TabelaPreco>().Property(p => p.ValorCompra).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<TabelaPreco>().Property(p => p.ValorLocacao).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<DespesaEvento>().Property(p => p.ValorAlmoco).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<DespesaEvento>().Property(p => p.ValorEstadia).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<DespesaEvento>().Property(p => p.ValorJantar).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<DespesaEvento>().Property(p => p.ValorLanche).HasColumnType("decimal(18,2)");
        }

    
        
    }
}
