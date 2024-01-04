using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class DBmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JoblistModels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoblistModels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TaskListModels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Done = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskListModels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    T_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.T_ID);
                });

            migrationBuilder.CreateTable(
                name: "TeamJoblists",
                columns: table => new
                {
                    T_JID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamJobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamJoblists", x => x.T_JID);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TeamTaskNames",
                columns: table => new
                {
                    T_TID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TTaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamJobName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamTaskNames", x => x.T_TID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    U_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RandomCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.U_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoblistModels");

            migrationBuilder.DropTable(
                name: "TaskListModels");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "TeamJoblists");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "TeamTaskNames");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
