namespace CommuneServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Results : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("public.Cn", "Cmd", "public.Cm");
            DropIndex("public.Cn", new[] { "Cmd" });
            DropIndex("public.RC", new[] { "Cid" });
            DropIndex("public.RL", new[] { "Lid" });
            DropPrimaryKey("public.Cm");
            CreateTable(
                "public.St",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LSEncrypted = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.RPV",
                c => new
                    {
                        IdPV = c.Guid(nullable: false),
                        IdSt = c.String(nullable: false, maxLength: 128),
                        TSEncrypted = c.String(),
                        PVPEncrypted = c.String(),
                        FEPEncrypted = c.String(),
                        Source = c.Int(nullable: false),
                        CB = c.String(),
                        CA = c.DateTime(nullable: false),
                        UBy = c.String(),
                        UA = c.DateTime(),
                        VB = c.String(),
                        VA = c.DateTime(),
                    })
                .PrimaryKey(t => t.IdPV)
                .ForeignKey("public.St", t => t.IdSt)
                .Index(t => t.IdSt);
            
            CreateTable(
                "public.W",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IdW = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.St", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("public.Cm", "IdC", c => c.Long(nullable: false));
            AddColumn("public.RB", "NP", c => c.Int(nullable: false));
            AddColumn("public.RB", "NVE", c => c.Int(nullable: false));
            AddColumn("public.RB", "NVA", c => c.Int(nullable: false));
            AddColumn("public.RB", "NVC", c => c.Int(nullable: false));
            AddColumn("public.RB", "IS", c => c.Boolean(nullable: false));
            AddColumn("public.RB", "IF", c => c.Boolean(nullable: false));
            AddColumn("public.RB", "FPEncrypted", c => c.String());
            AddColumn("public.RB", "Source", c => c.Int(nullable: false));
            AddColumn("public.RC", "IdRL", c => c.String(nullable: false, maxLength: 128));
            AddColumn("public.RC", "NO", c => c.Int(nullable: false));
            AddColumn("public.RL", "IdRB", c => c.String(nullable: false, maxLength: 128));
            AddColumn("public.RL", "NC", c => c.Int(nullable: false));
            AddColumn("public.RL", "NO", c => c.Int(nullable: false));
            AddColumn("public.RL", "IF", c => c.Boolean(nullable: false));
            AlterColumn("public.Cn", "Cmd", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("public.Cm", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("public.Cm", "CW", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("public.Cm", "Id");
            CreateIndex("public.Cn", "Cmd");
            CreateIndex("public.RL", new[] { "IdRB", "Lid" }, unique: true);
            CreateIndex("public.RC", new[] { "IdRL", "Cid" }, unique: true);
            CreateIndex("public.Cm", "Id");
            CreateIndex("public.Cm", "CW");
            AddForeignKey("public.RL", "IdRB", "public.RB", "Id");
            AddForeignKey("public.RC", "IdRL", "public.RL", "Id");
            AddForeignKey("public.Cm", "Id", "public.St", "Id");
            AddForeignKey("public.Cm", "CW", "public.W", "Id");
            AddForeignKey("public.Cn", "Cmd", "public.Cm", "Id");
            DropColumn("public.Cm", "CC");
            DropColumn("public.Cm", "NCEncrypted");
        }
        
        public override void Down()
        {
            AddColumn("public.Cm", "NCEncrypted", c => c.String());
            AddColumn("public.Cm", "CC", c => c.String());
            DropForeignKey("public.Cn", "Cmd", "public.Cm");
            DropForeignKey("public.W", "Id", "public.St");
            DropForeignKey("public.Cm", "CW", "public.W");
            DropForeignKey("public.Cm", "Id", "public.St");
            DropForeignKey("public.RPV", "IdSt", "public.St");
            DropForeignKey("public.RC", "IdRL", "public.RL");
            DropForeignKey("public.RL", "IdRB", "public.RB");
            DropIndex("public.W", new[] { "Id" });
            DropIndex("public.Cm", new[] { "CW" });
            DropIndex("public.Cm", new[] { "Id" });
            DropIndex("public.RPV", new[] { "IdSt" });
            DropIndex("public.RC", new[] { "IdRL", "Cid" });
            DropIndex("public.RL", new[] { "IdRB", "Lid" });
            DropIndex("public.Cn", new[] { "Cmd" });
            DropPrimaryKey("public.Cm");
            AlterColumn("public.Cm", "CW", c => c.String());
            AlterColumn("public.Cm", "Id", c => c.Long(nullable: false));
            AlterColumn("public.Cn", "Cmd", c => c.Long(nullable: false));
            DropColumn("public.RL", "IF");
            DropColumn("public.RL", "NO");
            DropColumn("public.RL", "NC");
            DropColumn("public.RL", "IdRB");
            DropColumn("public.RC", "NO");
            DropColumn("public.RC", "IdRL");
            DropColumn("public.RB", "Source");
            DropColumn("public.RB", "FPEncrypted");
            DropColumn("public.RB", "IF");
            DropColumn("public.RB", "IS");
            DropColumn("public.RB", "NVC");
            DropColumn("public.RB", "NVA");
            DropColumn("public.RB", "NVE");
            DropColumn("public.RB", "NP");
            DropColumn("public.Cm", "IdC");
            DropTable("public.W");
            DropTable("public.RPV");
            DropTable("public.St");
            AddPrimaryKey("public.Cm", "Id");
            CreateIndex("public.RL", "Lid", unique: true);
            CreateIndex("public.RC", "Cid", unique: true);
            CreateIndex("public.Cn", "Cmd");
            AddForeignKey("public.Cn", "Cmd", "public.Cm", "Id", cascadeDelete: true);
        }
    }
}
