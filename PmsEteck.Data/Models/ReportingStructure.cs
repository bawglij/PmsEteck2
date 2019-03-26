using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ReportingStructure", Schema = "donkervoort")]
    public class ReportingStructure
    {
        [Key]
        public int iReportingStructureKey { get; set; }

        public int? RecNo { get; set; }

        public int? RcStart { get; set; }

        public int? RcEnd { get; set; }

        [MaxLength(50)]
        public string sSpatie { get; set; }

        [MaxLength(255)]
        public string sDescription { get; set; }

        public int? isFlipCode { get; set; }

        public int? iBalResKey { get; set; }

        public int? Berekenen { get; set; }

        [MaxLength(50)]
        public string Niveau { get; set; }

        public int? RS_JaarTerug { get; set; }

        public int? RS_VordSchuld { get; set; }

        public int? RS_IsStand { get; set; }

        public int? IsDirect { get; set; }

        public int? Bedrijfsresultaat { get; set; }

        public int? KostenTotaal { get; set; }

        public int? Leningen { get; set; }

        public int? KortlSchuldenVorderingen { get; set; }

        public int? Personeelskosten { get; set; }

        public int? bk_Splitsing { get; set; }

        public int? Kosten { get; set; }

        public int? Omzet { get; set; }

        public int? KostprijsOmzet { get; set; }

        public int? Marge { get; set; }

        public int? wk_Debiteuren { get; set; }

        public int? wk_Crediteuren { get; set; }

        public int? wk_LiquideMiddelen { get; set; }

        public int? wk_Belastingen { get; set; }

        public int? EigenVermogen { get; set; }

        public int? TotaalVermogen { get; set; }

        public int? CurRat_Activa { get; set; }

        public int? CurRat_Passiva { get; set; }

        [MaxLength(50)]
        public string sCalculation { get; set; }

        public int? iCalculation { get; set; }

        public int? VastVariabel { get; set; }

        public int? Factor { get; set; }

        [MaxLength(50)]
        public string sOpmaak { get; set; }

        public int? PercTonen { get; set; }

        public int? iProjectrapportagType { get; set; }


    }
}