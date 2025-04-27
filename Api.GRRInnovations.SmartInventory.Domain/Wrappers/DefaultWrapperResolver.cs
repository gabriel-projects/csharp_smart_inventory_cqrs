using Api.GRRInnovations.SmartInventory.Domain.Entities;
using Api.GRRInnovations.SmartInventory.Domain.Wrappers.Out;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.GRRInnovations.SmartInventory.Domain.Wrappers
{
    public class DefaultWrapperResolver : DefaultContractResolver
    {
        public override JsonContract ResolveContract(Type type)
        {
            var result = base.CreateContract(type);

            if (type == typeof(WrapperOutProduct))
            {
                result.DefaultCreator = () => new WrapperOutProduct(new ProductModel());
            }

            return result;
        }

        public static async Task<TResult> Deserialize<TResult>(Stream stream) where TResult : new()
        {
            var result = default(TResult);

            using (var sr = new StreamReader(stream))
            {
                var json = await sr.ReadToEndAsync().ConfigureAwait(false);

                result = Deserialize<TResult>(json);
            }

            return result;
        }

        public static TResult Deserialize<TResult>(string json) where TResult : new()
        {
            var result = JsonConvert.DeserializeObject<TResult>(json, new JsonSerializerSettings
            {
                ContractResolver = new DefaultWrapperResolver(),
                TypeNameHandling = TypeNameHandling.Auto
            });

            return result;
        }
    }
}
