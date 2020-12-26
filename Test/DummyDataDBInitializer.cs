using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class DummyDataDBInitializer
    {
        public void Seed(EvolentContactInformationContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Contact.AddRange(
                new Contact() { FirstName = "User1", LastName = "User1SLastname", EmailId = "abc@gmail.com", PhoneNumber = "9999999999", Status = true },
                new Contact() { FirstName = "User2", LastName = "User2SLastname", EmailId = "xyz@gmail.com", PhoneNumber = "9999999999", Status = true },
                new Contact() { FirstName = "User3", LastName = "User3SLastname", EmailId = "test@gmail.com", PhoneNumber = "9999999999", Status = false }
                );

            context.SaveChanges();
        }
    }
}
