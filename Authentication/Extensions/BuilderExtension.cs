using Authentication.Logic;

namespace Authentication.Extensions;

public static class BuilderExtension
{
    public static void AddLogics(this WebApplicationBuilder builder)
    {
        var logics = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(t => t.IsClass && t.Namespace == typeof(ShareLogic).Namespace 
                                        && t.Name != "ShareLogic");
        foreach (var logic in logics)
        {
            builder.Services.AddScoped(logic);
        } 
    }
}