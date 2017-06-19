namespace BankaRacun_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicijalni_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RACUN",
                c => new
                    {
                        ID_RACUNA = c.Int(nullable: false, identity: true),
                        NOSILAC_RACUNA = c.String(nullable: false, maxLength: 100),
                        BROJ_RACUNA = c.String(nullable: false, maxLength: 50),
                        AKTIVAN_RACUN = c.Boolean(nullable: false),
                        ONLINE_BANKING = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID_RACUNA);
            
            CreateTable(
                "dbo.UPLATNICA",
                c => new
                    {
                        ID_UPLATNICE = c.Int(nullable: false, identity: true),
                        ID_RACUNA = c.Int(nullable: false),
                        IZNOS_UPLATE = c.Double(nullable: false),
                        DATUM_PROMETA = c.DateTime(nullable: false),
                        SVRHA_UPLATE = c.String(nullable: false, maxLength: 100),
                        UPLATILAC = c.String(nullable: false, maxLength: 50),
                        HITNO = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID_UPLATNICE)
                .ForeignKey("dbo.RACUN", t => t.ID_RACUNA, cascadeDelete: true)
                .Index(t => t.ID_RACUNA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UPLATNICA", "ID_RACUNA", "dbo.RACUN");
            DropIndex("dbo.UPLATNICA", new[] { "ID_RACUNA" });
            DropTable("dbo.UPLATNICA");
            DropTable("dbo.RACUN");
        }
    }
}
