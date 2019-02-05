using System;
using System.Collections.Generic;
using System.Linq;

namespace injects
{
        public class Injector : IInjector, IDisposable
        {
            private readonly Dictionary<Type, Func<IInjector, object>> _registeredTypes = new Dictionary<Type, Func<IInjector, object>>();
            public void Register<TypeIn, TypeOut>(Func<IInjector, object> fn) where TypeOut : TypeIn
            {
            _registeredTypes[typeof(TypeIn)] = fn;
            }
            public T Resolve<T>() where T: class
            {
                Func<IInjector, object> fn = _registeredTypes.TryGetValue(typeof(T), out fn) ? fn : null;
                return fn?.Invoke(this) as T;
            }
            public void Dispose()
            {

            }
        }
}
