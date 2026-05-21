using System;

public static class GameTime
{
    public static int TimeZoneOffsetHours = 7;

    public static DateTime Now
    {
        get
        {
            return DateTime.UtcNow.AddHours(TimeZoneOffsetHours);
        }
    }
}
