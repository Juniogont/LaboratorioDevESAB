namespace SGE.Models
{
    public class CaseEquipamentos
    {
        public int Id { get; set; }
        public Case Case { get; set; }
        public int? CaseId { get; set; }
        public Equipamento Equipamento { get; set; }
        public int? EquipamentoId { get; set; }
    }
}
