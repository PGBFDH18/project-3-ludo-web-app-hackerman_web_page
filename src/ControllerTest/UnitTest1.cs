using System;
using Xunit;
using Hackerman_WebbApp.Controllers;
using RestSharp;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace ControllerTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task ReadListOfGamesAsync()
        {
            // Arrange

            RestClient client = new RestClient();
         
            var controller = new LudoController(client);


            // Act

            IActionResult result =await controller.ListGames();

            // Assert

            Assert.NotNull(result);



        }
    }
}
