using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationPatients.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    iId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dtDateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sEmergencyContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sEmergencyContactPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sPrimaryCarePhysician = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sDiagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dtAdmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtDischargeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAdmitted = table.Column<bool>(type: "bit", nullable: false),
                    IsDischarged = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.iId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
