namespace AllergyFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserMade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Allergens", "UserMade", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Allergens", "UserMade");
        }
    }
}
