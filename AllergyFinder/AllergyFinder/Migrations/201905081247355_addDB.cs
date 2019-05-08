namespace AllergyFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDB : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "Address");
            DropColumn("dbo.Customers", "City");
            DropColumn("dbo.Customers", "State");
            DropColumn("dbo.Customers", "Zip");
            DropColumn("dbo.Customers", "Latitude");
            DropColumn("dbo.Customers", "Longitude");
            DropColumn("dbo.Customers", "City_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "City_Id", c => c.String());
            AddColumn("dbo.Customers", "Longitude", c => c.Single());
            AddColumn("dbo.Customers", "Latitude", c => c.Single());
            AddColumn("dbo.Customers", "Zip", c => c.String());
            AddColumn("dbo.Customers", "State", c => c.String());
            AddColumn("dbo.Customers", "City", c => c.String());
            AddColumn("dbo.Customers", "Address", c => c.String());
        }
    }
}
