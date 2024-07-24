    //
    // The PersonContext class inherits from the Entity Framework’s DbContext
    // (Database Context) class for creating, updating, and deleting the
    // Person objects as needed in the database
    //
    public class PersonContext : DbContext
    {
        // List<Person>: a collection of all the Person entities in the database
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //
            // Called by the Entity Framework to connect to the database
            //
            // pass a DbContextOptionsBuilder parameter to the OnConfiguring override
            // method and call the UseSqlite options method to specify that it
            // will connect to a SQLite database. A connection string is passed
            // with the details used to connect to the database
            optionsBuilder.UseSqlite($"Data Source=Persons.db");
            
        } // end method
    } // end class
