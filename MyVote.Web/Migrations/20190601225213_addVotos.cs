using Microsoft.EntityFrameworkCore.Migrations;

namespace MyVote.Web.Migrations
{
    public partial class addVotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumVotos",
                table: "Candidates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumVotos",
                table: "Candidates");
        }
    }
}
