using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Logging;
using Rk.Messages.Interfaces;
using Rk.Messages.Interfaces.Services;
using Rk.Messages.Logic.ProductsNS.Dto;
using Rk.Messages.Logic.ProductsNS.Queries.GetProductQuery;
using RK.Messages.Shared;

namespace Rk.Messages.Logic.ProductsNS.PipelineBehaviour
{
    public class ProductViewStatisticBehavior: IPipelineBehavior<GetProductQuery, ProductResponse>
    {
        private readonly ILogger<ProductViewStatisticBehavior> _logger;
        private readonly IKafkaProducer<Null, ProductViewStatisticEvent> _producer;
        private readonly IUserService _user;

        public ProductViewStatisticBehavior(IKafkaProducer<Null, ProductViewStatisticEvent> producer, 
            ILogger<ProductViewStatisticBehavior> logger, IUserService user)
        {
            _producer = producer;
            _logger = logger;
            _user = user;
        }

        public async Task<ProductResponse> Handle(GetProductQuery request, RequestHandlerDelegate<ProductResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();
            if (response == null) return null;

            var msg = new ProductViewStatisticEvent
            {
                Created = DateTime.Now,
                Page = $"/api/v1/products/{response.Id}",
                Production = response.Name,
                ProductionId = response.Id,
                CategoryId = response.CatalogSectionId,
                Producer = response.Organization?.Name ?? "undefined",
                UserName = _user.UserName ?? "Anonymous"
            };
            _producer.Produce(nameof(ProductViewStatisticEvent), new Message<Null, ProductViewStatisticEvent>{Value = msg},
                DeliveryReportHandler);
            return response;
        }

        private void DeliveryReportHandler(DeliveryReport<Null, ProductViewStatisticEvent> deliveryReport)
        {
            if (deliveryReport.Status == PersistenceStatus.NotPersisted)
            {
                _logger.Log(LogLevel.Warning, $"Проблема отправки собщения: {deliveryReport.Message.Value}");
            }
        }
    }
}