using Autofac;

namespace PwProject.Core.Autofac
{
    public static class DependencyResolver
    {
        private static IContainer _container;

        public static void SetResolver(IContainer container)
        {
            _container = container;
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
