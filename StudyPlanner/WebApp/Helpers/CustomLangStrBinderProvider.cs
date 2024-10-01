using Base.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Helpers;

public class CustomLangStrBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(LangStr))
        {
            var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
            return new CustomLangStrBinder(loggerFactory);
        }

        return null;
    }
}