using System;
using System.Collections.Generic;
using L4.Controller;
using L4.Data;
using L4.Domain;
using L4.Service;
using L4.View;

namespace L4
{
    public class DependencyInjectionContainer
    {
        private readonly Dictionary<Type, Func<object>> _factories = new();

        public DependencyInjectionContainer()
        {
            Build();
        }

        private void Build()
        {
            _factories[typeof(IErrorHandler)] = () => new ConsoleErrorHandler();
            _factories[typeof(CustomWorldService)] = () =>
                new CustomWorldService(
                    Get<DataWorldRepository>(),
                    Get<RaportWHRRepository>(),
                    Get<CustomWorldRepository>()
                );
            _factories[typeof(DataWorldRepository)] = () => new DataWorldRepository(Get<Database>());
            _factories[typeof(RaportWHRRepository)] = () => new RaportWHRRepository(Get<Database>());
            _factories[typeof(CustomWorldRepository)] = () => new CustomWorldRepository(Get<Database>());
            _factories[typeof(MainController)] = () => new MainController(Get<CustomWorldService>());

            // Singletons
            var database = new Database(Get<IErrorHandler>());
            _factories[typeof(Database)] = () => database;
        }

        public T Get<T>()
        {
            var factory = _factories[typeof(T)];
            return (T) factory();
        }
    }
}