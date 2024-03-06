using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagement_Model.Migrations
{
    /// <inheritdoc />
    public partial class init9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "aryanprajapati2112001@gmail.com",
                column: "Password",
                value: "orM+mYfowlQ2G8r87ViiRep7qRnq8JMRlpSmjgG9Wf4=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "aryanprajapati2112001@gmail.com",
                column: "Password",
                value: null);
        }
    }
}
