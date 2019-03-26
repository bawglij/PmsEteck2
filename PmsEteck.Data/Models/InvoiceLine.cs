using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("InvoiceLines", Schema = "invoice")]
    public class InvoiceLine
    {
        [Key]
        public int iInvoiceLineID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Factuur")]
        public int iInvoiceID { get; set; }

        [Display(Name = "Tariefkaartregel")]
        [ForeignKey("RateCardRow")]
        public int? iRateCardRowID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Rekeningnummer")]
        [DisplayFormat(DataFormatString = "{0:D6}", ApplyFormatInEditMode = true)]
        [Range(0, 999999, ErrorMessage = "{0} moet een waarde bevatten tussen {1} en {2}")]
        public int iLedgerNumber { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Aantal")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal dQuantity { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Prijs")]
        [DisplayFormat(DataFormatString = "{0:C5}")]
        public decimal dAmount { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Regelbedrag")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal dTotalAmount { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Omschrijving")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sDescription { get; set; }

        [Display(Name = "Omschrijving (2)")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sDescription2 { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Eenheidprijs")]
        [DisplayFormat(DataFormatString = "{0:C5}")]
        public decimal dUnitPrice { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Code rubriektype")]
        [ForeignKey("RubricType")]
        public int iRubricTypeID { get; set; }
        
        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Eenheid")]
        [ForeignKey("Unit")]
        public int iUnitID { get; set; }
        
        [Display(Name = "Startdatum")]
        [DataType(DataType.Date)]
        public DateTime? dtStartDate { get; set; }

        [Display(Name = "Einddatum")]
        [DataType(DataType.Date)]
        public DateTime? dtEndDate { get; set; }

        [Display(Name = "Beginstand")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal dStartPosition { get; set; }

        [Display(Name = "Eindstand")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal dEndPosition { get; set; }

        [Display(Name = "Verbruik")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal dConsumption { get; set; }

        [Display(Name = "Is eindafrekening")]
        public bool bIsEndCalculation { get; set; }
        
        [Display(Name = "Settlement Code")]
        [MaxLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sSettlementCode { get; set; }

        [Display(Name = "Settlement Text")]
        [MaxLength(30, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sSettlementText { get; set; }

        [Display(Name = "Korting")]
        public bool Discount { get; set; }

        [MaxLength(50)]
        [Display(Name = "Btw conditie code")]
        public string VatConditionCode { get; set; }

        [Display(Name = "Factuur")]
        public virtual Invoice Invoice { get; set; }

        [Display(Name = "Tariefkaartregel")]
        public virtual RateCardRow RateCardRow { get; set; }

        [Display(Name = "Eenheid")]
        public virtual Unit Unit { get; set; }

        [Display(Name = "Rubriektype")]
        public virtual RubricType RubricType { get; set; }

    }
}