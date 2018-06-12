namespace Garage2._0._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleTypeInparkedVehicle : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ParkedVehicles", "VechicleType_Id", "dbo.VehicleTypes");
            DropIndex("dbo.ParkedVehicles", new[] { "VechicleType_Id" });
            RenameColumn(table: "dbo.ParkedVehicles", name: "Type_Id", newName: "VehicleType_Id");
            RenameIndex(table: "dbo.ParkedVehicles", name: "IX_Type_Id", newName: "IX_VehicleType_Id");
            DropColumn("dbo.ParkedVehicles", "VechicleType_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParkedVehicles", "VechicleType_Id", c => c.Int());
            RenameIndex(table: "dbo.ParkedVehicles", name: "IX_VehicleType_Id", newName: "IX_Type_Id");
            RenameColumn(table: "dbo.ParkedVehicles", name: "VehicleType_Id", newName: "Type_Id");
            CreateIndex("dbo.ParkedVehicles", "VechicleType_Id");
            AddForeignKey("dbo.ParkedVehicles", "VechicleType_Id", "dbo.VehicleTypes", "Id");
        }
    }
}
