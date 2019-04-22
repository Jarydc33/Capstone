namespace AllergyFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "City_Id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "City_Id");
        }
    }
}
