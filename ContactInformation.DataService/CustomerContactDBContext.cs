namespace ContactInformation.DataService
{
    using ReadModel;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CustomerContactDBContext : DbContext
    {
        // Your context has been configured to use a 'CustomerContactDBContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ContactInformation.DataService.CustomerContactDBContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CustomerContactDBContext' 
        // connection string in the application configuration file.
        public CustomerContactDBContext()
            : base("name=CustomerContactDBContext")
        {

        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<CustomerContact> CustomerContacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<CustomerContactDBContext>(null);

            modelBuilder.Entity<CustomerContact>()
                .HasRequired<Customer>(x => x.Customer)
                .WithMany(x=>x.CustomerContacts)
                .HasForeignKey(x => x.CustomerId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CustomerContact>()
                .HasRequired<ContactType>(x => x.ContactType)
                .WithMany(x=>x.CustomerContacts)
                .HasForeignKey(x => x.ContactTypeId)
                .WillCascadeOnDelete(true);

            //base.OnModelCreating(modelBuilder);
        }


    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}