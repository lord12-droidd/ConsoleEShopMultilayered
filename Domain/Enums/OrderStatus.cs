using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum OrderStatus
    {
        New,
        AdminDeny,
        PayReceived,
        Sent,
        Completed,
        Received,
        UserDeny,
    }
}
