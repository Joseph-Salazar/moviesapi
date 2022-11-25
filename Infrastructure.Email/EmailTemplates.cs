using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using RazorEngine;
using RazorEngine.Templating;

namespace Infrastructure.Email;

public static class EmailTemplates
{
    private static readonly string PathTemplates = "EmailTemplates";
    private static IWebHostEnvironment? _webHostEnvironment;

    public static void Initialize(IWebHostEnvironment environment)
    {
        _webHostEnvironment = environment;
    }

    public static string GetRecoverPasswordTemplate(dynamic model)
    {
        string template = ReadPhysicalFile(@$"{PathTemplates}\RecoverPassword.cshtml");
        return RunCompile(template, model);
    }

    private static string RunCompile(string stringTemplate, object model)
    {
        return Engine.Razor.RunCompile(stringTemplate, Guid.NewGuid().ToString(), null, model);
    }

    private static string ReadPhysicalFile(string path)
    {
        if (_webHostEnvironment == null)
            throw new InvalidOperationException($"{nameof(EmailTemplates)} is not initialized");

        IFileInfo fileInfo = _webHostEnvironment.ContentRootFileProvider.GetFileInfo(path);

        if (!fileInfo.Exists)
            throw new FileNotFoundException($"Template file located at \"{path}\" was not found");

        return File.ReadAllText(fileInfo.PhysicalPath);
    }
}