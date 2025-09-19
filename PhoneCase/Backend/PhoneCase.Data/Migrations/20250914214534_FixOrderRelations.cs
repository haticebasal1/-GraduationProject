using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7dd21677-49a5-4d13-9006-dc909993c4b9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDate", "SecurityStamp" },
                values: new object[] { "cece6a12-0b7b-4118-9aaf-ad19c88228ed", "AQAAAAIAAYagAAAAEPuTHPPU+0532Au4dXnOumYnS/VjKW/qAxIMJEiUVuKPDPC4yMAFsj8c19+trC7wPg==", new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 675, DateTimeKind.Unspecified).AddTicks(4040), new TimeSpan(0, 0, 0, 0, 0)), "e4c1cb6f-a222-4bcb-8bf1-9cf0977b9439" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9309f4e0-4338-4af3-bf2b-fb9b1a2061f7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDate", "SecurityStamp" },
                values: new object[] { "3917977f-c753-45ea-ae37-1682f0cae483", "AQAAAAIAAYagAAAAEBXGAIArXPQaoucqRU0m2qCgP3F6DmktecYhBjxsUAdcc0W2tHnf7kEiB8+1NVxdYA==", new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6190), new TimeSpan(0, 0, 0, 0, 0)), "1d2dce59-0517-43ac-814c-f4366e56db4d" });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 710, DateTimeKind.Unspecified).AddTicks(1710), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 710, DateTimeKind.Unspecified).AddTicks(1710), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(3370), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(3380), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(3380), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(3380), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(3380), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(3380), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(3380), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(3380), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(3380), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(3380), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5980), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5980), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5980), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5990), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5990), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5990), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5990), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5990), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5990), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5990), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5990), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5990), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5990), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(5990), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6000), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6000), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6000), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6000), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6000), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6000), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6000), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6000), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6000), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6000), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6010), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6010), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6010), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6010), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6010), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6010), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6010), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6010), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6010), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6010), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6010), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6030), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 14, 21, 45, 34, 640, DateTimeKind.Unspecified).AddTicks(6030), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7dd21677-49a5-4d13-9006-dc909993c4b9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDate", "SecurityStamp" },
                values: new object[] { "da12aa8f-3dfb-4966-aefe-3a5533d4810a", "AQAAAAIAAYagAAAAEDzNDsqNUzz9elAdq+ay+MwoG2DSASH1haz+WvJ1PTRr0TtaFtf2i/onOBAqlchw4g==", new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 690, DateTimeKind.Unspecified).AddTicks(3540), new TimeSpan(0, 0, 0, 0, 0)), "e91c3fd3-4877-4f88-8e50-575e7c946e0f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9309f4e0-4338-4af3-bf2b-fb9b1a2061f7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDate", "SecurityStamp" },
                values: new object[] { "5009e8df-f86d-44b9-85b0-455cd2902253", "AQAAAAIAAYagAAAAEOvcHZ9MO2K1v/G4NeppuAeiZ8DkcWAFVMRjmt78+wFmAmDZYeJ/T0WA7UxbAjn6SA==", new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4590), new TimeSpan(0, 0, 0, 0, 0)), "d6a4d100-6ad2-40cd-8d95-f6be5fd439c0" });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 725, DateTimeKind.Unspecified).AddTicks(3530), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 725, DateTimeKind.Unspecified).AddTicks(3530), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(1920), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(1920), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(1930), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(1930), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(1930), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(1930), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(1930), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(1930), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(1930), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(1930), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4440), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4450), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4450), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4450), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4450), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4450), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4450), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4450), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4460), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4490), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4490), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 9, 7, 22, 44, 0, 654, DateTimeKind.Unspecified).AddTicks(4490), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
