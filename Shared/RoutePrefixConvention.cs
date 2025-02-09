using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace damage_assessment_api.Shared
{
    public class RoutePrefixConvention : IApplicationModelConvention
    {
        private readonly string _routePrefix;

        public RoutePrefixConvention(string routePrefix)
        {
            _routePrefix = routePrefix;
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                foreach (var selector in controller.Selectors)
                {
                    if (selector.AttributeRouteModel != null)
                    {
                        // Prepend the global prefix to existing routes
                        selector.AttributeRouteModel = new AttributeRouteModel(
                            new RouteAttribute($"{_routePrefix}/{selector.AttributeRouteModel.Template}"));
                    }
                    else
                    {
                        // Apply the global prefix if no route is defined
                        selector.AttributeRouteModel = new AttributeRouteModel(
                            new RouteAttribute($"{_routePrefix}/[controller]"));
                    }
                }
            }
        }
    }
}