using GalacticNetwork.Admin.Api.Configuration.Constants;
using GalacticNetwork.Admin.Api.ExceptionHandling;
using GalacticNetwork.Admin.Api.GraphQL;
using GraphQL;
using GraphQL.Types;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ControllerExceptionFilterAttribute))]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = AuthorizationConsts.AdministrationPolicy)]
    public class GraphQLController : ControllerBase
    {
        private readonly IDocumentExecuter m_documentExecuter;
        private readonly ISchema m_schema;

        public GraphQLController(ISchema schema, IDocumentExecuter documentExecuter)
        {
            m_schema = schema;
            m_documentExecuter = documentExecuter;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string query, [FromHeader] string token)
        {
            if (string.IsNullOrWhiteSpace(query)) { throw new ArgumentNullException(nameof(query)); }

            var graphQLQuery = new GraphQLQuery(query);
            return await PreformQuery(graphQLQuery, token);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query, [FromHeader] string token)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }
            return await PreformQuery(query, token);
        }

        /// Perform the query against the schema
        private async Task<IActionResult> PreformQuery(GraphQLQuery query, string token)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }
            var inputs = query.Variables.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                UserContext = new { token },
                Schema = m_schema,
                Query = query.Query,
                Inputs = inputs,
                // Uncomment the next line to show exceptions for debugging
                // ExposeExceptions = true,
            };

            var result = await m_documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
