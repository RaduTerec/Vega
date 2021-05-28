using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega.Migrations
{
    public partial class CreateFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Features",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql("INSERT INTO `vega`.`features` (`Name`) VALUES ('Airbags')");
            migrationBuilder.Sql("INSERT INTO `vega`.`features` (`Name`) VALUES ('ABS')");
            migrationBuilder.Sql("INSERT INTO `vega`.`features` (`Name`) VALUES ('ESP')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM `vega`.`features` WHERE `Name` IN ('Airbags', 'ABS', 'ESP')");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Features",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
