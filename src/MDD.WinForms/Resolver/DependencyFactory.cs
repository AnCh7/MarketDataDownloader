#region Usings

using MDD.DataFeed.Fidelity;
using MDD.DataFeed.Fidelity.DataParser;
using MDD.DataFeed.Fidelity.DataProvider;
using MDD.DataFeed.Fidelity.DataSaver;
using MDD.DataFeed.IQFeed;
using MDD.DataFeed.IQFeed.DataParser;
using MDD.DataFeed.IQFeed.DataProvider;
using MDD.DataFeed.IQFeed.DataSaver;
using MDD.Library.Abstraction;
using MDD.Library.Abstraction.Client;
using MDD.Library.Abstraction.Manager;
using MDD.Library.Abstraction.Parser;
using MDD.Library.Abstraction.Saver;
using MDD.Library.Helpers;
using MDD.Library.Logging;

using Microsoft.Practices.Unity;

#endregion

namespace MDD.WinForms.Resolver
{
	public static class DependencyFactory
	{
		static DependencyFactory()
		{
			Container = new UnityContainer();

			Container.RegisterType<IPathHelper, PathHelper>(new ContainerControlledLifetimeManager());
			Container.RegisterType<IFileContentHelper, FileContentHelper>(new ContainerControlledLifetimeManager());

			Container.RegisterType<IMyLogger, MyLogger>(new ContainerControlledLifetimeManager(),
														new InjectionConstructor(new TargetRichTextBox().Initialize(), "MainForm"));

			Container.RegisterType<IDataFeedClient, IQFeedSocketClient>("IQFeed", new ContainerControlledLifetimeManager());
			Container.RegisterType<IDataFeedClient, FidelityClient>("Fidelity", new ContainerControlledLifetimeManager());

			Container.RegisterType<IMarketDataParser, IQFeedDataParser>("IQFeed", new ContainerControlledLifetimeManager());
			Container.RegisterType<IMarketDataParser, FidelityDataParser>("Fidelity", new ContainerControlledLifetimeManager());

			Container.RegisterType<IMarketDataSaver, IQFeedFileSaver>("IQFeed", new ContainerControlledLifetimeManager());
			Container.RegisterType<IMarketDataSaver, FidelityFileSaver>("Fidelity", new ContainerControlledLifetimeManager());

			Container.RegisterType<IRequestBuilder, IQFeedRequestBuilder>("IQFeed", new ContainerControlledLifetimeManager());
			Container.RegisterType<IRequestBuilder, FidelityRequestBuilder>("Fidelity", new ContainerControlledLifetimeManager());

			Container.RegisterType<IDataFeedManager, IQFeedManager>("IQFeed", new ContainerControlledLifetimeManager());
			Container.RegisterType<IDataFeedManager, FidelityManager>("Fidelity", new ContainerControlledLifetimeManager());
		}

		public static IUnityContainer Container { get; private set; }
	}
}
