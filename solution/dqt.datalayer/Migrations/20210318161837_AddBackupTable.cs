﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace dqt.datalayer.Migrations
{
    public partial class AddBackupTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QualifiedTeachersBackup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Trn = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DoB = table.Column<DateTime>(nullable: false),
                    NINumber = table.Column<string>(nullable: true),
                    QTSAwardDate = table.Column<DateTime>(nullable: false),
                    ITTSubject1Code = table.Column<string>(nullable: true),
                    ITTSubject2Code = table.Column<string>(nullable: true),
                    ITTSubject3Code = table.Column<string>(nullable: true),
                    ActiveAlert = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualifiedTeachersBackup", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QualifiedTeachersBackup");
        }
    }
}
