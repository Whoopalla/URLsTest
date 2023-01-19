using Autofac;
using URLsTest.Client.ViewModels;
using URLsTest.Services;

namespace URLsTest.Client {
    public static class Container {
        public static ContainerBuilder RegisterViewModels(this ContainerBuilder containerBuilder) {
            containerBuilder.RegisterType<URLAnchorsViewModel>().As<URLAnchorsViewModel>().SingleInstance();
            return containerBuilder;
        }

        public static ContainerBuilder RegisterSerices(this ContainerBuilder containerBuilder) {
            containerBuilder.RegisterType<HtmlTagCounter>().As<IHtmlTagCounter>().SingleInstance();
            return containerBuilder;
        }

        public static ContainerBuilder RegisterViews(this ContainerBuilder containerBuilder) {
            containerBuilder.RegisterType<MainWindow>().As<MainWindow>().SingleInstance();
            return containerBuilder;
        }
    }
}
