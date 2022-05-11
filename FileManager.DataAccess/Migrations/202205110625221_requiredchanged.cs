namespace FileManager.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredchanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FileInformations", "Subject", c => c.String());
            AlterColumn("dbo.FileInformations", "Addressee", c => c.String());
            AlterColumn("dbo.FileInformations", "Receivedby", c => c.String());
            AlterColumn("dbo.FileInformations", "Pdfdirectory", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FileInformations", "Pdfdirectory", c => c.String(nullable: false));
            AlterColumn("dbo.FileInformations", "Receivedby", c => c.String(nullable: false));
            AlterColumn("dbo.FileInformations", "Addressee", c => c.String(nullable: false));
            AlterColumn("dbo.FileInformations", "Subject", c => c.String(nullable: false));
        }
    }
}
