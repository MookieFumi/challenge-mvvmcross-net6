using System;
using Challenge.Services;

namespace Challenge.Core.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
