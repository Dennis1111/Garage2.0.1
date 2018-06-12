namespace Garage2._0._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class getset : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ParkedVehicles", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.ParkedVehicles", "VehicleType_Id", "dbo.VehicleTypes");
            DropIndex("dbo.ParkedVehicles", new[] { "Member_Id" });
            DropIndex("dbo.ParkedVehicles", new[] { "VehicleType_Id" });
            RenameColumn(table: "dbo.ParkedVehicles", name: "Member_Id", newName: "MembersId");
            RenameColumn(table: "dbo.ParkedVehicles", name: "VehicleType_Id", newName: "VehicleTypeId");
            AlterColumn("dbo.ParkedVehicles", "MembersId", c => c.Int(nullable: false));
            AlterColumn("dbo.ParkedVehicles", "VehicleTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ParkedVehicles", "VehicleTypeId");
            CreateIndex("dbo.ParkedVehicles", "MembersId");
            AddForeignKey("dbo.ParkedVehicles", "MembersId", "dbo.Members", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ParkedVehicles", "VehicleTypeId", "dbo.VehicleTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkedVehicles", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.ParkedVehicles", "MembersId", "dbo.Members");
            DropIndex("dbo.ParkedVehicles", new[] { "MembersId" });
            DropIndex("dbo.ParkedVehicles", new[] { "VehicleTypeId" });
            AlterColumn("dbo.ParkedVehicles", "VehicleTypeId", c => c.Int());
            AlterColumn("dbo.ParkedVehicles", "MembersId", c => c.Int());
            RenameColumn(table: "dbo.ParkedVehicles", name: "VehicleTypeId", newName: "VehicleType_Id");
            RenameColumn(table: "dbo.ParkedVehicles", name: "MembersId", newName: "Member_Id");
            CreateIndex("dbo.ParkedVehicles", "VehicleType_Id");
            CreateIndex("dbo.ParkedVehicles", "Member_Id");
            AddForeignKey("dbo.ParkedVehicles", "VehicleType_Id", "dbo.VehicleTypes", "Id");
            AddForeignKey("dbo.ParkedVehicles", "Member_Id", "dbo.Members", "Id");
        }
    }
}
