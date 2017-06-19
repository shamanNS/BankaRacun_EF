using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankaRacun_EF.Models
{
    [Table("RACUN")]
    public class Racun
    {
        [Column("ID_RACUNA")]
        public int RacunId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("NOSILAC_RACUNA")]
        [Display(Name = "Nosilac racuna")]
        public string NosilacRacuna { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("BROJ_RACUNA")]
        [Display(Name = "Broj racuna")]
        public string BrojRacuna { get; set; }

        [Display(Name="Aktivan")]
        [Column("AKTIVAN_RACUN")]
        public bool Aktivan { get; set; }

        [Display(Name = "Online")]
        [Column("ONLINE_BANKING")]
        public bool OnlineBanking { get; set; }

        public virtual List<Uplatnica> Uplatnice { get; set; }

        public Racun()
        {
            this.Uplatnice = new List<Uplatnica>();
        }
    }
}