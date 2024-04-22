﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackendLearning.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddAgeUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "users");
        }
    }
}
