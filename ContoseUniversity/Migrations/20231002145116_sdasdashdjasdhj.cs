using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContoseUniversity.Migrations
{
    public partial class sdasdashdjasdhj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseAssignment",
                table: "CourseAssignment");

            migrationBuilder.DropIndex(
                name: "IX_CourseAssignment_CourseId",
                table: "CourseAssignment");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CourseAssignment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseAssignment",
                table: "CourseAssignment",
                columns: new[] { "CourseId", "InstructorId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseAssignment",
                table: "CourseAssignment");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CourseAssignment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseAssignment",
                table: "CourseAssignment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseAssignment_CourseId",
                table: "CourseAssignment",
                column: "CourseId");
        }
    }
}
