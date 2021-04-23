using System;
using MediatR;

namespace InstantPOS.Application.CQRS.Product.Command
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public Guid ProductID { get; set; }
        public string ProductKey { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUri { get; set; }
        public Guid ProductTypeID { get; set; }
        public int RecordStatus { get; set; }
   
    }
}
