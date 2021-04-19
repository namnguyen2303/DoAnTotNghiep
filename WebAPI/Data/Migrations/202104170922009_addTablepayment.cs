namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTablepayment : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.orders", new[] { "user_id" });
            CreateTable(
                "dbo.payments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.orders", "paymentId", c => c.Int(nullable: false));
            AlterColumn("dbo.carts", "createDate", c => c.DateTime());
            AlterColumn("dbo.orders", "user_id", c => c.Int());
            CreateIndex("dbo.orders", "paymentId");
            CreateIndex("dbo.orders", "user_id");
            AddForeignKey("dbo.orders", "paymentId", "dbo.payments", "id", cascadeDelete: true);
            DropColumn("dbo.orders", "is_active");
        }
        
        public override void Down()
        {
            AddColumn("dbo.orders", "is_active", c => c.Int(nullable: false));
            DropForeignKey("dbo.orders", "paymentId", "dbo.payments");
            DropIndex("dbo.orders", new[] { "user_id" });
            DropIndex("dbo.orders", new[] { "paymentId" });
            AlterColumn("dbo.orders", "user_id", c => c.Int(nullable: false));
            AlterColumn("dbo.carts", "createDate", c => c.Int());
            DropColumn("dbo.orders", "paymentId");
            DropTable("dbo.payments");
            CreateIndex("dbo.orders", "user_id");
        }
    }
}
