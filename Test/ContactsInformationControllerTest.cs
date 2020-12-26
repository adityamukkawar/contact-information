using DAL.Models;
using DAL.Repository;
using EvolentContactInformation.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test
{
    public class ContactsInformationControllerTest
    {
        private  UnitOfWork _UOW;
        public static DbContextOptions<EvolentContactInformationContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=EvolentContactInformation;user id=sa;password=*****;";

        static ContactsInformationControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<EvolentContactInformationContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public ContactsInformationControllerTest()
        {
            var context = new EvolentContactInformationContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);

            _UOW = new UnitOfWork(context);
        }

        [Fact]
        public void Details()
        {
            //Arrange  
            var controller = new ContactsInformationController(_UOW);
            var id = 2;

            //Act  
            var data = controller.Details(id);

            //Assert  
            Assert.IsType<ViewResult>(data);
        }

        [Fact]
        public void Create()
        {
            var controller = new ContactsInformationController(_UOW);
            var contact = new Contact() { FirstName = "User4", LastName = "User4SLastname", EmailId = "test@gmail.com", PhoneNumber = "9999999999", Status = false };
            var data = controller.Create(contact);
            Assert.IsType<RedirectToActionResult>(data);
        }

        [Fact]
        public void Update()
        {
            var controller = new ContactsInformationController(_UOW);
            var id = 2;
            var existingResult = controller.Details(id);
            var okResult = existingResult.Should().BeOfType<ViewResult>().Subject;
            var result = okResult.Model.Should().BeAssignableTo<Contact>().Subject;
            result.FirstName = "user4";            
            var updatedData = controller.Edit(2, result);
            Assert.IsType<RedirectToActionResult>(updatedData);
        }

        [Fact]
        public void Delete()
        {
            var controller = new ContactsInformationController(_UOW);
            var id = 2;
            var data = controller.DeleteConfirmed(id);
            Assert.IsType<RedirectToActionResult>(data);
        }
    }
}
