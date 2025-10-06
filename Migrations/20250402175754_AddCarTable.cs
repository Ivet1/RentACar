using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    /// <inheritdoc />
    public partial class AddCarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Cars Table
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            // Insert Seed Data
            migrationBuilder.Sql(@"
        INSERT INTO Cars (Brand, Model, Year, Seats, Description, PricePerDay, IsAvailable)
        VALUES
            ('Toyota', 'Supra Mark IV', 1994, 2, 'Legendary Japanese sports car known for its tuning potential.', 150.00, 1),
            ('Nissan', 'Skyline GT-R R32', 1990, 2, 'The Godzilla of Japanese performance cars, known for its dominance in motorsports.', 180.00, 1),
            ('Mazda', 'RX-7 FD3S', 1992, 2, 'Iconic sports car with a unique rotary engine, renowned for its handling.', 160.00, 1),
            ('Honda', 'NSX', 1990, 2, 'Honda’s answer to Ferrari, with input from Ayrton Senna and exceptional handling.', 220.00, 1),
            ('Mitsubishi', '3000GT VR-4', 1991, 2, 'Packed with advanced technology like all-wheel drive and twin-turbo V6.', 190.00, 1),
            ('Chevrolet', 'Corvette C4', 1990, 2, 'Classic American sports car with a V8 engine, known for its performance and style.', 170.00, 1);
    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop Cars Table
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
