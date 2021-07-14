using Microsoft.EntityFrameworkCore.Migrations;

namespace DyShop.Migrations
{
    public partial class OnDeleteBehProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

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

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1e5e9dc4-c84b-4d91-81f2-68fcec4ebb8d", "b75acd3e-1531-46ac-9844-86b577f0751a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4e298050-5ba0-4e28-803c-caaa7beb5e3f", 0, "37816837-07f4-4d86-b395-bcc72ed32f72", "admin@dyshop.cz", true, true, null, "ADMIN@DYSHOP.CZ", "ADMIN@DYSHOP.CZ", "AQAAAAEAACcQAAAAED0L5nIWdjyxDZi/6K28NhVZEBmIhki2ES72kdZ8BlFTKhd+o7AJERmfS8fqNKep0Q==", null, false, "ed0c58fc-facb-4257-8442-8d49b7cba713", false, "admin@dyshop.cz" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1e5e9dc4-c84b-4d91-81f2-68fcec4ebb8d", "4e298050-5ba0-4e28-803c-caaa7beb5e3f" });

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1e5e9dc4-c84b-4d91-81f2-68fcec4ebb8d", "4e298050-5ba0-4e28-803c-caaa7beb5e3f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e5e9dc4-c84b-4d91-81f2-68fcec4ebb8d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4e298050-5ba0-4e28-803c-caaa7beb5e3f");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
