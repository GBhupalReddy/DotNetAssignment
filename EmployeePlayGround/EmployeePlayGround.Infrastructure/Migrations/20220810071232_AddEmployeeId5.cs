using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePlayGround.Infrastructure.Migrations
{
    public partial class AddEmployeeId5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Emp",
                table: "Employee",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                schema: "Emp",
                table: "Employee",
                columns: new[] { "Id", "DepartmentId", "Name", "Salary" },
                values: new object[] { 5, 3, "Ram Bhupal", 15000m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Emp",
                table: "Employee",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                schema: "Emp",
                table: "Employee",
                columns: new[] { "Id", "DepartmentId", "Name", "Salary" },
                values: new object[] { 3, 3, "Ram Bhupal", 15000m });
        }
    }
}
