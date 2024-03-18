using BrDateTimeUtils;
using System;

namespace Notifications.Domain.AggregatesModel.NotificationAggregate
{
    public class NotificationChannelRecord
    {
        public NotificationChannel Channel { get; private set; }
        private int _channelId;
        public DateTime CreatedDate => _createdDate;
        private DateTime _createdDate;
        public NotificationChannelRecord() { }
        public NotificationChannelRecord(int channelId) => (_channelId, _createdDate) =(channelId, DateTime.Now.Brasilia());

        public NotificationChannel GetChannel() => NotificationChannel.From(_channelId);
    }
}
