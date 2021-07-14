using Microsoft.EntityFrameworkCore.Migrations;

namespace DyShop.Migrations
{
    public partial class NullableNoteClientAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "57baf4b3-8fef-4177-b3c6-18244ca9adb2", "68df39e4-05dd-43bf-922f-7ba21ab072ad" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57baf4b3-8fef-4177-b3c6-18244ca9adb2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "68df39e4-05dd-43bf-922f-7ba21ab072ad");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "ClientAddresses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "ClientAddresses",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "57baf4b3-8fef-4177-b3c6-18244ca9adb2", "e37ceb81-6e32-4580-80b1-4a49420f43e6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "68df39e4-05dd-43bf-922f-7ba21ab072ad", 0, "c150a48e-b374-4364-99f3-d784dc1c676c", "admin@dyshop.cz", true, true, null, "ADMIN@DYSHOP.CZ", "ADMIN@DYSHOP.CZ", "AQAAAAEAACcQAAAAEKLehmSwConSrWEialyUGWVEhmOvUT74dDQh2YPe0Yz3PL6PsTAyEwh1TBjfS1A4iQ==", null, false, "021265dc-ed06-41e1-8c78-6dc6fc89fadc", false, "admin@dyshop.cz" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "57baf4b3-8fef-4177-b3c6-18244ca9adb2", "68df39e4-05dd-43bf-922f-7ba21ab072ad" });
        }
    }
}
