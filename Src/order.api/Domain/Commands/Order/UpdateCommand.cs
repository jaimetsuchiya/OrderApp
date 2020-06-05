using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using order.api.Domain.Common;
using order.api.Domain.Entities;
using order.api.Infrastructure;
using order.api.Repositories;

namespace order.api.Domain.Commands
{
    public class UpdateOrderCommand : IRequest<Response<Order>>
    {
        public UpdateOrderCommand(Guid orderId, OrderStatusEnum orderStatus)
        {
            this.OrderId = orderId;
            this.OrderStatus = orderStatus;
        }

        public Guid OrderId { get; set; }
        public OrderStatusEnum OrderStatus { get; private set; }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Response<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUIContext _appContext;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IAppUIContext appUIContext)
        {
            _appContext = appUIContext;
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        public async Task<Response<Order>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = await _orderRepository.FindByIdAsync(request.OrderId);
            Response<Order> response = await OrderIsValid(request, orderEntity);

            if (response == null)
            {
                orderEntity.Status = request.OrderStatus;
                orderEntity.LastModified = DateTime.Now;
                orderEntity.LastModifiedBy = _appContext.UserName;

                _orderRepository.Update(orderEntity);

                await _unitOfWork.CompleteAsync();

                response = new Response<Order>(orderEntity);
            }

            return response;
        }

        private async Task<Response<Order>> OrderIsValid(UpdateOrderCommand request, Order orderEntity)
        {
            if (string.IsNullOrEmpty(_appContext.UserName))
                return new Response<Order>("Responsável pela operação não informado!");

            if( orderEntity == null)
                return new Response<Order>("Pedido não encontrado!");

            if (orderEntity.Status == request.OrderStatus)
                return new Response<Order>("Nenhuma alteração informada!");

            if (orderEntity.Status == OrderStatusEnum.Canceled || orderEntity.Status == OrderStatusEnum.Finished)
                return new Response<Order>("Não é possível alterar o status do pedido!");

            return null;
        }
    }
}
