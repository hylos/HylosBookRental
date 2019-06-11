namespace HylosBookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserDetailsInAccount : DbMigration
    {
        public override void Up()
        {
            
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "MembershipTypeId");
            DropColumn("dbo.AspNetUsers", "Disable");
            DropColumn("dbo.AspNetUsers", "BithDate");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "LastName");
            
        }
    }
}
