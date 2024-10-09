using CocktailStrategist.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;
using System.Transactions;

namespace CocktailStrategistIntegrationTests
{
    [TestFixture]
    public class DrinkControllerTests
    {
        private TransactionScope scope;

        [SetUp]
        public void Init()
        {
            scope = new TransactionScope();
            Console.WriteLine("starting scope");
        }

        [TearDown]
        public void CleanUp()
        {
            Console.WriteLine("destroying scope");
            scope.Dispose();
        }

        //[Test]
        //public async Task Get()
        //{
        //    // Arrange
        //    var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        //    {
        //    });
        //    var client = factory.CreateClient();
        //    var url = "/drink";

        //    // Act

        //    var response = await client.GetAsync(url);
        //    var content = await response.Content.ReadAsStringAsync();

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //    //Assert.That(content.Equals("value2"));
        //}

        [Test]
        public async Task DrinkControllerCreatesNewDrink()
        {
                // Arrange
                var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
                {
                });
                var client = factory.CreateClient();
                var url = "/drink";
                var id = Guid.NewGuid();
                var getUrl = $"/drink/{id}";

                var drink =  new Drink { Id = id, Name = "Test drink" } ;
                var payload = JsonConvert.SerializeObject(drink);
                var request = new StringContent(payload, Encoding.UTF8, "application/json");

                // Act

                var createResponse = await client.PostAsync(url, request);
                var getResponse = await client.GetAsync(getUrl);
                var content = await getResponse.Content.ReadAsStringAsync();
                var retrievedDrink = JsonConvert.DeserializeObject<Drink>(content); 
                

                // Assert
                createResponse.EnsureSuccessStatusCode();

                retrievedDrink.Should().BeEquivalentTo(drink, o => o.ComparingByMembers<Drink>());
        }

        // drink with a non-existant ingredient
        //test
    }
}