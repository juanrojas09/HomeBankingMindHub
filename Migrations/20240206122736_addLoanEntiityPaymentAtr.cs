using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeBankingNetMvc.Migrations
{
    /// <inheritdoc />
    public partial class addLoanEntiityPaymentAtr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Payments",
                table: "ClientLoans",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payments",
                table: "ClientLoans");
        }
    }
}
