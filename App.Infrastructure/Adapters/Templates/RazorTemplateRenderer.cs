using App.Interfaces.Ports.Emails;
using RazorLight;

namespace App.Infrastructure.Adapters.Templates;

public class RazorTemplateRenderer : ITemplateRenderer
{
    private readonly IRazorLightEngine _engine;

    public RazorTemplateRenderer()
    {
        _engine = new RazorLightEngineBuilder()
            .UseFileSystemProject(Path.Combine(AppContext.BaseDirectory, "Templates"))
            .UseMemoryCachingProvider()
            .Build();
    }

    public async Task<string> RenderAsync<TModel>(string templateName, TModel model)
    {
        return await _engine.CompileRenderAsync(templateName, model);
    }
}
