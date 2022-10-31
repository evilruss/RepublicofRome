[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RoRService.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(RoRService.App_Start.NinjectWebCommon), "Stop")]

namespace RoRService.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;
    using System.Web.Http;
    using WebApiContrib.IoC.Ninject;
    using Repositories.Contracts;
    using Repositories;
    using Resources.Contracts;
    using Resources;
    using Repositories.Helpers;
    using Engines.Contracts;
    using Engines;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                //Support WebAPI
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel); 

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ICardsResource>().To<CardsResource>();
            kernel.Bind<ICardsRepository>().To<CardsSQLiteRepository>();
            kernel.Bind<IEventsRepository>().To<EventsSQLiteRepository>();
            kernel.Bind<IFactionsRepository>().To<FactionsSQLiteRepository>();
            kernel.Bind<IFactionsResource>().To<FactionsResource>();
            kernel.Bind<IGameEngine>().To<GameEngine>();
            kernel.Bind<IGamesRepository>().To<GamesSQLiteRepository>();
            kernel.Bind<IGamesResource>().To<GamesResource>();
            kernel.Bind<ILegionsRepository>().To<LegionsSQLiteRepository>();
            kernel.Bind<IOfficesRepository>().To<OfficesSQLiteRepository>();
            kernel.Bind<IPlayersRepository>().To<PlayersSQLiteRepository>();
            kernel.Bind<IPlayersResource>().To<PlayersResource>();
            kernel.Bind<IRepublicsRepository>().To<RepublicsSQLiteRepository>();
            kernel.Bind<ISQLiteHelper>().To<SQLiteHelper>();
        }
    }
}
