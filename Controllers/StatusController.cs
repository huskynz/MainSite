using Microsoft.AspNetCore.Mvc;

namespace _12.Controllers;

[Route("api/status")]
public class StatusController : Controller
{
    private const string Banner = @"
 _   _           _          _   _ ______
| | | |         | |        | \ | |____  |
| |_| |_   _ ___| | ___   _|  \| |   / /
|  _  | | | / __| |/ / | | | . ` |  / /
| | | | |_| \__ \   <| |_| | |\  | / /
\_| |_/\__,_|___/_|\_\\__, \_| \_//_/
                       __/ |
                      |___/
";

    private const string Message = "This HuskyNZ Site is responding to your request!";

    [HttpGet]
    public ContentResult Get()
    {
        return Content($"{Banner}\n{Message}\n", "text/plain");
    }
}
