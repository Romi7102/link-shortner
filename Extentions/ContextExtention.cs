using LinkShortner.Models;
using System.Runtime.CompilerServices;


public static class ContextExtention {
    public static string GetDomain(this HttpContext context) {
        var req = context.Request;
        return $"{req.Scheme}://{req.Host}";
    }
    
    public static string Href(this Link url , HttpContext context) {
        return $"{context.GetDomain()}/s/{url.Code}";
    }
}

