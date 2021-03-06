using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ICI.Cashback.Infra.Data.Migrations
{
	public partial class InitialMigration : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
					name: "Log",
					columns: table => new
					{
						Id = table.Column<int>(nullable: false)
									.Annotation("Sqlite:Autoincrement", true),
						Date = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
						UserIp = table.Column<string>(maxLength: 12, nullable: false),
						Object = table.Column<string>(maxLength: 1000, nullable: true),
						OperationId = table.Column<int>(nullable: false),
						User = table.Column<string>(maxLength: 50, nullable: true),
						Table = table.Column<string>(maxLength: 50, nullable: true),
						Platform = table.Column<string>(nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Log", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Reseller",
					columns: table => new
					{
						Id = table.Column<int>(nullable: false)
									.Annotation("Sqlite:Autoincrement", true),
						Name = table.Column<string>(maxLength: 100, nullable: false),
						Cpf = table.Column<string>(maxLength: 11, nullable: false),
						Email = table.Column<string>(maxLength: 100, nullable: false),
						Password = table.Column<string>(maxLength: 64, nullable: false),
						Role = table.Column<string>(maxLength: 50, nullable: false),
						Enabled = table.Column<bool>(nullable: false, defaultValue: true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Reseller", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Purchase",
					columns: table => new
					{
						Id = table.Column<int>(nullable: false)
									.Annotation("Sqlite:Autoincrement", true),
						Code = table.Column<string>(maxLength: 30, nullable: false),
						Value = table.Column<float>(nullable: false),
						Date = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
						ResellerId = table.Column<int>(nullable: false),
						Status = table.Column<int>(nullable: false, defaultValue: 1)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Purchase", x => x.Id);
						table.ForeignKey(
											name: "FK_Purchase_Reseller_ResellerId",
											column: x => x.ResellerId,
											principalTable: "Reseller",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateIndex(
					name: "IX_Purchase_ResellerId",
					table: "Purchase",
					column: "ResellerId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "Log");

			migrationBuilder.DropTable(
					name: "Purchase");

			migrationBuilder.DropTable(
					name: "Reseller");
		}
	}
}
