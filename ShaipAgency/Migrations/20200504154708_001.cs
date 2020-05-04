using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShaipAgency.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ShaipName = table.Column<string>(type: "Nvarchar(20)", nullable: true),
                    IsWithdraw = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_REQ_TIMESTAMPS",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    RequestNo = table.Column<string>(type: "varchar(15)", nullable: true),
                    DetailNo = table.Column<int>(nullable: false),
                    EventCode = table.Column<string>(type: "char(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_REQ_TIMESTAMPS", x => x.Timestamp);
                });

            migrationBuilder.CreateTable(
                name: "TB_STD_EVENT_CODE",
                columns: table => new
                {
                    EventCode = table.Column<string>(type: "char(2)", nullable: false),
                    EventName = table.Column<string>(type: "Nvarchar(20)", nullable: false),
                    UseYN = table.Column<bool>(type: "bit", nullable: false),
                    EventDescription = table.Column<string>(type: "Nvarchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STD_EVENT_CODE", x => x.EventCode);
                });

            migrationBuilder.CreateTable(
                name: "TB_STD_REQUEST_CODE",
                columns: table => new
                {
                    RequestCode = table.Column<string>(type: "Char(2)", nullable: false),
                    RequestName = table.Column<string>(type: "NvarChar(20)", nullable: false),
                    UseYN = table.Column<bool>(type: "bit", nullable: false),
                    CreationUserId = table.Column<int>(nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STD_REQUEST_CODE", x => x.RequestCode);
                });

            migrationBuilder.CreateTable(
                name: "TB_STD_REQUEST_STATUS_CODE",
                columns: table => new
                {
                    RequestStatusCode = table.Column<string>(type: "char(2)", nullable: false),
                    RequestStatusName = table.Column<string>(type: "NvarChar(20)", nullable: false),
                    UseYN = table.Column<bool>(type: "bit", nullable: false),
                    RequestAlertPriority = table.Column<int>(nullable: false),
                    CreationUserId = table.Column<int>(nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STD_REQUEST_STATUS_CODE", x => x.RequestStatusCode);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_USER_ACTIVITY",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RequestCount = table.Column<int>(type: "int", nullable: false),
                    CancellationCount = table.Column<int>(type: "int", nullable: false),
                    BacktoBackCancellationCount = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    Blocked = table.Column<int>(type: "int", nullable: false),
                    SignUpDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastSignInDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    WithdrawalDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER_ACTIVITY", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_TB_USER_ACTIVITY_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_REQ_MASTERS",
                columns: table => new
                {
                    RequestNo = table.Column<string>(type: "varchar(15)", nullable: false),
                    RequestCode = table.Column<string>(nullable: true),
                    RequestUserId = table.Column<int>(type: "int", nullable: false),
                    RequestTitle = table.Column<string>(type: "Nvarchar(256)", nullable: true),
                    DetailCount = table.Column<int>(type: "int", nullable: false),
                    RemainClaims = table.Column<int>(type: "int", nullable: false),
                    DeliveryGroupNo = table.Column<string>(type: "varchar(15)", nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_REQ_MASTERS", x => x.RequestNo);
                    table.ForeignKey(
                        name: "FK_TB_REQ_MASTERS_TB_STD_REQUEST_CODE_RequestCode",
                        column: x => x.RequestCode,
                        principalTable: "TB_STD_REQUEST_CODE",
                        principalColumn: "RequestCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_STD_REQUEST_STATUS_ROUTE_INFO",
                columns: table => new
                {
                    RequestCode = table.Column<string>(nullable: false),
                    EventCode = table.Column<string>(nullable: false),
                    NextStatusCode = table.Column<string>(nullable: true),
                    CreationUserId = table.Column<int>(nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STD_REQUEST_STATUS_ROUTE_INFO", x => new { x.RequestCode, x.EventCode });
                    table.ForeignKey(
                        name: "FK_TB_STD_REQUEST_STATUS_ROUTE_INFO_TB_STD_EVENT_CODE_EventCode",
                        column: x => x.EventCode,
                        principalTable: "TB_STD_EVENT_CODE",
                        principalColumn: "EventCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_STD_REQUEST_STATUS_ROUTE_INFO_TB_STD_REQUEST_STATUS_CODE_NextStatusCode",
                        column: x => x.NextStatusCode,
                        principalTable: "TB_STD_REQUEST_STATUS_CODE",
                        principalColumn: "RequestStatusCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_STD_REQUEST_STATUS_ROUTE_INFO_TB_STD_REQUEST_CODE_RequestCode",
                        column: x => x.RequestCode,
                        principalTable: "TB_STD_REQUEST_CODE",
                        principalColumn: "RequestCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_REQ_DETAILS_DEPOSIT",
                columns: table => new
                {
                    RequestNo = table.Column<string>(nullable: false),
                    DepositDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    PersonName = table.Column<string>(type: "Nvarchar(20)", nullable: true),
                    AccountNumber = table.Column<string>(type: "Nvarchar(40)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_REQ_DETAILS_DEPOSIT", x => x.RequestNo);
                    table.ForeignKey(
                        name: "FK_TB_REQ_DETAILS_DEPOSIT_TB_REQ_MASTERS_RequestNo",
                        column: x => x.RequestNo,
                        principalTable: "TB_REQ_MASTERS",
                        principalColumn: "RequestNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_USER_PASSBOOK",
                columns: table => new
                {
                    RequestNo = table.Column<string>(nullable: false),
                    EventCode = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER_PASSBOOK", x => new { x.RequestNo, x.EventCode });
                    table.ForeignKey(
                        name: "FK_TB_USER_PASSBOOK_TB_STD_EVENT_CODE_EventCode",
                        column: x => x.EventCode,
                        principalTable: "TB_STD_EVENT_CODE",
                        principalColumn: "EventCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_USER_PASSBOOK_TB_REQ_MASTERS_RequestNo",
                        column: x => x.RequestNo,
                        principalTable: "TB_REQ_MASTERS",
                        principalColumn: "RequestNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_USER_PASSBOOK_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TB_REQ_MASTERS_RequestCode",
                table: "TB_REQ_MASTERS",
                column: "RequestCode");

            migrationBuilder.CreateIndex(
                name: "IX_TB_STD_REQUEST_STATUS_ROUTE_INFO_EventCode",
                table: "TB_STD_REQUEST_STATUS_ROUTE_INFO",
                column: "EventCode");

            migrationBuilder.CreateIndex(
                name: "IX_TB_STD_REQUEST_STATUS_ROUTE_INFO_NextStatusCode",
                table: "TB_STD_REQUEST_STATUS_ROUTE_INFO",
                column: "NextStatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USER_PASSBOOK_EventCode",
                table: "TB_USER_PASSBOOK",
                column: "EventCode");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USER_PASSBOOK_UserId",
                table: "TB_USER_PASSBOOK",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TB_REQ_DETAILS_DEPOSIT");

            migrationBuilder.DropTable(
                name: "TB_REQ_TIMESTAMPS");

            migrationBuilder.DropTable(
                name: "TB_STD_REQUEST_STATUS_ROUTE_INFO");

            migrationBuilder.DropTable(
                name: "TB_USER_ACTIVITY");

            migrationBuilder.DropTable(
                name: "TB_USER_PASSBOOK");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TB_STD_REQUEST_STATUS_CODE");

            migrationBuilder.DropTable(
                name: "TB_STD_EVENT_CODE");

            migrationBuilder.DropTable(
                name: "TB_REQ_MASTERS");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TB_STD_REQUEST_CODE");
        }
    }
}
