namespace _12.Models;

public enum TimelineCategory
{
    Certification,
    Technology,
    Career,
    Project,
    Infrastructure,
}

public record TimelineEvent(string Year, TimelineCategory Category, IReadOnlyList<string> Events)
{
    /// <summary>Tailwind background color class for the timeline marker dot.</summary>
    public string CategoryColorClass => Category switch
    {
        TimelineCategory.Certification => "bg-amber-500",
        TimelineCategory.Technology => "bg-blue-500",
        TimelineCategory.Career => "bg-green-500",
        TimelineCategory.Project => "bg-purple-500",
        TimelineCategory.Infrastructure => "bg-gray-500",
        _ => "bg-blue-500",
    };
}
