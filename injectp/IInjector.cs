using System;

namespace injects
{
    interface IInjector
    {
        void Register<TypeIn, TypeOut>() where TypeOut : TypeIn;
        void RegisterSingleton<T>(T obj);
        void Register<T>(T type);
        T Resolve<T>();
        object Resolve(Type type);
    }
}
