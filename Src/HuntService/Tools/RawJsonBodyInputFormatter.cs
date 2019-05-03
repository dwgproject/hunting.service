using Microsoft.AspNetCore.Mvc.Formatters;
using System.Threading.Tasks;
using System;
using System.IO;

namespace GravityZero.HuntingSupport.Service.Tools
{
    public class RawJsonBodyInputFormatter : InputFormatter{
        public RawJsonBodyInputFormatter()
        {   
            this.SupportedMediaTypes.Add("application/json");
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            using (var reader = new StreamReader(request.Body))
            {
                var content = await reader.ReadToEndAsync();
                return await InputFormatterResult.SuccessAsync(content);
            }
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(string);
        }
    }
}
//GravityZero.
//GravityZero.Hunting.Service.Commands