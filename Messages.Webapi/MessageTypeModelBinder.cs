using Messages.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Messages.Webapi
{
    internal class MessageTypeModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Message resultMessage = null;
            // в случае ошибки возвращаем исключение
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var senderId = bindingContext.HttpContext.Request.RouteValues["senderId"].ToString();
            var receiverId = bindingContext.HttpContext.Request.RouteValues["receiverId"].ToString();

            
              
            String valueFromBody;
            using (var streamReader = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                valueFromBody = await streamReader.ReadToEndAsync();
            }

            Assembly assem = typeof(IMessageType).Assembly;

            var types = assem.GetTypes()
              .Where(type => typeof(IMessageType).IsAssignableFrom(type))
              .Where(type => type != typeof(IMessageType));


            foreach (var type in types)
            {
                var modelInstance = JsonConvert.DeserializeObject(valueFromBody, type) ;

               var dict = modelInstance.GetType()
                    .GetProperties()
                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(modelInstance, null));

                bool allAttributesSet = dict.Values.All(val => val != null);

                if (allAttributesSet)

                {
                   var typeName= modelInstance.GetType().FullName+ "Processor";
                    var processor = Activator.CreateInstance(assem.GetType(typeName)) as IMessageProcessor;                  

                    resultMessage = processor.ToMessage(modelInstance as IMessageType);
                        break;
                }

            }

            if (resultMessage != null)
            {
                resultMessage.SenderId = Int32.Parse(senderId);
                resultMessage.ReceiverId = Int32.Parse(receiverId);

            }

            bindingContext.Result = ModelBindingResult.Success(resultMessage);
            
        }
    }
}