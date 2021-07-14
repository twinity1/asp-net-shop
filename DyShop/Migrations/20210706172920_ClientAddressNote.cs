using Microsoft.EntityFrameworkCore.Migrations;

namespace DyShop.Migrations
{
    public partial class ClientAddressNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fcc149b9-10eb-4baf-9012-d14d9b5eef77", "b7c7ed02-76e6-4917-9132-bdb374b58896" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fcc149b9-10eb-4baf-9012-d14d9b5eef77");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7c7ed02-76e6-4917-9132-bdb374b58896");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "ClientAddresses",
                type: "text",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Note",
                table: "ClientAddresses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fcc149b9-10eb-4baf-9012-d14d9b5eef77", "fdb36574-3cad-4d12-ab75-84a414e920c3", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b7c7ed02-76e6-4917-9132-bdb374b58896", 0, "ed635d99-ebb2-4d17-a263-71a1b30c8fe0", "admin@dyshop.cz", true, true, null, "ADMIN@DYSHOP.CZ", "ADMIN@DYSHOP.CZ", "AQAAAAEAACcQAAAAEK9nm3QJECVT/kLb1UoMG5pSHV0fR0GK+mDIYEFtCYKQel/Zkkat/R7bBlxcB6SpyQ==", null, false, "755cf88e-31c7-462d-9636-b68210a781f6", false, "admin@dyshop.cz" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "fcc149b9-10eb-4baf-9012-d14d9b5eef77", "b7c7ed02-76e6-4917-9132-bdb374b58896" });
        }
    }
}
