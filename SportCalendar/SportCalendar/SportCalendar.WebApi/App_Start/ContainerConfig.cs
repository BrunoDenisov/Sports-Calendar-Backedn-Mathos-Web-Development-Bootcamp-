﻿using Autofac;
using Autofac.Integration.WebApi;
using SportCalendar.Repository;
using SportCalendar.RepositoryCommon;
using SportCalendar.Service;
using SportCalendar.ServiceCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http;

namespace SportCalendar.WebApi.App_Start
{
    public class ContainerConfig
    {
        public static void ConfigureContainer()
        {

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<SportRepository>().As<ISportRepository>();
            builder.RegisterType<SportService>().As<ISportService>();
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>();
            builder.RegisterType<ReviewService>().As<IReviewService>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Controller"));

            //registering interfaces for table User
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<UserService>().As<IUserService>();

            //County interface
            builder.RegisterType<CountyService>().As<ICountyService>();
            builder.RegisterType<CountyRepository>().As<ICountyRepository>();

            //Sponsor interface
            builder.RegisterType<SponsorService>().As<ISponsorService>();
            builder.RegisterType<SponsorRepository>().As<ISponsorRepository>();
            //City registration interface
            builder.RegisterType<CityService>().As<ICityService>();
            builder.RegisterType<CityRepository>().As<ICityRepository>();

            //Location registration interface
            builder.RegisterType<LocationService>().As<ILocationService>();
            builder.RegisterType<LocationRepository>().As<ILocationRepository>();

            // register interfaces

            Autofac.IContainer container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}