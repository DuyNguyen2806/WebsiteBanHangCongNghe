using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteBanHangCongNghe.Migrations
{
    /// <inheritdoc />
    public partial class intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    img = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "blog",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    short_desc = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    img = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    short_description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    imgs = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    short_description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "deliveryment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deliveryment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Instock",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "0: Hết hàng, 1:Còn hàng")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instock", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    fee = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    short_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    slug = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    imgs = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    brand_id = table.Column<int>(type: "int", nullable: false),
                    discount = table.Column<double>(type: "float", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true),
                    isTrend = table.Column<bool>(type: "bit", nullable: true),
                    isVisible = table.Column<bool>(type: "bit", nullable: true),
                    Instock_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                    table.ForeignKey(
                        name: "FK_Product_Brand",
                        column: x => x.brand_id,
                        principalTable: "Brand",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Product_Instock",
                        column: x => x.Instock_id,
                        principalTable: "Instock",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.id);
                    table.ForeignKey(
                        name: "FK_admin_Role",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gender = table.Column<bool>(type: "bit", nullable: false),
                    address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    randomKey = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    commentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comment_Product",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    dateorder = table.Column<DateTime>(type: "datetime", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    datedelivery = table.Column<DateTime>(type: "datetime", nullable: true),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    status_id = table.Column<int>(type: "int", nullable: false),
                    note = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    fee = table.Column<double>(type: "float", nullable: false),
                    pay_id = table.Column<int>(type: "int", nullable: false),
                    admin_id = table.Column<int>(type: "int", nullable: true),
                    delivery_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK_Order_Payment",
                        column: x => x.pay_id,
                        principalTable: "Payment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Order_Status",
                        column: x => x.status_id,
                        principalTable: "Status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Order_User",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Order_admin",
                        column: x => x.admin_id,
                        principalTable: "admin",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Order_deliveryment",
                        column: x => x.delivery_id,
                        principalTable: "deliveryment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    discount = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_admin_role_id",
                table: "admin",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_product_id",
                table: "Comment",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_admin_id",
                table: "Order",
                column: "admin_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_delivery_id",
                table: "Order",
                column: "delivery_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_pay_id",
                table: "Order",
                column: "pay_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_status_id",
                table: "Order",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_user_id",
                table: "Order",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_order_id",
                table: "OrderDetail",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_product_id",
                table: "OrderDetail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_brand_id",
                table: "Product",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_category_id",
                table: "Product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Instock_id",
                table: "Product",
                column: "Instock_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_role_id",
                table: "User",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "blog");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "deliveryment");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Instock");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
