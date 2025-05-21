namespace CapaPresentacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearTablaTareas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tareas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false),
                        Estado = c.String(),
                        ProyectoId = c.Int(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Proyectoes", t => t.ProyectoId, cascadeDelete: true)
                .Index(t => t.ProyectoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tareas", "ProyectoId", "dbo.Proyectoes");
            DropIndex("dbo.Tareas", new[] { "ProyectoId" });
            DropTable("dbo.Tareas");
        }
    }
}
