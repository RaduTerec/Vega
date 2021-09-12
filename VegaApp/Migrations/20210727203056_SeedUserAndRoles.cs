using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega.Migrations
{
    public partial class SeedUserAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bdea8fa1-fc61-423d-9832-ebf111d0cc1e", "863315b4-55cb-4255-bd6f-01df1c8d988f", "Admin", "ADMIN" },
                    { "35dbbc4f-8843-49f7-b3e0-bde48d27b0ea", "0aedbac8-e009-4de0-81cf-6e71cbc0d38d", "Moderator", "MODERATOR" },
                    { "a09194fc-1e87-4e14-a34f-414e10936f9e", "db023a06-a3db-4404-94b9-0c29ebf66fdb", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "231f71e7-6ef6-4da3-be60-f6d09852b9c5", 0, "d82768b9-6679-4e10-9740-7601fec9b157", "admin@vega.com", true, false, null, "John Admin", "ADMIN@VEGA.COM", "ADMIN", "AQAAAAEAACcQAAAAEHT+cGWgCYVA1rdT70icG0MM//immTyibRVOon714znnzZirIaxaV3V9FsXPFAcE3A==", "0721458796", true, "0604ad6a-3a43-4f8c-bd78-3d6620328dd3", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bdea8fa1-fc61-423d-9832-ebf111d0cc1e", "231f71e7-6ef6-4da3-be60-f6d09852b9c5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35dbbc4f-8843-49f7-b3e0-bde48d27b0ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a09194fc-1e87-4e14-a34f-414e10936f9e");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bdea8fa1-fc61-423d-9832-ebf111d0cc1e", "231f71e7-6ef6-4da3-be60-f6d09852b9c5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdea8fa1-fc61-423d-9832-ebf111d0cc1e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "231f71e7-6ef6-4da3-be60-f6d09852b9c5");
        }
    }
}
