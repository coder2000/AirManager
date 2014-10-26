// Copyright 2014 Dieter Lunn All Rights Reserved

using System;
using System.ComponentModel;
using Xunit;

namespace AirManager.Infrastructure.Extensions
{
    public class NotifyExpectation<T> where T : INotifyPropertyChanged
    {
        private readonly T _owner;
        private readonly string _propertyName;
        private readonly bool _eventExpected;

        public NotifyExpectation(T owner, string propertyName, bool eventExpected)
        {
            _owner = owner;
            _propertyName = propertyName;
            _eventExpected = eventExpected;
        }

        public void When(Action<T> action)
        {
            bool eventWasRaised = false;
            _owner.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == _propertyName)
                {
                    eventWasRaised = true;
                }
            };

            action(_owner);

            Assert.Equal(_eventExpected, eventWasRaised);
        }
    }
}
