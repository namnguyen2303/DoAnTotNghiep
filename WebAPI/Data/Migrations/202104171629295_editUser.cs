namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.users", "username", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.users", "pass", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.users", "phone", c => c.String(nullable: false, maxLength: 15, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.users", "phone", c => c.String(maxLength: 15, unicode: false));
            AlterColumn("dbo.users", "pass", c => c.String(unicode: false));
            AlterColumn("dbo.users", "username", c => c.String(maxLength: 100, unicode: false));
        }
    }
}
