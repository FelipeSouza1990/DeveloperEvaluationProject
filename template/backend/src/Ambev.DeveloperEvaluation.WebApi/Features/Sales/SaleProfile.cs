using Ambev.DeveloperEvaluation.Domain.Sales.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    public class SaleProfile: Profile
    {
        public SaleProfile() 
        { 
            CreateMap<SaleProfile, SaleDto>();  
            CreateMap<SaleItem, SaleItemDto>(); 
        }
    }
}
