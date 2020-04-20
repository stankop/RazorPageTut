using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPageTut.Services.Migrations
{
    public partial class spGetEmployeeById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"create procedure spGetEmployeeByID
                                @Id int
                                as
                                begin
	                                select * from Employees where Id =@Id 
                                end";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"drop procedure spGetEmployeeByID";
            migrationBuilder.Sql(procedure);
        }
    }
}
