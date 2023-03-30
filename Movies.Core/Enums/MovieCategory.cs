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