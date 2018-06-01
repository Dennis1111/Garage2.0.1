namespace Garage2._0._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkedVehicles",
                c => new
                    {
                        RegistrationNumber = c.String(nullable: false, maxLength: 6),
                        Type = c.Int(nullable: false),
                        Color = c.String(maxLength: 20),
                        Brand = c.String(maxLength: 20),
                        Wheels = c.Int(nullable: false),
                        ParkingTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RegistrationNumber);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ParkedVehicles");
        }
    }
}
