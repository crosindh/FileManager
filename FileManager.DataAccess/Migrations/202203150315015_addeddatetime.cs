namespace FileManager.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FileInformations", "UploadDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FileInformations", "UploadDate", c => c.DateTime(nullable: false));
        }
    }
}
