namespace CapaPresentacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cambios : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ComentarioPdfs", newName: "ComentarioDocumentoes");
            AlterColumn("dbo.DocumentoPdfs", "Nombre", c => c.String(nullable: false));
            DropColumn("dbo.DocumentoPdfs", "FechaSubida");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DocumentoPdfs", "FechaSubida", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DocumentoPdfs", "Nombre", c => c.String());
            RenameTable(name: "dbo.ComentarioDocumentoes", newName: "ComentarioPdfs");
        }
    }
}
