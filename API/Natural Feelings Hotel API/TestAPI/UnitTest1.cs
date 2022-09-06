using Natural_Feelings_Hotel_API.Controllers;
using Natural_Feelings_Hotel_API.Models;
using NUnit.Framework;

namespace TestAPI
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            AdminController adminController = new AdminController();
            bool result = adminController.UpdateOffer(new Offer
            {
                Id_Offer = 1,
                Description = "Habibi",
                Price = 299,
                Image = "Image1.png",
                Name = "Mitad"
            });
            Assert.AreEqual(true, result);
        }
    }
}