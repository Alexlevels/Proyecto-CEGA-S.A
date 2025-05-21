namespace CapaPresentacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearTablasComentarios : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComentarioProyectoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProyectoId = c.Int(nullable: false),
                        Texto = c.String(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Proyectoes", t => t.ProyectoId, cascadeDelete: true)
                .Index(t => t.ProyectoId);
            
            CreateTable(
                "dbo.ComentarioTareas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Texto = c.String(),
                        ArchivoRuta = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        TareaId = c.Int(nullable: false),
                        UsuarioId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tareas", t => t.TareaId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId)
                .Index(t => t.TareaId)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComentarioTareas", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ComentarioTareas", "TareaId", "dbo.Tareas");
            DropForeignKey("dbo.ComentarioProyectoes", "ProyectoId", "dbo.Proyectoes");
            DropIndex("dbo.ComentarioTareas", new[] { "UsuarioId" });
            DropIndex("dbo.ComentarioTareas", new[] { "TareaId" });
            DropIndex("dbo.ComentarioProyectoes", new[] { "ProyectoId" });
            DropTable("dbo.ComentarioTareas");
            DropTable("dbo.ComentarioProyectoes");
        }
    }
}
