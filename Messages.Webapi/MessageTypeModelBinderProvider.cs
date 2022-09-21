using Messages.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Messages.Webapi
{
    internal class MessageTypeModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinder binder = new MessageTypeModelBinder();

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(Message) ? binder : null;
        }
    }
}