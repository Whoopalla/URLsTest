using Autofac;
using System;
using System.Windows;
using URLsTest.Client.Utils;

namespace URLsTest.Client {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        IContainer Container { get; set; }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            StartupUri = new Uri("Views/MainWindow.xaml", UriKind.Relative);
            var builder = new ContainerBuilder();
            builder.RegisterSerices()
                .RegisterViewModels()
                .RegisterViews();

            Container = builder.Build();
            DISource.Resolver = (type) => {
                return Container.Resolve(type);
            };
        }
    }
}
