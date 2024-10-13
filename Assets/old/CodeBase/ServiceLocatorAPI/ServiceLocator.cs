using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBase.ServiceLocatorAPI
{
    public static class ServiceLocator 
    {
        private static readonly Dictionary<(Type, string), ServiceRegistration> _registrationMap = new();
        
        public static ServiceRegistration Register<T>(T instance, string tag = null)
        {
            var key = (typeof(T), tag);
            
            if (_registrationMap.ContainsKey(key))
                throw new Exception($"Service with type {key.Item1.FullName} and tag {key.Item2} has already registered");

            var serviceRegistration = new ServiceRegistration(instance);
            _registrationMap[key] = serviceRegistration;
            
            return serviceRegistration;
        }
        
        public static T Resolve<T>(string tag = null)
        {
            var key = (typeof(T), tag);

            if (_registrationMap.ContainsKey(key))
                return (T)_registrationMap[key].Instance;
            
            throw new Exception($"Couldn't find service for type {key.Item1.FullName} and type {key.Item2}");
        }

        public static void DisposeScene()
        {
            for (int i = 0; i < _registrationMap.Count; i++)
            {
                var item = _registrationMap.ElementAt(i);
                
                if (_registrationMap[item.Key].IsDisposable)
                    _registrationMap.Remove(item.Key);
            }
        }
    }
}