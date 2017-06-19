using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankaRacun_EF.Models
{
    [Table("UPLATNICA")]
    public class Uplatnica
    {
        [Column("ID_UPLATNICE")]
        public int UplatnicaId { get; set; }

        [ForeignKey("Racun")]
        [Column("ID_RACUNA")]
        public virtual int RacunId { get; set; }

        public virtual Racun Racun { get; set; }

        [Required]
        [Column("IZNOS_UPLATE")]
        [Display(Name = "Iznos uplate")]
        public double Iznos { get; set; }

        [Required]
        [Column("DATUM_PROMETA")]
        [Display(Name = "Datum prometa")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatumPrometa { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("SVRHA_UPLATE")]
        [Display(Name = "Svrha uplate")]
        public string SvrhaUplate { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("UPLATILAC")]
        public string Uplatilac { get; set; }

        [Column("HITNO")]
        public bool Hitno { get; set; }

        public Uplatnica()
        {

        }
    }
}