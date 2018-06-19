using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Context.Polling;
using Housing.Selection.Context.Selection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Housing.Selection.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HousingSelectionDbContext>();

            services.AddMvc();

            services.AddTransient<IBatchRepository, BatchRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<INameRepository, NameRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();

            services.AddTransient<IServiceBatchCalls, ServiceBatchCalls>();
            services.AddTransient<IServiceRoomCalls, ServiceRoomCalls>();
            services.AddTransient<IServiceUserCalls, ServiceUserCalls>();
            services.AddTransient<IApiPathBuilder, ApiPathBuilder>();
            services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();

            services.AddTransient<IServiceBatchCalls, ServiceBatchCalls>();
            services.AddTransient<IServiceRoomCalls, ServiceRoomCalls>();
            services.AddTransient<IServiceUserCalls, ServiceUserCalls>();

            services.AddTransient<IPollBatch, PollBatch>();
            services.AddTransient<IPollRoom, PollRoom>();
            services.AddTransient<IPollUser, PollUser>();

            services.AddTransient<ISelectionService, SelectionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddApplicationInsights(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
