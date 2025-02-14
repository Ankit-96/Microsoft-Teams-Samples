﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using CallingBotSample.AdaptiveCards;
using CallingBotSample.Bots;
using CallingBotSample.Options;
using CallingBotSample.Services.MicrosoftGraph;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Graph.Communications.Common.Telemetry;

namespace CallingBotSample
{

    public class Startup
    {
        private readonly GraphLogger logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this.logger = new GraphLogger(typeof(Startup).Assembly.GetName().Name);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOptions();
            services.AddMemoryCache();

            services.AddSingleton<IGraphLogger>(this.logger);

            // Create the Bot Framework Authentication to be used with the Bot Adapter.
            services.AddSingleton<BotFrameworkAuthentication, ConfigurationBotFrameworkAuthentication>();

            // Create the Bot Framework Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

            // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
            services.AddTransient<IBot, MessageBot>();
            services.AddTransient<CallingBot>();

            // Create the Conversation state passing in the storage layer.
            services.AddSingleton(new ConversationState(new MemoryStorage()));

            services.Configure<AzureAdOptions>(Configuration.GetSection("AzureAd"));
            services.Configure<BotOptions>(Configuration.GetSection("Bot"));

            services.AddSingleton<IAdaptiveCardFactory, AdaptiveCardFactory>();

            services.AddMicrosoftGraphServices(options => Configuration.Bind("AzureAd", options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();

            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseWebSockets()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            // app.UseHttpsRedirection();
        }
    }
}
