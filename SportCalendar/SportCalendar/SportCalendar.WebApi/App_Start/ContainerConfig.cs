using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace SportCalendar.WebApi.App_Start
{
    public class ContainerConfig
    {
        public static void ConfigureContainer()
        {

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // register interfaces

            Autofac.IContainer container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}