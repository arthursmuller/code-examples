using Notifications.Domain.Exceptions;
using RichEnumeration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notifications.Domain.AggregatesModel.NotificationAggregate
{
    public class NotificationChannel : Enumeration
    {

        public static NotificationChannel Platform = new NotificationChannel(1, nameof(Platform).ToLowerInvariant());
        public static NotificationChannel Push = new NotificationChannel(2, nameof(Push).ToLowerInvariant());
        public static NotificationChannel Email = new NotificationChannel(3, nameof(Email).ToLowerInvariant());
        public static NotificationChannel Sms = new NotificationChannel(4, nameof(Sms).ToLowerInvariant());

        public NotificationChannel(int id, string name): base(id, name) { }

        public static IEnumerable<NotificationChannel> List() =>
               new[] {
                    Platform,
                    Push,
                    Email,
                    Sms
               };
        public static NotificationChannel FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
                throw new NotificationException($"Possible values for NotificationChannel: {String.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static NotificationChannel From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
                throw new NotificationException($"Possible values for NotificationChannel: {String.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
