using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePlayGround.Infrastructure.Migrations
{
    public partial class AddEmployeesusingSeedmethodEmployeeId8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { 8, 2, "Venkat", 12000m });

            migrationBuilder.InsertData(
                schema: "Emp",
                table: "Employee",
                columns: new[] { "Id", "DepartmentId", "Name", "Salary" },
                values: new object[] { 9, 2, "Ramana", 20000m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Emp",
                table: "Employee",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Emp",
                table: "Employee",
                keyColumn: "Id",
                keyValue: 9);

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
    }
}
