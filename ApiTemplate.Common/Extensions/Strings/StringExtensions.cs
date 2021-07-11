using System;
using System.Collections.Generic;
using System.Text;

namespace RabitMQTask.Common.Extensions.Strings
{
    public static class StringExtensions
    {
        public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
        }
    }
}
