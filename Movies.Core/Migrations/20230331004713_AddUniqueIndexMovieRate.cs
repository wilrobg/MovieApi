using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexMovieRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MovieRate_MovieId",
                table: "MovieRate");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRate_MovieId_UserId",
                table: "MovieRate",
                columns: new[] { "MovieId", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MovieRate_MovieId_UserId",
                table: "MovieRate");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRate_MovieId",
                table: "MovieRate",
                column: "MovieId");
        }
    }
}
