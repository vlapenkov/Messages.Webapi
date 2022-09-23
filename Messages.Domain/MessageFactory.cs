using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Messages.Domain
    {
    public interface IMessageType
        {
        }

    public class MessageType1 : IMessageType
        {
        public string Header { get; set; }
        public string Content { get; set; }

        }

    public class MessageType2 : IMessageType
        {
        public string Caption { get; set; }
        public string Message { get; set; }

        }

    public interface IMessageProcessor
        {
        IMessageType Process ( Message message );
        Message ToMessage ( IMessageType baseMessageType );
        }


    public class MessageType1Processor : IMessageProcessor
        {

        public IMessageType Process ( Message message )
            {
            return new MessageType1 { Header = message.Name, Content = message.Description };
            }

        public Message ToMessage ( IMessageType baseMessageType )
            {
            var mt = (baseMessageType as MessageType1);
            return new Message { Name = mt.Header, Description = mt.Content };
            }
        }


    public class MessageType2Processor : IMessageProcessor
        {
        public IMessageType Process ( Message message )
            {
            return new MessageType2 { Caption = message.Name, Message = message.Description };
            }
        public Message ToMessage ( IMessageType baseMessageType )
            {
            var mt = (baseMessageType as MessageType2);
            return new Message { Name = mt.Caption, Description = mt.Message };
            }
        }


    public class MessageTypeFactory
        {
        private static IDictionary<string, Type> _messageTypes = LoadTypes ( );

        private static IDictionary<string, Type> LoadTypes ( )
            {
            Dictionary<string, Type> messageTypes = new Dictionary<string, Type> ( );
            var typeInThisAssembly = Assembly.GetExecutingAssembly ( ).GetTypes ( )
               .Where ( type => typeof ( IMessageProcessor ).IsAssignableFrom ( type ) );
            foreach (var type in typeInThisAssembly)
                messageTypes.Add ( type.Name, type );

            return messageTypes;
            }


        public IMessageProcessor GetProcessorByName ( string name )
            {
            var type = _messageTypes[name];

            return Activator.CreateInstance ( type ) as IMessageProcessor;
            }


        }
    }


