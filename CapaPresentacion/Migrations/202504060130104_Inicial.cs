namespace CapaPresentacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empleadoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Correo = c.String(nullable: false),
                        Puesto = c.String(),
                        Salario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaContratacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Incapacidads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpleadoId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        ArchivoComprobante = c.String(),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empleadoes", t => t.EmpleadoId, cascadeDelete: true)
                .Index(t => t.EmpleadoId);
            
            CreateTable(
                "dbo.Vacacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpleadoId = c.Int(nullable: false),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(nullable: false),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empleadoes", t => t.EmpleadoId, cascadeDelete: true)
                .Index(t => t.EmpleadoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vacacions", "EmpleadoId", "dbo.Empleadoes");
            DropForeignKey("dbo.Incapacidads", "EmpleadoId", "dbo.Empleadoes");
            DropIndex("dbo.Vacacions", new[] { "EmpleadoId" });
            DropIndex("dbo.Incapacidads", new[] { "EmpleadoId" });
            DropTable("dbo.Vacacions");
            DropTable("dbo.Incapacidads");
            DropTable("dbo.Empleadoes");
        }
    }
}
