namespace webbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.author",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.book",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        isbn = c.Long(nullable: false),
                        titulo = c.String(nullable: false, maxLength: 60),
                        autor = c.Int(),
                        ano = c.Int(nullable: false),
                        sinopse = c.String(maxLength: 2500),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.author", t => t.autor)
                .Index(t => t.autor);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            // Insert Authors for example
            Sql("INSERT INTO author(nome) VALUES('HARARI, YUVAL NOAH')");
            Sql("INSERT INTO author(nome) VALUES('KARNAL, LEANDRO')");
            Sql("INSERT INTO author(nome) VALUES('DUHIGG, CHARLES')");
            Sql("INSERT INTO author(nome) VALUES('CARNEGIE, DALE')");
            Sql("INSERT INTO author(nome) VALUES('SOUZA, JESSE')");

            // Insert Books for example
            Sql("INSERT INTO book(isbn, titulo, autor, ano, sinopse) VALUES(8525432180, 'SAPIENS - UMA BREVE HIST�RIA DA HUMANIDADE', 1, 2015, 'O autor repassa a hist�ria da humanidade, ou do homo sapiens, desde o surgimento da esp�cie durante a pr�-hist�ria at� o presente, mas em vez de apenas inventariar os fatos hist�ricos, ele os relaciona com quest�es do presente e os questiona de maneira surpreendente. Al�m disso, para cada fato ou cren�a que temos como certa hoje em dia, o autor apresenta as diversas interpreta��es existentes a partir de diferentes pontos de vista, inclusive as muito atuais, e vai al�m, sugerindo interpreta��es muitas vezes desconcertantes.')");
            Sql("INSERT INTO book(isbn, titulo, autor, ano, sinopse) VALUES(8595550115, 'O INFERNO SOMOS N�S', 2, 2018, 'Em tempos adversos, de crise, preconceito e intoler�ncia, como transformar o �dio em compreens�o do outro em suas diferen�as? Como sair de um cen�rio de viol�ncia e construir uma cultura de paz?  O historiador Leandro Karnal e a Monja Coen, fundadora da Comunidade Zen-budista do Brasil, conversam nesse livro sobre essas e outras quest�es. Os autores lembram que o medo pode estar na origem da viol�ncia e apontam como o conhecimento, de si e do outro, � capaz de produzir uma nova atitude na sociedade, menos agressiva e mais acolhedora.')");
            Sql("INSERT INTO book(isbn, titulo, autor, ano, sinopse) VALUES(8539004119, 'O PODER DO H�BITO', 3, 2012, 'Durante os �ltimos dois anos, uma jovem transformou quase todos os aspectos de sua vida. PArou de fumar, correu uma maratona e foi promovida. EM um laborat�rio, neurologistas descobriram que os padr�es dentro do c�rebro dela mudaram de maneira fundamental. PUblicit�rios da Procter & Gamble observaram v�deos de pessoas fazendo a cama. TEntavam desesperadamente descobrir como vender um novo produto chamado Febreze, que estava prestes a se tornar um dos maiores fracassos na hist�ria da empresa. DE repente, um deles detecta um padr�o quase impercept�vel - e, com uma sutil mudan�a na campanha publicit�ria, Febreze come�a a vender um bilh�o de d�lares por anos. UM diretor executivo pouco conhecido assume uma das maiores empresas norte-americanas. SEu primeiro passo � atacar um �nico padr�o entre os funcion�rios - a maneira como lidam com a seguran�a no ambiente de trabalho -, e logo a empresa come�a a ter o melhor desempenho no �ndice Dow Jones. O Que todas essas pessoas tem em comum? Conseguiram ter sucesso focando em padr�es que moldam cada aspecto de nossas vidas. TIveram �xito transformando h�bitos. COm perspic�cia e habilidade, Charles Duhigg apresenta um novo entendimento da natureza humana e seu potencial para a transforma��o.')");
            Sql("INSERT INTO book(isbn, titulo, autor, ano, sinopse) VALUES(8504020266, 'COMO FAZER AMIGOS E INFLUENCIAR PESSOAS (LIVRO DE BOLSO)', 4, 2016, 'N�o � por acaso que, mais de setenta anos depois de sua primeira edi��o, depois de mais de 50 milh�es de exemplares vendidos, \"Como fazer amigos e influenciar pessoas\" segue sendo um livro inovador, e uma das principais refer�ncias do mundo sobre relacionamentos, seja no �mbito profissional ou pesssoal. Os conselhos, m�todos e as ideias de Dale Carnegie j� beneficiaram milh�es de pesssoas, e permanecem completamente atuais. Carnegie fornece, nesse livro, t�cnicas e m�todos, de maneira extremamente direta, para que qualquer pessoa alcance seus objetivos pessoais e profissionais. Esta edi��o cont�m o texto integral, com um novo design, em formato compacto.')");
            Sql("INSERT INTO book(isbn, titulo, autor, ano, sinopse) VALUES(8544105378, 'A ELITE DO ATRASO', 4, 2017, 'Numa �poca em que a quest�o das desigualdades racial e social est�o, mais do que nunca, no centro de cena - dos grandes ve�culos de comunica��o aos coment�rios nas redes sociais e at� mesmo nas conversas das mesas de bar, onde todos parecem ter uma ideia muito bem definida do que � capaz de construir um pa�s ideal -, o soci�logo Jess� Souza escancara o pacto dos donos do poder para perpetuar uma sociedade cruel forjada na escravid�o. Esse � o pilar de sustenta��o de nossa elite, A Elite do Atraso. Depois da pol�mica aberta pela obra A Tolice da Intelig�ncia Brasileira e da contund�ncia exposta em A Radiografia do Golpe, o autor apresenta obra surpreendente, forte, inovadora e cr�tica na ess�ncia, com um texto aguerrido e acess�vel. A Elite do Atraso � um livro para ser apoiado, debatido ou questionado - mas ser� imposs�vel reagir de maneira indiferente � leitura contundente de Jess� Souza a ideias difundidas na academia e na m�dia.')");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.book", "autor", "dbo.author");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.book", new[] { "autor" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.book");
            DropTable("dbo.author");
        }
    }
}
