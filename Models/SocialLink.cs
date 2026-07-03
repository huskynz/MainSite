namespace _12.Models;

public record SocialLink(string Name, string Text, string Href)
{
    public static readonly IReadOnlyList<SocialLink> All = new[]
    {
        new SocialLink("Email", "peter@husky.nz", "mailto:peter@husky.nz"),
        new SocialLink("GitHub (Main)", "HuskyNZ", "https://github.com/HuskyNZ"),
        new SocialLink("GitHub (Personal)", "Husky-Devel", "https://github.com/husky-devel"),
        new SocialLink("Twitch", "huskynzofficial", "https://hnz.li/twitch"),
        new SocialLink("YouTube", "@huskynz", "https://hnz.li/youtube"),
        new SocialLink("Discord", "huskynzofficial", "https://discord.com"),
        new SocialLink("Download CV", "Download", "https://hnz.li/cv"),
    };
}
