using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Api.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                schema: "manufacturing",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "manufacturing",
                newName: "orders",
                newSchema: "manufacturing");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "manufacturing",
                table: "orders",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                schema: "manufacturing",
                table: "orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                schema: "manufacturing",
                table: "orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                schema: "manufacturing",
                table: "orders",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                schema: "manufacturing",
                table: "orders");

            migrationBuilder.RenameTable(
                name: "orders",
                schema: "manufacturing",
                newName: "Orders",
                newSchema: "manufacturing");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "manufacturing",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                schema: "manufacturing",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                schema: "manufacturing",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                schema: "manufacturing",
                table: "Orders",
                column: "Id");
        }
    }
}
