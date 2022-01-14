using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace ReactMovieApi.Utils
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var propName = bindingContext.ModelName;
            var propValue = bindingContext.ValueProvider.GetValue(propName);

            if(propValue == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }
            else
            {
                try
                {
                    var deserializedValue = JsonConvert.DeserializeObject<T>(propValue.FirstValue);
                    bindingContext.Result = ModelBindingResult.Success(deserializedValue);
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.TryAddModelError(propName, "--- The given value is not of the correct type! --- " + e.Message);
                }
                return Task.CompletedTask;
            }
        }
    }
}
