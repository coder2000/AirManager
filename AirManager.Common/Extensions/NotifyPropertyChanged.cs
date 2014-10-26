// Copyright 2014 Dieter Lunn All Rights Reserved

using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace AirManager.Infrastructure.Extensions
{
    public static class NotifyPropertyChanged
    {
        public static NotifyExpectation<T> ShouldNotifyOn<T, TProperty>(this T owner,
            Expression<Func<T, TProperty>> propertyExpression) where T: INotifyPropertyChanged
        {
            return CreateExpectation(owner, propertyExpression, true);
        }

        public static NotifyExpectation<T> ShouldNotNotifyOn<T, TProperty>(this T owner,
            Expression<Func<T, TProperty>> propertyExpression) where T : INotifyPropertyChanged
        {
            return CreateExpectation(owner, propertyExpression, false);
        }

        private static NotifyExpectation<T> CreateExpectation<T, TProperty>(T owner, Expression<Func<T, TProperty>> propertyExpression, bool b) where T : INotifyPropertyChanged
        {
            string propertyName = ((MemberExpression) propertyExpression.Body).Member.Name;
            return new NotifyExpectation<T>(owner, propertyName, b);
        }
    }
}
