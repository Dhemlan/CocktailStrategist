using CocktailStrategist.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailStrategist.Tests.Integration
{
    internal class TestUtils
    {
        internal static async Task<int?> CountGetRequestContents(string url, HttpClient client)
        {
            var getResponse = await client.GetAsync(url);
            var content = await getResponse.Content.ReadAsStringAsync();
            var retrievedList = JsonConvert.DeserializeObject<IEnumerable<object>>(content);
            return retrievedList?.Count();
        }
    }
}
