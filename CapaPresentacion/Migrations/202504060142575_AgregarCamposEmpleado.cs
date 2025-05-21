namespace CapaPresentacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarCamposEmpleado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Incapacidads", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Vacacions", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "NombreCompleto", c => c.String());
            AddColumn("dbo.AspNetUsers", "Puesto", c => c.String());
            AddColumn("dbo.AspNetUsers", "Salario", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.AspNetUsers", "FechaContratacion", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Incapacidads", "ApplicationUser_Id");
            CreateIndex("dbo.Vacacions", "ApplicationUser_Id");
            AddForeignKey("dbo.Incapacidads", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Vacacions", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vacacions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Incapacidads", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Vacacions", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Incapacidads", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "FechaContratacion");
            DropColumn("dbo.AspNetUsers", "Salario");
            DropColumn("dbo.AspNetUsers", "Puesto");
            DropColumn("dbo.AspNetUsers", "NombreCompleto");
            DropColumn("dbo.Vacacions", "ApplicationUser_Id");
            DropColumn("dbo.Incapacidads", "ApplicationUser_Id");
        }
    }
}
