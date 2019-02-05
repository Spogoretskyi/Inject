using System;

namespace injects
{
    public interface IInjector
    {
        void Register<TypeIn, TypeOut>(Func<IInjector, object> fn) where TypeOut : TypeIn;
        T Resolve<T>() where T : class;
    }
}
