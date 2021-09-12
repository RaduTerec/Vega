using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO `vega`.`makes` (`Name`) VALUES ('Opel')");
            migrationBuilder.Sql("INSERT INTO `vega`.`makes` (`Name`) VALUES ('VW')");
            migrationBuilder.Sql("INSERT INTO `vega`.`makes` (`Name`) VALUES ('Peugeot')");

            migrationBuilder.Sql("INSERT INTO `vega`.`models` (`Name`, `MakeId`) VALUES ('Corsa', (SELECT ID FROM `vega`.`makes` WHERE `Name`='Opel'))");
            migrationBuilder.Sql("INSERT INTO `vega`.`models` (`Name`, `MakeId`) VALUES ('Astra', (SELECT ID FROM `vega`.`makes` WHERE `Name`='Opel'))");
            migrationBuilder.Sql("INSERT INTO `vega`.`models` (`Name`, `MakeId`) VALUES ('Insignia', (SELECT ID FROM `vega`.`makes` WHERE `Name`='Opel'))");
            migrationBuilder.Sql("INSERT INTO `vega`.`models` (`Name`, `MakeId`) VALUES ('Polo', (SELECT ID FROM `vega`.`makes` WHERE `Name`='VW'))");
            migrationBuilder.Sql("INSERT INTO `vega`.`models` (`Name`, `MakeId`) VALUES ('Golf', (SELECT ID FROM `vega`.`makes` WHERE `Name`='VW'))");
            migrationBuilder.Sql("INSERT INTO `vega`.`models` (`Name`, `MakeId`) VALUES ('Borra', (SELECT ID FROM `vega`.`makes` WHERE `Name`='VW'))");
            migrationBuilder.Sql("INSERT INTO `vega`.`models` (`Name`, `MakeId`) VALUES ('Passat', (SELECT ID FROM `vega`.`makes` WHERE `Name`='VW'))");
            migrationBuilder.Sql("INSERT INTO `vega`.`models` (`Name`, `MakeId`) VALUES ('308', (SELECT ID FROM `vega`.`makes` WHERE `Name`='Peugeot'))");
            migrationBuilder.Sql("INSERT INTO `vega`.`models` (`Name`, `MakeId`) VALUES ('408', (SELECT ID FROM `vega`.`makes` WHERE `Name`='Peugeot'))");
            migrationBuilder.Sql("INSERT INTO `vega`.`models` (`Name`, `MakeId`) VALUES ('508', (SELECT ID FROM `vega`.`makes` WHERE `Name`='Peugeot'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM `vega`.`makes` WHERE `Name` IN ('Opel', 'VW', 'Peugeot')");
        }
    }
}
