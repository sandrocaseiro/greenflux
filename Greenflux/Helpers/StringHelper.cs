using System;

namespace Greenflux.Helpers
{
    public static class StringHelper
    {
        public static string DefaultIfNull(this string value, string defaultValue) =>
            !String.IsNullOrEmpty(value) ? value : defaultValue;
    }
}