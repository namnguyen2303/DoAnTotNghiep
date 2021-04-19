use master 
go
create database TranDungShop
go
use TranDungShop
go
create table users(
id int identity(1,1) primary key not null,
code varchar(50) not null,
[status] int ,
[role] int ,
username varchar(100),
pass varchar(max),
phone varchar(15),
is_active int not null,
created_at datetime
)
go 
create table slides
(
id int identity(1,1) not null primary key,
summary nvarchar(max),
image_url varchar(255),
slug varchar(255),
[type] int, 
[status] int, 
is_active int not null,
created_at datetime
)
go
create table news(
id int identity(1,1) not null primary key,
[user_id] int not null,
summary nvarchar(max),
content text,
imageUrl varchar(255),
is_hot int,
is_new int,
type_new int,
[status] int, 
is_active int not null,
created_at datetime,
Constraint fk_new_u foreign key([user_id]) references users(id)
)

go
create table categories(
id int identity(1,1) not null primary key,
name_category nvarchar(255),
is_active int not null,
created_at datetime
)
go
create table product_categories(
id int identity(1,1) not null primary key,
name_product_category nvarchar(255),
category_id int,
is_active int not null,
created_at datetime,
Constraint fk_cate foreign key(category_id) references categories(id)
)
go
create table products(
id int identity(1,1) not null primary key,
product_category_id int not null,
--cart_id int not null,
code varchar(50),
product_name nvarchar(255),
price_start float,
price_sale float,
summary nvarchar(255),
[description] nvarchar(255),
detail text,
slug varchar(255),
size char(5),
active int,
image_url varchar(255),
more_image varchar(max),
is_hot int,
is_new int,
is_sale int,
is_active int not null,
created_at datetime,
Constraint fk_proCate foreign key(product_category_id) references product_categories(id),
--Constraint fk_proCart foreign key(cart_id) references cart(id)
)
go
create table customers(
id int identity(1,1) primary key not null,
code varchar(50),
name_customer nvarchar(50),
phone varchar(15),
pass varchar(50),
address_customer nvarchar(255),
gender int,
[type] int, 
[status] int, 
DOB datetime,
email varchar(100),
avatar_url varchar(255),
is_active int not null,
created_at datetime ,
)
go
create table cart(
id int identity(1,1) not null primary key,
[status] int,
total_price float,
customer_id int not null,
product_id int not null,
date_buy datetime,
note nvarchar(255),
is_active int not null,
created_at datetime,
Constraint fk_pro foreign key(product_id) references products(id),
Constraint fk_cus_cart foreign key(customer_id) references customers(id),
)

go
create table payments(
id int not null primary key identity(1,1),
code varchar(255),
[status] int,
[type] int,
paymentDate datetime,
amount int,
customer_id int not null,
constraint fk_cus_pay foreign key(customer_id) references customers(id)
)
go
create table orders(
id int identity(1,1) not null primary key,
customer_id int not null,
[user_id] int not null,
[status] int,
total_price float,
date_buy datetime,
note nvarchar(255),
is_active int not null,
created_at datetime,
Constraint fk_cus_order foreign key(customer_id) references customers(id),
Constraint fk_u_order foreign key([user_id]) references users(id)
)
create table order_detail
(
id int identity(1,1) not null primary key,
order_id int not null,
product_id int not null,
payment_id int not null,
date_buy datetime,
sum_price float,
quanity int,
is_active int not null,
created_at datetime,
Constraint fk_pro_o foreign key(product_id) references products(id),
Constraint fk_pay foreign key(payment_id) references payments(id),
Constraint fk_o_od foreign key(order_id) references orders(id)
)

go
create table colors(
id int identity(1,1) not null,
code int,
color_name nvarchar(100),
image_url varchar(255) not null,
product_id int not null,
is_active int,
created_at datetime,
constraint pk_color primary key(id,image_url),
constraint fk_clor foreign key(product_id) references products(id)
)
go

create table sales(
id int IDENTITY(1, 1) primary key not null,
product_id int,
price_sale float,
date_sale datetime,
amount_sale int,
is_active int,
created_at datetime,
constraint fk_pro_sale foreign key(product_id) references products(id)
)
go
create table rating(
id int identity(1,1) not null primary key,
customer_id int not null,
product_id int not null,
[value] int,
constraint fk_rat_pro foreign key(product_id) references products(id),
constraint fk_rat_cus foreign key(customer_id) references customers(id),
)