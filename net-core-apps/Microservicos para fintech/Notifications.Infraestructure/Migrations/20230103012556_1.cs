using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notifications.Infraestructure.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cellphone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserUpdate = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cellphone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserUpdate = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NotificationChannels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationChannels", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    _userId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserUpdate = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Viewed = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotification_ApplicationUser__userId",
                        column: x => x._userId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BusinessNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    _businessId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserUpdate = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Viewed = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessNotification_Business__businessId",
                        column: x => x._businessId,
                        principalTable: "Business",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BusinessOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    _userId = table.Column<int>(type: "int", nullable: false),
                    _businessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessOwners_ApplicationUser__userId",
                        column: x => x._userId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessOwners_Business__businessId",
                        column: x => x._businessId,
                        principalTable: "Business",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserNotificationChannelRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    _channelId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    _notificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotificationChannelRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotificationChannelRecords_NotificationChannels__channel~",
                        column: x => x._channelId,
                        principalTable: "NotificationChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNotificationChannelRecords_UserNotification__notificatio~",
                        column: x => x._notificationId,
                        principalTable: "UserNotification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserNotificationRecipients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cellphone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    _notificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotificationRecipients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotificationRecipients_UserNotification__notificationId",
                        column: x => x._notificationId,
                        principalTable: "UserNotification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BusinessNotificationChannelRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    _channelId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    _notificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessNotificationChannelRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessNotificationChannelRecords_BusinessNotification__not~",
                        column: x => x._notificationId,
                        principalTable: "BusinessNotification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessNotificationChannelRecords_NotificationChannels__cha~",
                        column: x => x._channelId,
                        principalTable: "NotificationChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BusinessNotificationRecipients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cellphone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    _notificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessNotificationRecipients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessNotificationRecipients_BusinessNotification__notific~",
                        column: x => x._notificationId,
                        principalTable: "BusinessNotification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "Cellphone", "CreatedDate", "Email", "Name", "UpdateDate", "UserUpdate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 1, 2, 22, 25, 56, 44, DateTimeKind.Unspecified).AddTicks(7567), "arthur.muller@capwise.com.br", "Arthur Silva Muller", new DateTime(2023, 1, 2, 22, 25, 56, 41, DateTimeKind.Unspecified).AddTicks(5681), null },
                    { 2, "", new DateTime(2023, 1, 2, 22, 25, 56, 44, DateTimeKind.Unspecified).AddTicks(8970), "pablo@capwise.com.br", "Pablo Maino", new DateTime(2023, 1, 2, 22, 25, 56, 44, DateTimeKind.Unspecified).AddTicks(8935), null },
                    { 3, "", new DateTime(2023, 1, 2, 22, 25, 56, 44, DateTimeKind.Unspecified).AddTicks(8983), "arthur@capwise.com.br", "Arthur Decker", new DateTime(2023, 1, 2, 22, 25, 56, 44, DateTimeKind.Unspecified).AddTicks(8977), null },
                    { 4, "", new DateTime(2023, 1, 2, 22, 25, 56, 44, DateTimeKind.Unspecified).AddTicks(8996), "", "", new DateTime(2023, 1, 2, 22, 25, 56, 44, DateTimeKind.Unspecified).AddTicks(8990), null }
                });

            migrationBuilder.InsertData(
                table: "NotificationChannels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "platform" },
                    { 2, "push" },
                    { 3, "email" },
                    { 4, "sms" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNotification__businessId",
                table: "BusinessNotification",
                column: "_businessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNotificationChannelRecords__channelId",
                table: "BusinessNotificationChannelRecords",
                column: "_channelId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNotificationChannelRecords__notificationId",
                table: "BusinessNotificationChannelRecords",
                column: "_notificationId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNotificationRecipients__notificationId",
                table: "BusinessNotificationRecipients",
                column: "_notificationId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwners__businessId",
                table: "BusinessOwners",
                column: "_businessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwners__userId",
                table: "BusinessOwners",
                column: "_userId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotification__userId",
                table: "UserNotification",
                column: "_userId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationChannelRecords__channelId",
                table: "UserNotificationChannelRecords",
                column: "_channelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationChannelRecords__notificationId",
                table: "UserNotificationChannelRecords",
                column: "_notificationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationRecipients__notificationId",
                table: "UserNotificationRecipients",
                column: "_notificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessNotificationChannelRecords");

            migrationBuilder.DropTable(
                name: "BusinessNotificationRecipients");

            migrationBuilder.DropTable(
                name: "BusinessOwners");

            migrationBuilder.DropTable(
                name: "UserNotificationChannelRecords");

            migrationBuilder.DropTable(
                name: "UserNotificationRecipients");

            migrationBuilder.DropTable(
                name: "BusinessNotification");

            migrationBuilder.DropTable(
                name: "NotificationChannels");

            migrationBuilder.DropTable(
                name: "UserNotification");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "ApplicationUser");
        }
    }
}
