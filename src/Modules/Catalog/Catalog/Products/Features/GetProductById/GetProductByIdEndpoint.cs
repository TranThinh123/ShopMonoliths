﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carter;
using Catalog.Products.Dtos;
using Catalog.Products.Features.GetProducts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Catalog.Products.Features.GetProductById
{
    //public record GetProductByIdRequest(Guid ProductId);
    public record GetProductByIdResponse(ProductDto Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/product/{id}", async (Guid id, ISender sender)=>
           {
               var result = await sender.Send(new GetProductByIdQuery(id));
               var response = result.Adapt<GetProductByIdResponse>();
               return Results.Ok(response);
           })
                .WithName("GetProductById")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product By Id")
                .WithDescription("Get Product By Id");

        }
    }
}
