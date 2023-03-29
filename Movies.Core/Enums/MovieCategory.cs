using System.ComponentModel;
using System.Reflection;

namespace Movies.Core.Enums;

public enum MovieCategory : short
{
    [Description("Science Fiction")]
    ScienceFiction=1,

    Comedy=2,

    Terror=3,
    
    Action=4,

    [Description("Car Racing")]
    CarRacing =5,

    Generic = 6
}

public static class Enumss
{
    public static string GetEnumDescription(this MovieCategory value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        if (attributes != null && attributes.Any())
        {
            return attributes.First().Description;
        }

        return value.ToString();
    }
}