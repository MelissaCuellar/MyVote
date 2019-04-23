using Microsoft.EntityFrameworkCore.Migrations;

namespace MyVote.Web.Migrations
{
    public partial class addimagevotingevents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "VotingEvents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "VotingEvents");
        }
    }
}
