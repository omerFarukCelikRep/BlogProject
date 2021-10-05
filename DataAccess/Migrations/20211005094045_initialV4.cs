using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initialV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("545b3ede-ad45-495b-bb35-a787ce7b1732"), "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Biography", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModifiedDate", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("b7010a89-68ca-40e7-8c9d-a7b44bea9028"), 0, null, "42c16f4e-db6f-4a23-8baa-adcab6d04a4c", new DateTime(2021, 10, 5, 12, 40, 44, 311, DateTimeKind.Local).AddTicks(1340), null, "admin@email.com", false, false, null, null, "Admin", null, null, "AQAAAAEAACcQAAAAECPfBeUdr2L6Tt2OFwOzNBU0d14Lp15QMQ3abd3QCkcIiVY8yXaCctW6r9pzTHkrTA==", "1234567890", false, null, null, 1, false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("545b3ede-ad45-495b-bb35-a787ce7b1732"), new Guid("b7010a89-68ca-40e7-8c9d-a7b44bea9028") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("545b3ede-ad45-495b-bb35-a787ce7b1732"), new Guid("b7010a89-68ca-40e7-8c9d-a7b44bea9028") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("545b3ede-ad45-495b-bb35-a787ce7b1732"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b7010a89-68ca-40e7-8c9d-a7b44bea9028"));
        }
    }
}
