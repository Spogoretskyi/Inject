using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace injects
{
    public class Injector : IInjector, IDisposable
    {
        private readonly Dictionary<Type, Func<object>> _registeredTypes = new Dictionary<Type, Func<object>>();

        public void Register<TypeIn, TypeOut>() where TypeOut : TypeIn
        {
            _registeredTypes[typeof(TypeIn)] = () => Resolve<TypeOut>();

            Type type = typeof(TypeIn);

            if (_registeredTypes.ContainsKey(type))
                _registeredTypes.Remove(type);

            _registeredTypes.Add(typeof(TypeIn), expression);
        }


        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        public object Resolve(Type Test)
        {
            
            var ci = typeof(Test).G
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Register<T>(T type)
        {
            _registeredTypes[typeof(T)] = () => type;
        }

    }
}
