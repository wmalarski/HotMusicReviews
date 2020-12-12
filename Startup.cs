using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotMusicReviews.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using HotMusicReviews.Services;
using MongoDB.Driver;
using HotMusicReviews.GraphQL.Schema;

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

            ConfigureMongoDb(services);

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

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
            var client = new MongoClient(settings.ConnectionString);
            return client.GetDatabase(settings.DatabaseName);
        }

    }
}
