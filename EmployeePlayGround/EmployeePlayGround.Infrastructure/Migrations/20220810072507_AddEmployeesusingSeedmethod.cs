using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePlayGround.Infrastructure.Migrations
{
    public partial class AddEmployeesusingSeedmethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { 6, 2, "Kameswari", 0m });

            migrationBuilder.InsertData(
                schema: "Emp",
                table: "Employee",
                columns: new[] { "Id", "DepartmentId", "Name", "Salary" },
                values: new object[] { 7, 2, "Navaneet", 0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Emp",
                table: "Employee",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Emp",
                table: "Employee",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.InsertData(
                schema: "Emp",
                table: "Employee",
                columns: new[] { "Id", "DepartmentId", "Name", "Salary" },
                values: new object[] { 5, 3, "Ram Bhupal", 15000m });
        }
    }
}
