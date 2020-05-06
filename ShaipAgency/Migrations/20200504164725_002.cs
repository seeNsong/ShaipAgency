using Microsoft.EntityFrameworkCore.Migrations;

namespace ShaipAgency.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestStatusCode",
                table: "TB_REQ_DETAILS_DEPOSIT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_REQ_DETAILS_DEPOSIT_RequestStatusCode",
                table: "TB_REQ_DETAILS_DEPOSIT",
                column: "RequestStatusCode");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_REQ_DETAILS_DEPOSIT_TB_STD_REQUEST_STATUS_CODE_RequestStatusCode",
                table: "TB_REQ_DETAILS_DEPOSIT",
                column: "RequestStatusCode",
                principalTable: "TB_STD_REQUEST_STATUS_CODE",
                principalColumn: "RequestStatusCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_REQ_DETAILS_DEPOSIT_TB_STD_REQUEST_STATUS_CODE_RequestStatusCode",
                table: "TB_REQ_DETAILS_DEPOSIT");

            migrationBuilder.DropIndex(
                name: "IX_TB_REQ_DETAILS_DEPOSIT_RequestStatusCode",
                table: "TB_REQ_DETAILS_DEPOSIT");

            migrationBuilder.DropColumn(
                name: "RequestStatusCode",
                table: "TB_REQ_DETAILS_DEPOSIT");
        }
    }
}
