using SOTI.CureWell.DAL;
using SOTI.CureWell.DAL.Interfaces;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace SOTI.CureWell.WebAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IDoctor, CureWellRepository>();
            container.RegisterType<ISurgery, CureWellRepository>();
            container.RegisterType<ISpecialization, CureWellRepository>();
            container.RegisterType<IDoctorSpecialization, CureWellRepository>();
            container.RegisterType<IUser, CureWellRepository>();
            container.RegisterType<IBookAppointment, CureWellRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}