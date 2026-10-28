using System.Globalization;
using Markdig;
using _12.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace _12.Services;

public class BlogService
{
    private readonly string _contentRoot;
    private readonly MarkdownPipeline _pipeline;
    private readonly IDeserializer _yamlDeserializer;

    public BlogService(IWebHostEnvironment env)
    {
        _contentRoot = Path.Combine(env.ContentRootPath, "Content", "Blog");
        _pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        _yamlDeserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();
    }

    public IReadOnlyList<BlogPost> GetAllPosts()
    {
        if (!Directory.Exists(_contentRoot))
        {
            return Array.Empty<BlogPost>();
        }

        return Directory.EnumerateFiles(_contentRoot, "*.md")
            .Select(LoadPost)
            .Where(post => !post.Draft)
            .OrderByDescending(post => post.Date)
            .ToList();
    }

    public BlogPost? GetPostBySlug(string slug)
    {
        var path = Path.Combine(_contentRoot, $"{slug}.md");
        if (!File.Exists(path))
        {
            return null;
        }

        var post = LoadPost(path);
        return post.Draft ? null : post;
    }

    private BlogPost LoadPost(string path)
    {
        var slug = Path.GetFileNameWithoutExtension(path);
        var raw = File.ReadAllText(path);
        var (frontMatter, body) = SplitFrontMatter(raw);

        var meta = _yamlDeserializer.Deserialize<FrontMatter>(frontMatter) ?? new FrontMatter();
        var html = Markdown.ToHtml(body, _pipeline);
        var date = DateTime.Parse(meta.Date, CultureInfo.InvariantCulture, DateTimeStyles.None);

        return new BlogPost(slug, meta.Title, meta.Summary, date, meta.Tags, meta.Draft, html);
    }

    private static (string FrontMatter, string Body) SplitFrontMatter(string raw)
    {
        var normalized = raw.Replace("\r\n", "\n").TrimStart('\n');
        const string delimiter = "---";

        if (!normalized.StartsWith(delimiter))
        {
            return (string.Empty, normalized);
        }

        var end = normalized.IndexOf("\n---", delimiter.Length, StringComparison.Ordinal);
        if (end == -1)
        {
            return (string.Empty, normalized);
        }

        var frontMatter = normalized[delimiter.Length..end];
        var body = normalized[(end + 4)..].TrimStart('\n');

        return (frontMatter, body);
    }

    private class FrontMatter
    {
        public string Title { get; set; } = "";
        public string Summary { get; set; } = "";
        public string Date { get; set; } = "";
        public bool Draft { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}
