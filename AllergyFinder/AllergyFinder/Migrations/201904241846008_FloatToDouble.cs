namespace AllergyFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FloatToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AllergenReactionJunctions", "Percentage", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AllergenReactionJunctions", "Percentage", c => c.Single());
        }
    }
}
