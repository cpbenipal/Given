using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Given.Swagger      
{
    public class CustomDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            //sort operations alphabetically
            var paths = swaggerDoc.Paths.OrderBy(e => e.Key).ToList();
            var oaPaths = new OpenApiPaths();
            paths.ForEach(p => oaPaths.Add(p.Key, p.Value));
            swaggerDoc.Paths = oaPaths;
        }
    }
}
