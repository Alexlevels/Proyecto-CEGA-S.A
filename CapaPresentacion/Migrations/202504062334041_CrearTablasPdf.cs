namespace CapaPresentacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearTablasPdf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarpetaDocumentacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DocumentoPdfs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        RutaArchivo = c.String(),
                        CarpetaId = c.Int(nullable: false),
                        FechaSubida = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarpetaDocumentacions", t => t.CarpetaId, cascadeDelete: true)
                .Index(t => t.CarpetaId);
            
            CreateTable(
                "dbo.ComentarioPdfs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Texto = c.String(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        DocumentoPdfId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DocumentoPdfs", t => t.DocumentoPdfId, cascadeDelete: true)
                .Index(t => t.DocumentoPdfId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComentarioPdfs", "DocumentoPdfId", "dbo.DocumentoPdfs");
            DropForeignKey("dbo.DocumentoPdfs", "CarpetaId", "dbo.CarpetaDocumentacions");
            DropIndex("dbo.ComentarioPdfs", new[] { "DocumentoPdfId" });
            DropIndex("dbo.DocumentoPdfs", new[] { "CarpetaId" });
            DropTable("dbo.ComentarioPdfs");
            DropTable("dbo.DocumentoPdfs");
            DropTable("dbo.CarpetaDocumentacions");
        }
    }
}
