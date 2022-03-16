namespace FileManager.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddepartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileInformations", "Department", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileInformations", "Department");
        }
    }
}
