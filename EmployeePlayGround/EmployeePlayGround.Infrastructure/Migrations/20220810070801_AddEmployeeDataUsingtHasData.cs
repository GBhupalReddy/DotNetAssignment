using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePlayGround.Infrastructure.Migrations
{
    public partial class AddEmployeeDataUsingtHasData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Emp",
                table: "Employee",
                columns: new[] { "Id", "DepartmentId", "Name", "Salary" },
                values: new object[] { 4, 3, "Ram Bhupal", 15000m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Emp",
                table: "Employee",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
