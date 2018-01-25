using AutoMapper;
using CoruscantMarketplace.Core.Business;
using CoruscantMarketplace.Core.Impl.Business;
using CoruscantMarketplace.Core.Models;
using CoruscantMarketplace.Crosscutting;
using CoruscantMarketplace.DataLayer.Impl.Entities;
using CoruscantMarketplace.DataLayer.Impl.Repositories;
using CoruscantMarketplace.DataLayer.Repositories;
using CoruscantMarketplace.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace CoruscantMarketplace
{
    /// <summary>
    /// Classe de configuração do Startup da aplicação ASP.Net Core
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Construtor da classe de startup do projeto
        /// </summary>
        /// <param name="env">
        /// Instância do IHostingEnvironment
        /// </param>
        public Startup(IHostingEnvironment env)
        {
            //Configuração para recuperar as propriedades definidas no appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// Retorna a instância do IConfiguration gerada no Startup
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Método para configuração dos serviços a serem usados no container da aplicação.
        /// Chamado pelo runtime.
        /// </summary>
        /// <param name="services">
        /// Instância do IServiceCollection
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            
            //Injeção de dependencia da entidade de configurações do appsettings.json
            services.Configure<Configuracoes>(Configuration.GetSection("Configuracoes"));

            //Carregar os valores nas propriedades estáticas
            Configuracoes configuracores = Configuration.GetSection("Configuracoes").Get<Configuracoes>();
            
            //Configura as Injeções de Dependência
            services.AddScoped<IProdutoBusiness, ProdutoBusiness>();
            services.AddScoped<IAuthTokenBusiness, AuthTokenBusiness>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            //Configura o AutoMapper
            services.AddAutoMapper(cfg =>
            {
                //Define que o AutoMapper irá automaticamente gerar o mapeamento automatico
                //Utilizando como padrões LowerUnderscoreNaming (fonte dos dados), PascalCaseNaming (destino dos dados)
                cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
                cfg.CreateMissingTypeMaps = true;
                cfg.ValidateInlineMaps = false;

                //Configura o para os mapeamentos entre ModelBase e EntityBase (nos dois sentidos)
                //A abstração do elemento Id para ObjectId e String
                //Inclui as classes que herdam destas duas entidades bases
                cfg.CreateMap<Produto, ProdutoEntity>()
                    .ForMember(eb => eb.Id, opt => opt.MapFrom(mb => new ObjectId(mb.Id)));

                cfg.CreateMap<ProdutoEntity, Produto>()
                    .ForMember(mb => mb.Id, opt => opt.MapFrom(eb => eb.Id.ToString()));

            });

            services.AddMvc();

            // Habilita o uso do Auth0
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = "https://coruscantmarketplace.auth0.com/";
                options.Audience = "http://localhost:57665/api";
            });

            // Habilita o uso do Swagger
            services.AddSwaggerGen(c =>
            {
                // Define informações do cabeçalho da página do Swagger
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Coruscant Marketplace",
                    Version = "v1",
                    Description = "Documentação das APIs do Coruscant Marketplace",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Breno Machado", Email = "brenomachado@gmail.com", Url = "https://www.linkedin.com/in/brenomachado/" },
                    License = new License { Name = "CC BY 4.0", Url = "https://creativecommons.org/licenses/by/4.0/" }

                });

                // Define onde está a documentação da API gerada automaticamente através dos comentários no .Net
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "CoruscantMarketplace.xml");
                c.IncludeXmlComments(xmlPath);

                // Inclui o esquema de autenticação necessária para a API do Produto
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Necessita uso do Token de autorização JWT usando o Bearer esquema. Exemplo: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Método para a configuração do Pipeline da requisição HTTP. 
        /// Chamado pelo runtime
        /// </summary>
        /// <param name="app">
        /// Instancia do IApplicationBuilder
        /// </param>
        /// <param name="env">
        /// Instancia do IHostingEnvironment
        /// </param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                Configuracoes.Ambiente = Configuracoes.TipoAmbiente.Desenvolvimento;

                app.UseDeveloperExceptionPage();
            }
            else
            {
                Configuracoes.Ambiente = Configuracoes.TipoAmbiente.Producao;
            }

            // Inclusão do middleware para tratamento de exceções
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            // Habilita o uso do Auth0
            app.UseAuthentication();

            // Habilita o uso do Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coruscant Marketplace V1");
            });

            app.UseMvc();
        }
    }
}
