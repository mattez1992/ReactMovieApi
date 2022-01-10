using Microsoft.AspNetCore.Mvc.Filters;

namespace ReactMovieApi.Filters
{
    public class CustomActionFilter
    {
        private readonly ILogger<CustomActionFilter> _logger;

        public CustomActionFilter(ILogger<CustomActionFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogWarning("OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogWarning("OnActionExecuting");
        }
    }
}
