using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Prn.Models;

namespace Project_Prn.dal
{
    public class NotificationDAO
    {
        private Prngroup4Context dbc;
        public NotificationDAO()
        {
            dbc = new Prngroup4Context();
        }
        public bool IsNotified(int userId, int regisId)
        {
            return dbc.Notifications
                .Any(n => n.UserId == userId && n.RegistrationId == regisId);
        }

        public void MarkAsNotified(int userId, int regisId)
        {
            var notification = new Notification
            {
                UserId = userId,
                RegistrationId = regisId
            };
            dbc.Notifications.Add(notification);
            dbc.SaveChanges();
        }

    }
}
