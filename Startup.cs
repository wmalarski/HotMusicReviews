using HotMusicReviews.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using HotMusicReviews.Services;
using MongoDB.Driver;
using HotMusicReviews.GraphQL.Performers;
using HotMusicReviews.GraphQL.Users;
using HotMusicReviews.GraphQL.Albums;
using HotMusicReviews.GraphQL.Reviews;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using HotMusicReviews.GraphQL.LastFm;
using System.Security.Authentication;
using Microsoft.Extensions.Logging;

namespace HotMusicReviews
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ReviewsDatabaseSettings>(Configuration.GetSection(nameof(ReviewsDatabaseSettings)));
            services.AddSingleton<IReviewsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ReviewsDatabaseSettings>>().Value);

            services.Configure<LastFmSettings>(Configuration.GetSection(nameof(LastFmSettings)));
            services.AddSingleton<LastFmSettings>(sp =>
                sp.GetRequiredService<IOptions<LastFmSettings>>().Value
            );

            services.AddHttpClient<LastFmService>(client =>
            {
                var lastFmSettings = Configuration.GetSection(nameof(LastFmSettings)).Get<LastFmSettings>();
                client.BaseAddress = new Uri(lastFmSettings.ApiUrl);
            });

            ConfigureMongoDb(services);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var authSettings = Configuration.GetSection(nameof(AuthSettings)).Get<AuthSettings>();
                options.Authority = authSettings.Authority;
                options.Audience = authSettings.Audience;
            });

            services.AddAuthorization();

            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<PerformerQuery>()
                .AddTypeExtension<AlbumQuery>()
                .AddTypeExtension<ReviewQuery>()
                .AddTypeExtension<LastFmQuery>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<PerformerMutations>()
                .AddTypeExtension<AlbumMutations>()
                .AddTypeExtension<ReviewMutations>()
                .AddDataLoader<AlbumByIdDataLoader>()
                .AddDataLoader<PerformerByIdDataLoader>()
                .AddDataLoader<ReviewByIdDataLoader>()
                .AddDataLoader<AlbumByMBidDataLoader>()
                .AddDataLoader<PerformerByMBidDataLoader>()
                .AddType<PerformerType>()
                .AddType<AlbumType>()
                .AddType<ReviewType>()
                .AddType<UserType>()
                .EnableRelaySupport()
                .AddFiltering()
                .AddSorting()
                .AddAuthorization()
                .AddHttpRequestInterceptor(CurrentUserGlobalState.AuthenticationInterceptor());

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
                loggingBuilder.AddAzureWebAppDiagnostics();
            });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }

        private void ConfigureMongoDb(IServiceCollection services)
        {
            var settings = GetMongoDbSettings();
            services.AddSingleton(_ => CreateMongoDatabase(settings));

            AddMongoDbService<AlbumService, Album>(settings.AlbumsCollectionName);
            AddMongoDbService<PerformerService, Performer>(settings.PerformersCollectionName);
            AddMongoDbService<ReviewService, Review>(settings.ReviewsCollectionName);

            void AddMongoDbService<TService, TModel>(string collectionName)
            {
                services.AddSingleton(sp => sp.GetRequiredService<IMongoDatabase>().GetCollection<TModel>(collectionName));
                services.AddSingleton(typeof(TService));
            }
        }

        private IReviewsDatabaseSettings GetMongoDbSettings() =>
            Configuration.GetSection(nameof(ReviewsDatabaseSettings)).Get<ReviewsDatabaseSettings>();

        private IMongoDatabase CreateMongoDatabase(IReviewsDatabaseSettings settings)
        {
            var clientSettings = MongoClientSettings.FromUrl(new MongoUrl(settings.ConnectionString));
            clientSettings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var client = new MongoClient(clientSettings);
            return client.GetDatabase(settings.DatabaseName);
        }
    }
}
