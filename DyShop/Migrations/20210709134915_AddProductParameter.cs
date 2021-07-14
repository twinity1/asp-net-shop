using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DyShop.Migrations
{
    public partial class AddProductParameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f59a4902-200c-4e03-aa90-ddb4ff24882b", "af247a9e-e536-4d08-879a-ff0b227a2679" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f59a4902-200c-4e03-aa90-ddb4ff24882b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "af247a9e-e536-4d08-879a-ff0b227a2679");

            migrationBuilder.CreateTable(
                name: "ProductParameterGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductParameterGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductParameters_ProductParameterGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ProductParameterGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductParameterRelations",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    ParameterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductParameterRelations", x => new { x.ProductId, x.ParameterId });
                    table.ForeignKey(
                        name: "FK_ProductParameterRelations_ProductParameters_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "ProductParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductParameterRelations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "525170bb-45ac-49e9-b910-af6dc0f9b805", "e55b637e-d263-4232-a5d4-11dc2c692a0c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "adfbf15a-61ac-4b1f-9a8c-916d6e85a1e4", 0, "af0da998-bad8-4279-96d1-baf4f4c50b3e", "admin@dyshop.cz", true, true, null, "ADMIN@DYSHOP.CZ", "ADMIN@DYSHOP.CZ", "AQAAAAEAACcQAAAAEKXLINIVHeV/XLNrVE8kgMc9SYlExsQwN0j5uZn5TVV5lb/C25qn/8niyBDWMZqxqg==", null, false, "8d43d577-2d99-4cd2-b097-6656bfb167b6", false, "admin@dyshop.cz" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "525170bb-45ac-49e9-b910-af6dc0f9b805", "adfbf15a-61ac-4b1f-9a8c-916d6e85a1e4" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductParameterRelations_ParameterId",
                table: "ProductParameterRelations",
                column: "ParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductParameters_GroupId",
                table: "ProductParameters",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductParameterRelations");

            migrationBuilder.DropTable(
                name: "ProductParameters");

            migrationBuilder.DropTable(
                name: "ProductParameterGroups");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "525170bb-45ac-49e9-b910-af6dc0f9b805", "adfbf15a-61ac-4b1f-9a8c-916d6e85a1e4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "525170bb-45ac-49e9-b910-af6dc0f9b805");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adfbf15a-61ac-4b1f-9a8c-916d6e85a1e4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f59a4902-200c-4e03-aa90-ddb4ff24882b", "c3cfb292-edf6-411e-b703-1415dfc97dc4", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "af247a9e-e536-4d08-879a-ff0b227a2679", 0, "5861a774-de72-4456-b944-fe7ba6aee076", "admin@dyshop.cz", true, true, null, "ADMIN@DYSHOP.CZ", "ADMIN@DYSHOP.CZ", "AQAAAAEAACcQAAAAEPJFNbsUP40FiZVdFcFky1whBM53nV+7Xb8AF03/wOVXaHP3XeInK4E0yBBUDwCfQw==", null, false, "89b9e46c-9761-4206-8057-2e8287a50bf3", false, "admin@dyshop.cz" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f59a4902-200c-4e03-aa90-ddb4ff24882b", "af247a9e-e536-4d08-879a-ff0b227a2679" });
        }
    }
}
