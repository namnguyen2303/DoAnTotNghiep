namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editCustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.customers", "CustomerType_id", "dbo.customerTypes");
            DropIndex("dbo.customers", new[] { "CustomerType_id" });
            RenameColumn(table: "dbo.customers", name: "CustomerType_id", newName: "CustomerTypeId");
            AlterColumn("dbo.customers", "CustomerTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.customers", "CustomerTypeId");
            AddForeignKey("dbo.customers", "CustomerTypeId", "dbo.customerTypes", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.customers", "CustomerTypeId", "dbo.customerTypes");
            DropIndex("dbo.customers", new[] { "CustomerTypeId" });
            AlterColumn("dbo.customers", "CustomerTypeId", c => c.Int());
            RenameColumn(table: "dbo.customers", name: "CustomerTypeId", newName: "CustomerType_id");
            CreateIndex("dbo.customers", "CustomerType_id");
            AddForeignKey("dbo.customers", "CustomerType_id", "dbo.customerTypes", "id");
        }
    }
}
