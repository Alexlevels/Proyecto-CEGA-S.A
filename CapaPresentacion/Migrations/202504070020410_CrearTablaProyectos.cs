namespace CapaPresentacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearTablaProyectos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Proyectoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Descripcion = c.String(),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Proyectoes");
        }
    }
}
