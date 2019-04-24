namespace AllergyFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPercentage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllergenReactionJunctions", "Percentage", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllergenReactionJunctions", "Percentage");
        }
    }
}
