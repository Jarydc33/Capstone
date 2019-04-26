namespace AllergyFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMenuAPI : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FoodItem = c.String(),
                        RestaurantId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RestaurantId = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuItems", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.MenuItems", new[] { "RestaurantId" });
            DropTable("dbo.Restaurants");
            DropTable("dbo.MenuItems");
        }
    }
}
