using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagement_Model.Migrations
{
    /// <inheritdoc />
    public partial class init7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "aryanprajapati2112001@gmail.com",
                column: "Password",
                value: "orM+mYfowlQ2G8r87ViiRep7qRnq8JMRlpSmjgG9Wf4=");
        }
    }
}
