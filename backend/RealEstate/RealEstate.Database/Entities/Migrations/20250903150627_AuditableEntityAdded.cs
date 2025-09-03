using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Database.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AuditableEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Properties",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Favorites",
                newName: "ModifiedBy");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Properties",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Favorites",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Favorites");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Properties",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Favorites",
                newName: "UserName");
        }
    }
}
