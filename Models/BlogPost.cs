namespace _12.Models;

public record BlogPost(string Slug, string Title, string Summary, DateTime Date, IReadOnlyList<string> Tags, bool Draft, string HtmlContent);
