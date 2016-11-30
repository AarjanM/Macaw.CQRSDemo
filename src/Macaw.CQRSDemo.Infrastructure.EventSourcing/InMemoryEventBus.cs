using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Macaw.CQRSDemo.Infrastructure.EventSourcing
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        public IEventStore EventStore { get; }

        private static readonly IDictionary<Type, Type> RegisteredSagas = new Dictionary<Type, Type>();
        private static readonly IList<Type> RegisteredHandlers = new List<Type>();

        public InMemoryEventBus(IEventStore eventStore, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            EventStore = eventStore;
        }

        public void RegisterSaga<T>() where T : Saga
        {
            var sagaType = typeof(T);
            if (sagaType.GetInterfaces().Count(i => i.Name.StartsWith(typeof(IStartWithMessage<>).Name)) != 1)
            {
                throw new InvalidOperationException("The specified saga must implement the IStartWithMessage<T> interface.");
            }
            var messageType = sagaType.
                GetInterfaces().First(i => i.Name.StartsWith(typeof(IStartWithMessage<>).Name)).
                GenericTypeArguments.
                First();
            RegisteredSagas.Add(messageType, sagaType);
        }

        public void Send<T>(T message) where T : Command
        {
            SendInternal(message);
        }

        public void RegisterHandler<T>()
        {
            RegisteredHandlers.Add(typeof(T));
        }

        void IEventBus.RaiseEvent<T>(T theEvent)
        {
            EventStore.Save(theEvent);
            SendInternal(theEvent);
        }

        private void SendInternal<T>(T message) where T : Message
        {
            LaunchSagasThatStartWithMessage(message);
            DeliverMessageToRunningSagas(message);
            DeliverMessageToRegisteredHandlers(message);

            // Saga and handlers are similar things. Handlers are  one-off event handlers
            // whereas saga may be persisted and survive sessions, wait for more messages and so forth.
            // Saga are mostly complex workflows; handlers are plain one-off event handlers.
        }

        private void LaunchSagasThatStartWithMessage<T>(T message) where T : Message
        {
            var messageType = message.GetType();
            var openInterface = typeof(IStartWithMessage<>);
            var closedInterface = openInterface.MakeGenericType(messageType);
            var sagasToLaunch = from s in RegisteredSagas.Values
                                where closedInterface.IsAssignableFrom(s)
                                select s;
            foreach (var s in sagasToLaunch)
            {
                dynamic sagaInstance = _serviceProvider.GetService(s);
                sagaInstance.Handle(message);
            }
        }

        private void DeliverMessageToRunningSagas<T>(T message) where T : Message
        {
            var messageType = message.GetType();
            var openInterface = typeof(IHandleMessage<>);
            var closedInterface = openInterface.MakeGenericType(messageType);
            var sagasToNotify = from s in RegisteredSagas.Values
                                where closedInterface.IsAssignableFrom(s)
                                select s;
            foreach (var s in sagasToNotify)
            {
                dynamic sagaInstance = _serviceProvider.GetService(s);
                sagaInstance.Handle(message);
            }
        }

        private void DeliverMessageToRegisteredHandlers<T>(T message) where T : Message
        {
            var messageType = message.GetType();
            var openInterface = typeof(IHandleMessage<>);
            var closedInterface = openInterface.MakeGenericType(messageType);
            var handlersToNotify = from h in RegisteredHandlers
                                   where closedInterface.IsAssignableFrom(h)
                                   select h;
            foreach (var h in handlersToNotify)
            {
                dynamic handlerInstance = _serviceProvider.GetService(h);
                handlerInstance.Handle(message);
            }
        }
    }
}