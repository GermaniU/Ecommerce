using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [AllowAnonymous]
        [HttpGet("list", Name = "GetProductList")]
        [ProducesResponseType(typeof(IReadOnlyList<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductList()
        {

            var query = new GetProductListQuery();

            var productos = await _mediator.Send(query);

            return Ok(productos);

        }
    }
}