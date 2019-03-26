using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Labels", Schema = "service")]
    public class Label
    {
        #region Constructor
        private PmsEteckContext context = new PmsEteckContext();
        #endregion

        #region Properties
        [Key]
        public int iLabelID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is verplicht")]
        [StringLength(75, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Omschrijving")]
        public string sDescription { get; set; }

        [Display(Name = "Meldingen")]
        //public List<Ticket> Tickets { get; set; }
        public List<TicketLabel> Tickets { get; set; }

        #endregion

        #region Public Functions
        public List<Label> AddLabels(List<string> newLabels)
        {
            var labelList = new List<Label>();
            foreach (var item in newLabels)
            {
                Label label = new Label { sDescription = item.ToLower() };
                labelList.Add(label);
            }
            return labelList;
        }
        #endregion
    }
}
