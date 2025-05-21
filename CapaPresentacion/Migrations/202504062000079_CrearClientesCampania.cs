namespace CapaPresentacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearClientesCampania : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClienteCampanas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Correo = c.String(nullable: false),
                        CampanaMarketingId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CampanaMarketings", t => t.CampanaMarketingId)
                .Index(t => t.CampanaMarketingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClienteCampanas", "CampanaMarketingId", "dbo.CampanaMarketings");
            DropIndex("dbo.ClienteCampanas", new[] { "CampanaMarketingId" });
            DropTable("dbo.ClienteCampanas");
        }
    }
}
