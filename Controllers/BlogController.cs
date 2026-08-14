using _12.Services;
using Microsoft.AspNetCore.Mvc;

namespace _12.Controllers;

public class BlogController : Controller
{
    private readonly BlogService _blogService;

    public BlogController(BlogService blogService)
    {
        _blogService = blogService;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Blog";
        return View(_blogService.GetAllPosts());
    }

    [Route("Blog/{slug}")]
    public IActionResult Post(string slug)
    {
        var post = _blogService.GetPostBySlug(slug);
        if (post is null)
        {
            return NotFound();
        }

        ViewData["Title"] = post.Title;
        return View(post);
    }
}
