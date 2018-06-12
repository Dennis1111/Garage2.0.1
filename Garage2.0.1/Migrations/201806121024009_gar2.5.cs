namespace Garage2._0._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gar25 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ParkedVehicles", "Member_Id", c => c.Int());
            AddColumn("dbo.ParkedVehicles", "Type_Id", c => c.Int());
            AddColumn("dbo.ParkedVehicles", "VechicleType_Id", c => c.Int());
            CreateIndex("dbo.ParkedVehicles", "Member_Id");
            CreateIndex("dbo.ParkedVehicles", "Type_Id");
            CreateIndex("dbo.ParkedVehicles", "VechicleType_Id");
            AddForeignKey("dbo.ParkedVehicles", "Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.ParkedVehicles", "Type_Id", "dbo.VehicleTypes", "Id");
            AddForeignKey("dbo.ParkedVehicles", "VechicleType_Id", "dbo.VehicleTypes", "Id");
            DropColumn("dbo.ParkedVehicles", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParkedVehicles", "Type", c => c.Int(nullable: false));
            DropForeignKey("dbo.ParkedVehicles", "VechicleType_Id", "dbo.VehicleTypes");
            DropForeignKey("dbo.ParkedVehicles", "Type_Id", "dbo.VehicleTypes");
            DropForeignKey("dbo.ParkedVehicles", "Member_Id", "dbo.Members");
            DropIndex("dbo.ParkedVehicles", new[] { "VechicleType_Id" });
            DropIndex("dbo.ParkedVehicles", new[] { "Type_Id" });
            DropIndex("dbo.ParkedVehicles", new[] { "Member_Id" });
            DropColumn("dbo.ParkedVehicles", "VechicleType_Id");
            DropColumn("dbo.ParkedVehicles", "Type_Id");
            DropColumn("dbo.ParkedVehicles", "Member_Id");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Members");
        }
    }
}
