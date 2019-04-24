namespace AllergyFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemaakingDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Allergens",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        KnownAllergies = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AllergenJunctions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        AllergenId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Allergens", t => t.AllergenId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.AllergenId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Latitude = c.Single(),
                        Longitude = c.Single(),
                        City_Id = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AllergenReactionJunctions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ReactionId = c.Int(),
                        AllergenId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Allergens", t => t.AllergenId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Reactions", t => t.ReactionId)
                .Index(t => t.ReactionId)
                .Index(t => t.AllergenId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Reactions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CommonReactions = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AllergenTotals",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        AllergenId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Allergens", t => t.AllergenId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.AllergenId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.FoodLogs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Reactions = c.String(),
                        Allergens = c.String(),
                        CustomerId = c.Int(nullable: false),
                        MealId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.LocationComments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Latitude = c.Single(),
                        Longitude = c.Single(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ReactionTotals",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ReactionId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Reactions", t => t.ReactionId, cascadeDelete: true)
                .Index(t => t.ReactionId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ReactionTotals", "ReactionId", "dbo.Reactions");
            DropForeignKey("dbo.ReactionTotals", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.FoodLogs", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.AllergenTotals", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.AllergenTotals", "AllergenId", "dbo.Allergens");
            DropForeignKey("dbo.AllergenReactionJunctions", "ReactionId", "dbo.Reactions");
            DropForeignKey("dbo.AllergenReactionJunctions", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.AllergenReactionJunctions", "AllergenId", "dbo.Allergens");
            DropForeignKey("dbo.AllergenJunctions", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AllergenJunctions", "AllergenId", "dbo.Allergens");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ReactionTotals", new[] { "CustomerId" });
            DropIndex("dbo.ReactionTotals", new[] { "ReactionId" });
            DropIndex("dbo.FoodLogs", new[] { "CustomerId" });
            DropIndex("dbo.AllergenTotals", new[] { "CustomerId" });
            DropIndex("dbo.AllergenTotals", new[] { "AllergenId" });
            DropIndex("dbo.AllergenReactionJunctions", new[] { "CustomerId" });
            DropIndex("dbo.AllergenReactionJunctions", new[] { "AllergenId" });
            DropIndex("dbo.AllergenReactionJunctions", new[] { "ReactionId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Customers", new[] { "ApplicationUserId" });
            DropIndex("dbo.AllergenJunctions", new[] { "AllergenId" });
            DropIndex("dbo.AllergenJunctions", new[] { "CustomerId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ReactionTotals");
            DropTable("dbo.LocationComments");
            DropTable("dbo.FoodLogs");
            DropTable("dbo.AllergenTotals");
            DropTable("dbo.Reactions");
            DropTable("dbo.AllergenReactionJunctions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Customers");
            DropTable("dbo.AllergenJunctions");
            DropTable("dbo.Allergens");
        }
    }
}
