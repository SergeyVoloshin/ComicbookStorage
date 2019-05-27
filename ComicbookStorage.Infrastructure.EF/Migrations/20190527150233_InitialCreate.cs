using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComicbookStorage.Infrastructure.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comicbook",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    CoverExtension = table.Column<string>(maxLength: 4, nullable: false),
                    UserFriendlyId = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comicbook", x => x.Id);
                    table.UniqueConstraint("AK_Comicbook_UserFriendlyId", x => x.UserFriendlyId);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Subject = table.Column<string>(maxLength: 255, nullable: false),
                    Body = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Salt = table.Column<string>(maxLength: 255, nullable: false),
                    EncryptionIterationCount = table.Column<int>(nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false),
                    ConfirmationCode = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.UniqueConstraint("AK_User_Email", x => x.Email);
                    table.UniqueConstraint("AK_User_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Recipient = table.Column<string>(maxLength: 255, nullable: false),
                    Subject = table.Column<string>(maxLength: 255, nullable: false),
                    Body = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastSendingAttemptTime = table.Column<DateTime>(nullable: true),
                    LastSendingError = table.Column<string>(nullable: true),
                    EmailTemplateId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Email_EmailTemplate_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalTable: "EmailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "Body", "Subject" },
                values: new object[] { 1, @"You recently registered the user {User.Name} on {ApplicationName}<br/>
Please confirm your email by clicking the link below.<br/>
<a href=""{ConfirmationLink}"">Confirm this email address</a>", "[{ApplicationName}] Confirm your email address" });

            migrationBuilder.CreateIndex(
                name: "IX_Email_EmailTemplateId",
                table: "Email",
                column: "EmailTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comicbook");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "EmailTemplate");
        }
    }
}
