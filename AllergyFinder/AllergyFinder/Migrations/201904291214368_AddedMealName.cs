namespace AllergyFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMealName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodLogs", "MealName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodLogs", "MealName");
        }
    }
}
