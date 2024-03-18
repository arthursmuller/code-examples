using Notifications.Domain.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Notifications.Domain.AggregatesModel.NotificationAggregate
{
    public abstract class NotificationBase : BaseEntity, IAggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public bool Viewed { get; set; }
        public IReadOnlyCollection<NotificationChannelRecord> Channels => _channels;
        private List<NotificationChannelRecord> _channels;
        public IReadOnlyCollection<Recipient> Recipients => _recipients;
        private List<Recipient> _recipients;

        public NotificationBase()
        {
            Viewed = false;
        }

        public void AddRecipient(string name, string email, string cellphone)
        {
            _recipients = _recipients ?? new List<Recipient>();
            _recipients.Add(new Recipient(name, email, cellphone));
        }

        public void AddChannel(int _channelId)
        {
            _channels = _channels ?? new List<NotificationChannelRecord>();
            _channels.Add(new NotificationChannelRecord(_channelId));
        }

        public IEnumerable<NotificationChannel> GetChannels() => _channels.Select(e => e.GetChannel());

    }
}
