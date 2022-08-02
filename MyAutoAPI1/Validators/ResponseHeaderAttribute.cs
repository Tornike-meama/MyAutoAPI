using Microsoft.AspNetCore.Mvc.Filters;
using MyAutoAPI1.Controllers.GetBody.Currency;

namespace MyAutoAPI1.Validators
{
    public class ResponseHeaderAttribute : ActionFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public ResponseHeaderAttribute(string name, string value) =>
            (_name, _value) = (name, value);

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var objResult = (AddCurrencyModel)context.Result;


            base.OnResultExecuting(context);
        }
    }
}
