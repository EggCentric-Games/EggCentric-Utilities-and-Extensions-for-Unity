using System;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.DI
{
    public class NaiveContainer : IContainer
    {
        private readonly Dictionary<Type, object> _bindings;

        public NaiveContainer() => _bindings = new Dictionary<Type, object>();

        public void Bind(object service)
        {
            if (_bindings.TryGetValue(service.GetType(), out var binding))
            {
                Debug.LogWarning($"Service {service.GetType()} was binded already");
                return;
            }

            _bindings.Add(service.GetType(), service);
        }

        public bool Resolve<TRequested>(out TRequested service)
        {
            service = default;

            if (!_bindings.TryGetValue(typeof(TRequested), out var binding))
            {
                Debug.LogWarning($"There is no registered service of type {typeof(TRequested)}");
                return false;
            }

            service = (TRequested)binding;
            return true;
        }

        public bool ResolveAll<TRequested>(out TRequested[] services)
        {
            services = Array.Empty<TRequested>();

            if (!_bindings.TryGetValue(typeof(TRequested), out var binding))
            {
                Debug.LogWarning($"There is no registered service of type {typeof(TRequested)}");
                return false;
            }

            services = new TRequested[] { (TRequested)binding };
            return true;
        }
    }

    public class NaiveContainer<TItem> : IContainer<TItem>
    {
        private readonly Dictionary<Type, TItem> _bindings;

        public NaiveContainer() => _bindings = new Dictionary<Type, TItem>();

        public void Bind(TItem service)
        {
            if (_bindings.TryGetValue(service.GetType(), out var binding))
            {
                Debug.LogWarning($"Service {service.GetType()} was binded already");
                return;
            }

            _bindings.Add(service.GetType(), service);
        }

        public bool Resolve<TRequested>(out TRequested service) where TRequested : TItem
        {
            service = default;

            if (!_bindings.TryGetValue(typeof(TRequested), out var binding))
            {
                Debug.LogWarning($"There is no registered service of type {typeof(TRequested)}");
                return false;
            }

            service = (TRequested)binding;
            return true;
        }

        public bool ResolveAll<TRequested>(out TRequested[] services) where TRequested : TItem
        {
            services = Array.Empty<TRequested>();

            if (!_bindings.TryGetValue(typeof(TRequested), out var binding))
            {
                Debug.LogWarning($"There is no registered service of type {typeof(TRequested)}");
                return false;
            }

            services = new TRequested[] { (TRequested)binding };
            return true;
        }
    }
}