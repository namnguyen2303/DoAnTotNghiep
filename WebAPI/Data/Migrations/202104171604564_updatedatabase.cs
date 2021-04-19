namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.userRoles", "customerId", "dbo.customers");
            DropIndex("dbo.userRoles", new[] { "customerId" });
            RenameColumn(table: "dbo.orders", name: "user_id", newName: "userId");
            RenameIndex(table: "dbo.orders", name: "IX_user_id", newName: "IX_userId");
            DropPrimaryKey("dbo.userRoles");
            AddColumn("dbo.userRoles", "userId", c => c.Int(nullable: false));
            AlterColumn("dbo.users", "code", c => c.String(maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.userRoles", new[] { "roleId", "userId" });
            CreateIndex("dbo.userRoles", "userId");
            AddForeignKey("dbo.userRoles", "userId", "dbo.users", "id");
            DropColumn("dbo.userRoles", "customerId");
            DropColumn("dbo.users", "role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.users", "role", c => c.Int());
            AddColumn("dbo.userRoles", "customerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.userRoles", "userId", "dbo.users");
            DropIndex("dbo.userRoles", new[] { "userId" });
            DropPrimaryKey("dbo.userRoles");
            AlterColumn("dbo.users", "code", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("dbo.userRoles", "userId");
            AddPrimaryKey("dbo.userRoles", new[] { "roleId", "customerId" });
            RenameIndex(table: "dbo.orders", name: "IX_userId", newName: "IX_user_id");
            RenameColumn(table: "dbo.orders", name: "userId", newName: "user_id");
            CreateIndex("dbo.userRoles", "customerId");
            AddForeignKey("dbo.userRoles", "customerId", "dbo.customers", "id");
        }
    }
}
