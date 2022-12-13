namespace SGE.Models
{
    public class SistemaCases
    {
        public int Id { get; set; }
        public Sistema Sistema { get; set; }
        public int? SistemaId { get; set; }
        public Case Case { get; set; }
        public int? CaseId { get; set; }

    }
}
