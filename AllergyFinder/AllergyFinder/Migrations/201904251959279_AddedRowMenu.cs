namespace AllergyFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRowMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItems", "Allergens", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuItems", "Allergens");
        }
    }
}
