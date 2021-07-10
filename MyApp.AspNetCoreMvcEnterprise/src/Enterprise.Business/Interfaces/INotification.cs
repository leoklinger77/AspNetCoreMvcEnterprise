using Enterprise.Business.Notifications;
using System.Collections.Generic;

namespace Enterprise.Business.Interfaces
{
    public interface INotification
    {
        bool HasNotification();
        List<Notifier> FindAll();
        void Handle(string message);
    }
}
