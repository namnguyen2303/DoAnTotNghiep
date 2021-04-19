namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cartDetails",
                c => new
                    {
                        cartId = c.Int(nullable: false),
                        productId = c.Int(nullable: false),
                        totalPrices = c.Decimal(nullable: false, precision: 18, scale: 0),
                        totalItems = c.Int(nullable: false),
                        createDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.cartId, t.productId })
                .ForeignKey("dbo.carts", t => t.cartId)
                .ForeignKey("dbo.products", t => t.productId)
                .Index(t => t.cartId)
                .Index(t => t.productId);
            
            CreateTable(
                "dbo.carts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        createDate = c.Int(),
                        customerId = c.Int(nullable: false),
                        totalPrices = c.Decimal(nullable: false, precision: 18, scale: 0),
                        totalItems = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.customers", t => t.customerId)
                .Index(t => t.customerId);
            
            CreateTable(
                "dbo.customers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        code = c.String(maxLength: 50, unicode: false),
                        name_customer = c.String(maxLength: 50),
                        phone = c.String(maxLength: 15, unicode: false),
                        pass = c.String(nullable: false, maxLength: 250, unicode: false),
                        address_customer = c.String(maxLength: 255),
                        gender = c.Int(),
                        type = c.Int(),
                        status = c.Int(),
                        DOB = c.DateTime(),
                        email = c.String(maxLength: 100, unicode: false),
                        avatar_url = c.String(maxLength: 255, unicode: false),
                        is_active = c.Int(nullable: false),
                        created_at = c.DateTime(),
                        CustomerType_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.customerTypes", t => t.CustomerType_id)
                .Index(t => t.CustomerType_id);
            
            CreateTable(
                "dbo.customerTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        code = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        customer_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                        status = c.Int(),
                        total_price = c.Decimal(nullable: false, precision: 18, scale: 0),
                        date_buy = c.DateTime(),
                        note = c.String(maxLength: 255),
                        is_active = c.Int(nullable: false),
                        created_at = c.DateTime(),
                        totalItem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.user_id)
                .ForeignKey("dbo.customers", t => t.customer_id)
                .Index(t => t.customer_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.orderDetails",
                c => new
                    {
                        orderId = c.Int(nullable: false),
                        productId = c.Int(nullable: false),
                        totalPrices = c.Decimal(nullable: false, precision: 18, scale: 0),
                        totalItems = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.orderId, t.productId })
                .ForeignKey("dbo.products", t => t.productId)
                .ForeignKey("dbo.orders", t => t.orderId)
                .Index(t => t.orderId)
                .Index(t => t.productId);
            
            CreateTable(
                "dbo.products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_category_id = c.Int(nullable: false),
                        code = c.String(maxLength: 50, unicode: false),
                        product_name = c.String(maxLength: 255),
                        price_start = c.Decimal(precision: 18, scale: 0),
                        price_sale = c.Decimal(precision: 18, scale: 0),
                        summary = c.String(maxLength: 255),
                        description = c.String(maxLength: 255),
                        detail = c.String(unicode: false, storeType: "text"),
                        slug = c.String(maxLength: 255, unicode: false),
                        size = c.String(maxLength: 5, fixedLength: true, unicode: false),
                        active = c.Int(),
                        image_url = c.String(maxLength: 255, unicode: false),
                        more_image = c.String(unicode: false),
                        is_hot = c.Int(),
                        is_new = c.Int(),
                        is_sale = c.Int(),
                        is_active = c.Int(nullable: false),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.product_categories", t => t.product_category_id)
                .Index(t => t.product_category_id);
            
            CreateTable(
                "dbo.colors",
                c => new
                    {
                        id = c.Int(nullable: false),
                        image_url = c.String(nullable: false, maxLength: 255, unicode: false),
                        code = c.Int(),
                        color_name = c.String(maxLength: 100),
                        product_id = c.Int(nullable: false),
                        is_active = c.Int(),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.id, t.image_url })
                .ForeignKey("dbo.products", t => t.product_id)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.product_categories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name_product_category = c.String(maxLength: 255),
                        category_id = c.Int(),
                        is_active = c.Int(nullable: false),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.categories", t => t.category_id)
                .Index(t => t.category_id);
            
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name_category = c.String(maxLength: 255),
                        is_active = c.Int(nullable: false),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.rating",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        customer_id = c.Int(nullable: false),
                        product_id = c.Int(nullable: false),
                        value = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.products", t => t.product_id)
                .ForeignKey("dbo.customers", t => t.customer_id)
                .Index(t => t.customer_id)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.sales",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_id = c.Int(),
                        price_sale = c.Double(),
                        date_sale = c.DateTime(),
                        amount_sale = c.Int(),
                        is_active = c.Int(),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.products", t => t.product_id)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 50, unicode: false),
                        status = c.Int(),
                        role = c.Int(),
                        username = c.String(maxLength: 100, unicode: false),
                        pass = c.String(unicode: false),
                        phone = c.String(maxLength: 15, unicode: false),
                        is_active = c.Int(nullable: false),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.news",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        summary = c.String(),
                        content = c.String(unicode: false, storeType: "text"),
                        imageUrl = c.String(maxLength: 255, unicode: false),
                        is_hot = c.Int(),
                        is_new = c.Int(),
                        type_new = c.Int(),
                        status = c.Int(),
                        is_active = c.Int(nullable: false),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.user_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.userRoles",
                c => new
                    {
                        roleId = c.Int(nullable: false),
                        customerId = c.Int(nullable: false),
                        note = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => new { t.roleId, t.customerId })
                .ForeignKey("dbo.roles", t => t.roleId)
                .ForeignKey("dbo.customers", t => t.customerId)
                .Index(t => t.roleId)
                .Index(t => t.customerId);
            
            CreateTable(
                "dbo.roles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 250),
                        code = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.slides",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        summary = c.String(),
                        image_url = c.String(maxLength: 255, unicode: false),
                        slug = c.String(maxLength: 255, unicode: false),
                        type = c.Int(),
                        status = c.Int(),
                        is_active = c.Int(nullable: false),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.userRoles", "customerId", "dbo.customers");
            DropForeignKey("dbo.userRoles", "roleId", "dbo.roles");
            DropForeignKey("dbo.rating", "customer_id", "dbo.customers");
            DropForeignKey("dbo.orders", "customer_id", "dbo.customers");
            DropForeignKey("dbo.orders", "user_id", "dbo.users");
            DropForeignKey("dbo.news", "user_id", "dbo.users");
            DropForeignKey("dbo.orderDetails", "orderId", "dbo.orders");
            DropForeignKey("dbo.sales", "product_id", "dbo.products");
            DropForeignKey("dbo.rating", "product_id", "dbo.products");
            DropForeignKey("dbo.products", "product_category_id", "dbo.product_categories");
            DropForeignKey("dbo.product_categories", "category_id", "dbo.categories");
            DropForeignKey("dbo.orderDetails", "productId", "dbo.products");
            DropForeignKey("dbo.colors", "product_id", "dbo.products");
            DropForeignKey("dbo.cartDetails", "productId", "dbo.products");
            DropForeignKey("dbo.customers", "CustomerType_id", "dbo.customerTypes");
            DropForeignKey("dbo.carts", "customerId", "dbo.customers");
            DropForeignKey("dbo.cartDetails", "cartId", "dbo.carts");
            DropIndex("dbo.userRoles", new[] { "customerId" });
            DropIndex("dbo.userRoles", new[] { "roleId" });
            DropIndex("dbo.news", new[] { "user_id" });
            DropIndex("dbo.sales", new[] { "product_id" });
            DropIndex("dbo.rating", new[] { "product_id" });
            DropIndex("dbo.rating", new[] { "customer_id" });
            DropIndex("dbo.product_categories", new[] { "category_id" });
            DropIndex("dbo.colors", new[] { "product_id" });
            DropIndex("dbo.products", new[] { "product_category_id" });
            DropIndex("dbo.orderDetails", new[] { "productId" });
            DropIndex("dbo.orderDetails", new[] { "orderId" });
            DropIndex("dbo.orders", new[] { "user_id" });
            DropIndex("dbo.orders", new[] { "customer_id" });
            DropIndex("dbo.customers", new[] { "CustomerType_id" });
            DropIndex("dbo.carts", new[] { "customerId" });
            DropIndex("dbo.cartDetails", new[] { "productId" });
            DropIndex("dbo.cartDetails", new[] { "cartId" });
            DropTable("dbo.slides");
            DropTable("dbo.roles");
            DropTable("dbo.userRoles");
            DropTable("dbo.news");
            DropTable("dbo.users");
            DropTable("dbo.sales");
            DropTable("dbo.rating");
            DropTable("dbo.categories");
            DropTable("dbo.product_categories");
            DropTable("dbo.colors");
            DropTable("dbo.products");
            DropTable("dbo.orderDetails");
            DropTable("dbo.orders");
            DropTable("dbo.customerTypes");
            DropTable("dbo.customers");
            DropTable("dbo.carts");
            DropTable("dbo.cartDetails");
        }
    }
}
