using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test2.Migrations
{
    public partial class CreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    direction = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    country = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    state = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    postalcode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    unit_price = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    photo = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    cost = table.Column<decimal>(type: "decimal(16,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCategory = table.Column<int>(type: "int", nullable: false),
                    idProduct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Category",
                        column: x => x.idCategory,
                        principalTable: "Category",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Product",
                        column: x => x.idProduct,
                        principalTable: "Product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    lastname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    photo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.role,
                        principalTable: "Role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "SizeProduct",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSize = table.Column<int>(type: "int", nullable: false),
                    idProduct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_SizeProduct_Product",
                        column: x => x.idProduct,
                        principalTable: "Product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SizeProduct_Size",
                        column: x => x.idSize,
                        principalTable: "Size",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    idUser = table.Column<long>(type: "bigint", nullable: false),
                    idProduct = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Cart_Product",
                        column: x => x.idProduct,
                        principalTable: "Product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Cart_User",
                        column: x => x.idUser,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser = table.Column<long>(type: "bigint", nullable: false),
                    total = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.id);
                    table.ForeignKey(
                        name: "FK_Sale_User",
                        column: x => x.idUser,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser = table.Column<long>(type: "bigint", nullable: false),
                    idAddress = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserAddress_Address",
                        column: x => x.idAddress,
                        principalTable: "Address",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_UserAddress_User",
                        column: x => x.idUser,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Concept",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSale = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    import = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    idProduct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concept", x => x.id);
                    table.ForeignKey(
                        name: "FK_Concept_Product",
                        column: x => x.idProduct,
                        principalTable: "Product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Concept_Sale",
                        column: x => x.idSale,
                        principalTable: "Sale",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_idProduct",
                table: "Cart",
                column: "idProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_idUser",
                table: "Cart",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_idCategory",
                table: "CategoryProduct",
                column: "idCategory");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_idProduct",
                table: "CategoryProduct",
                column: "idProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Concept_idProduct",
                table: "Concept",
                column: "idProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Concept_idSale",
                table: "Concept",
                column: "idSale");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_idUser",
                table: "Sale",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_SizeProduct_idProduct",
                table: "SizeProduct",
                column: "idProduct");

            migrationBuilder.CreateIndex(
                name: "IX_SizeProduct_idSize",
                table: "SizeProduct",
                column: "idSize");

            migrationBuilder.CreateIndex(
                name: "Email_Unique",
                table: "User",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_role",
                table: "User",
                column: "role");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_idAddress",
                table: "UserAddress",
                column: "idAddress");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_idUser",
                table: "UserAddress",
                column: "idUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "Concept");

            migrationBuilder.DropTable(
                name: "SizeProduct");

            migrationBuilder.DropTable(
                name: "UserAddress");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
