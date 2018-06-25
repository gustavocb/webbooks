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
            Sql("INSERT INTO book(isbn, titulo, autor, ano, sinopse) VALUES(8525432180, 'SAPIENS - UMA BREVE HISTÓRIA DA HUMANIDADE', 1, 2015, 'O autor repassa a história da humanidade, ou do homo sapiens, desde o surgimento da espécie durante a pré-história até o presente, mas em vez de apenas inventariar os fatos históricos, ele os relaciona com questões do presente e os questiona de maneira surpreendente. Além disso, para cada fato ou crença que temos como certa hoje em dia, o autor apresenta as diversas interpretações existentes a partir de diferentes pontos de vista, inclusive as muito atuais, e vai além, sugerindo interpretações muitas vezes desconcertantes.')");
            Sql("INSERT INTO book(isbn, titulo, autor, ano, sinopse) VALUES(8595550115, 'O INFERNO SOMOS NÓS', 2, 2018, 'Em tempos adversos, de crise, preconceito e intolerância, como transformar o ódio em compreensão do outro em suas diferenças? Como sair de um cenário de violência e construir uma cultura de paz?  O historiador Leandro Karnal e a Monja Coen, fundadora da Comunidade Zen-budista do Brasil, conversam nesse livro sobre essas e outras questões. Os autores lembram que o medo pode estar na origem da violência e apontam como o conhecimento, de si e do outro, é capaz de produzir uma nova atitude na sociedade, menos agressiva e mais acolhedora.')");
            Sql("INSERT INTO book(isbn, titulo, autor, ano, sinopse) VALUES(8539004119, 'O PODER DO HÁBITO', 3, 2012, 'Durante os últimos dois anos, uma jovem transformou quase todos os aspectos de sua vida. PArou de fumar, correu uma maratona e foi promovida. EM um laboratório, neurologistas descobriram que os padrões dentro do cérebro dela mudaram de maneira fundamental. PUblicitários da Procter & Gamble observaram vídeos de pessoas fazendo a cama. TEntavam desesperadamente descobrir como vender um novo produto chamado Febreze, que estava prestes a se tornar um dos maiores fracassos na história da empresa. DE repente, um deles detecta um padrão quase imperceptível - e, com uma sutil mudança na campanha publicitária, Febreze começa a vender um bilhão de dólares por anos. UM diretor executivo pouco conhecido assume uma das maiores empresas norte-americanas. SEu primeiro passo é atacar um único padrão entre os funcionários - a maneira como lidam com a segurança no ambiente de trabalho -, e logo a empresa começa a ter o melhor desempenho no índice Dow Jones. O Que todas essas pessoas tem em comum? Conseguiram ter sucesso focando em padrões que moldam cada aspecto de nossas vidas. TIveram êxito transformando hábitos. COm perspicácia e habilidade, Charles Duhigg apresenta um novo entendimento da natureza humana e seu potencial para a transformação.')");
            Sql("INSERT INTO book(isbn, titulo, autor, ano, sinopse) VALUES(8504020266, 'COMO FAZER AMIGOS E INFLUENCIAR PESSOAS (LIVRO DE BOLSO)', 4, 2016, 'Não é por acaso que, mais de setenta anos depois de sua primeira edição, depois de mais de 50 milhóes de exemplares vendidos, \"Como fazer amigos e influenciar pessoas\" segue sendo um livro inovador, e uma das principais referências do mundo sobre relacionamentos, seja no âmbito profissional ou pesssoal. Os conselhos, métodos e as ideias de Dale Carnegie já beneficiaram milhões de pesssoas, e permanecem completamente atuais. Carnegie fornece, nesse livro, técnicas e métodos, de maneira extremamente direta, para que qualquer pessoa alcance seus objetivos pessoais e profissionais. Esta edição contém o texto integral, com um novo design, em formato compacto.')");
            Sql("INSERT INTO book(isbn, titulo, autor, ano, sinopse) VALUES(8544105378, 'A ELITE DO ATRASO', 4, 2017, 'Numa época em que a questão das desigualdades racial e social estão, mais do que nunca, no centro de cena - dos grandes veículos de comunicação aos comentários nas redes sociais e até mesmo nas conversas das mesas de bar, onde todos parecem ter uma ideia muito bem definida do que é capaz de construir um país ideal -, o sociólogo Jessé Souza escancara o pacto dos donos do poder para perpetuar uma sociedade cruel forjada na escravidão. Esse é o pilar de sustentação de nossa elite, A Elite do Atraso. Depois da polêmica aberta pela obra A Tolice da Inteligência Brasileira e da contundência exposta em A Radiografia do Golpe, o autor apresenta obra surpreendente, forte, inovadora e crítica na essência, com um texto aguerrido e acessível. A Elite do Atraso é um livro para ser apoiado, debatido ou questionado - mas será impossível reagir de maneira indiferente à leitura contundente de Jessé Souza a ideias difundidas na academia e na mídia.')");
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
