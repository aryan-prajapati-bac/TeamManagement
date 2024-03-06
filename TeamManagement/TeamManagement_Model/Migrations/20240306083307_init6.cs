using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagement_Model.Migrations
{
    /// <inheritdoc />
    public partial class init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Login",
                keyColumn: "Email",
                keyValue: "aryanprajapati2112001@gmail.com",
                column: "Password",
                value: "orM+mYfowlQ2G8r87ViiRep7qRnq8JMRlpSmjgG9Wf4=");

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
                table: "Login",
                keyColumn: "Email",
                keyValue: "aryanprajapati2112001@gmail.com",
                column: "Password",
                value: "qVq+2b1o3yUX82d55dzBemgrEXuiz5AE7fdpOYdwAU4=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "aryanprajapati2112001@gmail.com",
                column: "Password",
                value: "team1234");
        }
    }
}
