namespace CapaPresentacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizarModelo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Incapacidads", "EmpleadoId", "dbo.Empleadoes");
            DropForeignKey("dbo.Vacacions", "EmpleadoId", "dbo.Empleadoes");
            DropIndex("dbo.Incapacidads", new[] { "EmpleadoId" });
            DropIndex("dbo.Vacacions", new[] { "EmpleadoId" });
            RenameColumn(table: "dbo.Incapacidads", name: "EmpleadoId", newName: "Empleado_Id");
            RenameColumn(table: "dbo.Vacacions", name: "EmpleadoId", newName: "Empleado_Id");
            RenameColumn(table: "dbo.Incapacidads", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.Vacacions", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Incapacidads", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.Vacacions", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            AlterColumn("dbo.Incapacidads", "Empleado_Id", c => c.Int());
            AlterColumn("dbo.Vacacions", "Empleado_Id", c => c.Int());
            CreateIndex("dbo.Incapacidads", "Empleado_Id");
            CreateIndex("dbo.Vacacions", "Empleado_Id");
            AddForeignKey("dbo.Incapacidads", "Empleado_Id", "dbo.Empleadoes", "Id");
            AddForeignKey("dbo.Vacacions", "Empleado_Id", "dbo.Empleadoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vacacions", "Empleado_Id", "dbo.Empleadoes");
            DropForeignKey("dbo.Incapacidads", "Empleado_Id", "dbo.Empleadoes");
            DropIndex("dbo.Vacacions", new[] { "Empleado_Id" });
            DropIndex("dbo.Incapacidads", new[] { "Empleado_Id" });
            AlterColumn("dbo.Vacacions", "Empleado_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Incapacidads", "Empleado_Id", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Vacacions", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Incapacidads", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Vacacions", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Incapacidads", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Vacacions", name: "Empleado_Id", newName: "EmpleadoId");
            RenameColumn(table: "dbo.Incapacidads", name: "Empleado_Id", newName: "EmpleadoId");
            CreateIndex("dbo.Vacacions", "EmpleadoId");
            CreateIndex("dbo.Incapacidads", "EmpleadoId");
            AddForeignKey("dbo.Vacacions", "EmpleadoId", "dbo.Empleadoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Incapacidads", "EmpleadoId", "dbo.Empleadoes", "Id", cascadeDelete: true);
        }
    }
}
