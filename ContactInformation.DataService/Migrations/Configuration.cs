namespace ContactInformation.DataService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContactInformation.DataService.CustomerContactDBContext>
    {
        public Configuration()
        {
            CommandTimeout = Int32.MaxValue;
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContactInformation.DataService.CustomerContactDBContext context)
        {
#if DEBUG

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            context.ContactTypes.AddOrUpdate(
                x => x.Id,
                new ReadModel.ContactType() { Id = 1, Type = "Mobile" },
                new ReadModel.ContactType() { Id = 2, Type = "Email" }
                );

            context.Customers.AddOrUpdate(
                x => x.Id,
                new ReadModel.Customer() { Id = 1, FirstName = "Vishal", LastName = "Deshpande" },
                new ReadModel.Customer() { Id = 2, FirstName = "William", LastName = "Smith" },
                 new ReadModel.Customer() { Id = 3, FirstName = "Jonathan", LastName = "Seagal" }
                 );

            context.CustomerContacts.AddOrUpdate(
                x => x.Id,
                new ReadModel.CustomerContact() { Id = 1, ContactStatus = "Active", ContactTypeId = 1, ContactValue = "9028224072", CustomerId = 1 },
                new ReadModel.CustomerContact() { Id = 2, ContactStatus = "Active", ContactTypeId = 1, ContactValue = "9158892338", CustomerId = 1 },
                new ReadModel.CustomerContact() { Id = 3, ContactStatus = "Active", ContactTypeId = 2, ContactValue = "vishal.deshp@gmail.com", CustomerId = 1 },
                new ReadModel.CustomerContact() { Id = 4, ContactStatus = "Active", ContactTypeId = 1, ContactValue = "1234567890", CustomerId = 2 },
                new ReadModel.CustomerContact() { Id = 5, ContactStatus = "Active", ContactTypeId = 1, ContactValue = "william@gmail.com", CustomerId = 2 },
                new ReadModel.CustomerContact() { Id = 6, ContactStatus = "Active", ContactTypeId = 1, ContactValue = "0987654321", CustomerId = 3 },
                new ReadModel.CustomerContact() { Id = 7, ContactStatus = "Active", ContactTypeId = 1, ContactValue = "7890654321", CustomerId = 3 },
                new ReadModel.CustomerContact() { Id = 8, ContactStatus = "Active", ContactTypeId = 1, ContactValue = "jonatahn@gmail.com", CustomerId = 3 },
                new ReadModel.CustomerContact() { Id = 9, ContactStatus = "Active", ContactTypeId = 1, ContactValue = "seagal@gmail.com", CustomerId = 3 }
                );
#endif
        }
    }
}
