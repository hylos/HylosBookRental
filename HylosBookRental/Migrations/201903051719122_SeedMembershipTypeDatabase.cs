namespace HylosBookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMembershipTypeDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[MembershipTypes](Name,SignUpFee,ChargeRateSixMonth,ChargeRateOneMonth) VALUES('Pay Per Rental',0,50,25)");
            Sql("INSERT INTO [dbo].[MembershipTypes](Name,SignUpFee,ChargeRateSixMonth,ChargeRateOneMonth) VALUES('Member',150,20,10)");
            Sql("INSERT INTO [dbo].[MembershipTypes](Name,SignUpFee,ChargeRateSixMonth,ChargeRateOneMonth) VALUES('SuperAdmin',0,0,0)");
        }
        
        public override void Down()
        {
        }
    }
}
