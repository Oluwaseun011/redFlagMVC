using Microsoft.EntityFrameworkCore.Migrations;

namespace redFlag.Migrations
{
    public partial class qwe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Staffs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_BranchId",
                table: "Staffs",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Branches_BranchId",
                table: "Staffs",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Branches_BranchId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_BranchId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Staffs");
        }
    }
}
