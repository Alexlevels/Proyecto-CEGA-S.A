namespace CapaPresentacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearTablaSubtarea : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subtareas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false),
                        Estado = c.String(),
                        FechaLimite = c.DateTime(),
                        TareaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tareas", t => t.TareaId, cascadeDelete: true)
                .Index(t => t.TareaId);
            
            AddColumn("dbo.Tareas", "Descripcion", c => c.String());
            AddColumn("dbo.Tareas", "FechaInicio", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tareas", "FechaFin", c => c.DateTime());
            DropColumn("dbo.Tareas", "FechaCreacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tareas", "FechaCreacion", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Subtareas", "TareaId", "dbo.Tareas");
            DropIndex("dbo.Subtareas", new[] { "TareaId" });
            DropColumn("dbo.Tareas", "FechaFin");
            DropColumn("dbo.Tareas", "FechaInicio");
            DropColumn("dbo.Tareas", "Descripcion");
            DropTable("dbo.Subtareas");
        }
    }
}
