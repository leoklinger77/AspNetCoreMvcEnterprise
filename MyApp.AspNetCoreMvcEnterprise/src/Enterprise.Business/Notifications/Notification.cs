using Enterprise.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enterprise.Business.Notifications
{
    public class Notifier
    {
        public string Message { get; }

        public Notifier(string message)
        {
            Message = message;
        }
    }

    public class Notification : INotification
    {
        private ICollection<Notifier> list;
        public Notification()
        {
            list = new List<Notifier>();
        }
        public List<Notifier> FindAll()
        {
            return (List<Notifier>)list;
        }

        public void Handle(string message)
        {
            list.Add(new Notifier(message));
        }

        public bool HasNotification()
        {
            return list.Any();
        }
    }
}
