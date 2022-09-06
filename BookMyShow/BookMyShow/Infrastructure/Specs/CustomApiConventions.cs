using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.Infrastructure.Specs
{
    public static class CustomApiConventions
    {
        #nullable disable
        #region DELETE
        /// <summary>
        /// Delete convention.
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Delete(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
           [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id)
        { }
        #endregion
    }
}
