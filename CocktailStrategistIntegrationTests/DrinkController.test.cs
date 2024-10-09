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
        private HttpClient client;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
            });
            client = factory.CreateClient();
        }
        [OneTimeTearDown] public void FixtureTearDown() { client.Dispose(); }

        //[SetUp]
        //public void SetUp()
        //{
        //    scope = new TransactionScope();
        //    Console.WriteLine("starting scope");
        //}

        //[TearDown]
        //public void TearDown()
        //{
        //    scope.Dispose();
        //}

        [Test]
        public async Task Get_retrievesSpecifiedDrink()
        {
            // Arrange
            var drink = new Drink { Id = Guid.Parse("7fa85f64-5717-4562-b3fc-2c963f66afa6"), Name = "Mai Tai" };
            var url = $"/drink/{drink.Id}";

            // Act

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            var retrievedDrink = JsonConvert.DeserializeObject<Drink>(content);

            // Assert
            response.EnsureSuccessStatusCode();

            retrievedDrink.Should().BeEquivalentTo(drink, o => o.ComparingByMembers<Drink>());

        }

        [Test]
        public async Task Post_createsNewDrink()
        {
                // Arrange
                
                var url = "/drink";

                var drink =  new Drink { Id = Guid.NewGuid(), Name = "Test drink" } ;
                var payload = JsonConvert.SerializeObject(drink);
                var request = new StringContent(payload, Encoding.UTF8, "application/json");

                // Act

                var response = await client.PostAsync(url, request);
                var content = await response.Content.ReadAsStringAsync();
                
        }

        // drink with a non-existant ingredient
        //test
    }
}