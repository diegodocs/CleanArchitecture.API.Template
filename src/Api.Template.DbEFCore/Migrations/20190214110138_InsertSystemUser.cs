using Api.Template.Domain.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Template.DbEFCore.Migrations
{
    public partial class InsertSystemUser : Migration
    {
        protected User user = new UserDefinition().SystemAppUser;

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "User",
               columns: new[] { "Id", "CreateDate", "IsActive", "ModifiedDate", "Name", "Login", "Email", "AuditUserId", "IsApprover" },
               values: new object[,]
               {
                    { user.Id, user.CreateDate, user.IsActive, null, user.Name,user.Login,user.Email,user.AuditUserId, true  },
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
              table: "User",
              keyColumn: "Id",
              keyValue: user.Id);

        }
    }
}
