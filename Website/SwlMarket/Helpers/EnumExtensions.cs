using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SwlMarket.Helpers
{
    public static class EnumExtensions
    {
        public static string DisplayName(this Enum enumValue, string defaultName = "")
        {
            if (enumValue == null) return defaultName;
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>(false)?.Name
                  ?? enumValue.ToString();
        }
    }
}
