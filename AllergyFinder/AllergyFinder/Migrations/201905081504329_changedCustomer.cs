namespace AllergyFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allergens", "CustomerId", c => c.Int());
            CreateIndex("dbo.Allergens", "CustomerId");
            AddForeignKey("dbo.Allergens", "CustomerId", "dbo.Customers", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Allergens", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Allergens", new[] { "CustomerId" });
            DropColumn("dbo.Allergens", "CustomerId");
        }
    }
}
