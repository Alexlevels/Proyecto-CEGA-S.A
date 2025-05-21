namespace CapaPresentacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearTablaSubtarea1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subtareas", "Descripcion", c => c.String(nullable: false));
            AlterColumn("dbo.Subtareas", "Estado", c => c.String(nullable: false));
            DropColumn("dbo.Subtareas", "Titulo");
            DropColumn("dbo.Subtareas", "FechaLimite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subtareas", "FechaLimite", c => c.DateTime());
            AddColumn("dbo.Subtareas", "Titulo", c => c.String(nullable: false));
            AlterColumn("dbo.Subtareas", "Estado", c => c.String());
            DropColumn("dbo.Subtareas", "Descripcion");
        }
    }
}
