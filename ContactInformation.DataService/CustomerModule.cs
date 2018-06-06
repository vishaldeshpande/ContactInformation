using Autofac;
using Autofac.Integration.WebApi;
using ContactInformation.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace ContactInformation.DataService
{
    public class CustomerModule
    {
        public static IContainer container;

        public static void Initialize(HttpConfiguration config)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<CustomerContactDBContext>().As<CustomerContactDBContext>();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
            //builder.RegisterType<ICustomerRepository>().As<IGenericRepository<Customer>>();
            //builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            IGenericRepository<Customer> customerRepository = new GenericRepository<Customer>(new CustomerContactDBContext());
            builder.Register<IGenericRepository<Customer>>(a => customerRepository);

            IGenericRepository<CustomerContact> customerContactRepository = new GenericRepository<CustomerContact>(new CustomerContactDBContext());
            builder.Register<IGenericRepository<CustomerContact>>(a => customerContactRepository);

            container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}