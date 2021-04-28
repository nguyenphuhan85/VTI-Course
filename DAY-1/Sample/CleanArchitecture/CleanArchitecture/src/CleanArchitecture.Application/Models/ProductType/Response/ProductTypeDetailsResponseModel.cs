using CleanArchitecture.Domain.Entites;
using CleanArchitecture.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Models.ProductType.Response
{
    public class ProductTypeDetailsResponseModel : AuditableEntity
    {
        public Guid ProductTypeID { get; set; }
        public string ProductTypeKey { get; set; }
        public string ProductTypeName { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public string RecordStatusName { get; set; }
    }
}
