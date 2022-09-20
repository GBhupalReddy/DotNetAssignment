using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePlayGround.Infrastructure.Migrations
{
    public partial class RenameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTable_DepartmentTable_DepartmentId",
                schema: "Emp",
                table: "EmployeeTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTable",
                schema: "Emp",
                table: "EmployeeTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentTable",
                schema: "Emp",
                table: "DepartmentTable");

            migrationBuilder.RenameTable(
                name: "EmployeeTable",
                schema: "Emp",
                newName: "Employee",
                newSchema: "Emp");

            migrationBuilder.RenameTable(
                name: "DepartmentTable",
                schema: "Emp",
                newName: "Department",
                newSchema: "Emp");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeTable_DepartmentId",
                schema: "Emp",
                table: "Employee",
                newName: "IX_Employee_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                schema: "Emp",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                schema: "Emp",
                table: "Department",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                schema: "Emp",
                table: "Employee",
                column: "DepartmentId",
                principalSchema: "Emp",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                schema: "Emp",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                schema: "Emp",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                schema: "Emp",
                table: "Department");

            migrationBuilder.RenameTable(
                name: "Employee",
                schema: "Emp",
                newName: "EmployeeTable",
                newSchema: "Emp");

            migrationBuilder.RenameTable(
                name: "Department",
                schema: "Emp",
                newName: "DepartmentTable",
                newSchema: "Emp");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_DepartmentId",
                schema: "Emp",
                table: "EmployeeTable",
                newName: "IX_EmployeeTable_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTable",
                schema: "Emp",
                table: "EmployeeTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentTable",
                schema: "Emp",
                table: "DepartmentTable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTable_DepartmentTable_DepartmentId",
                schema: "Emp",
                table: "EmployeeTable",
                column: "DepartmentId",
                principalSchema: "Emp",
                principalTable: "DepartmentTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
