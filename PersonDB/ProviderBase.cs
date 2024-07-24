    public abstract class ProviderBase
    {
        /// <summary>
        /// This method gets the list of all the business objects from the Person datastore.
        /// It returns the list of business objects
        /// </summary>
        public abstract List<Person> SelectAll();

        /// <summary>
        /// This method gets a single Person object from the Person datastore.
        /// It returns 0 to indicate the Person was loaded from datastore, or
        /// -1 to indicate that no Person was loaded from the datastore (not found).
        /// </summary>
        /// <param name="ID">The ID of the Person to load from the datastore.</param>
        /// <param name="person">The Person object loaded from the datastore.</param>
        public abstract int SelectPerson(string ID, ref Person person);

        /// <summary>
        /// This method inserts a record in the Person datastore. 
        /// It returns 0 to indicate the Person was inserted into datastore, or
        /// -1 to indicate the Person was not inserted because a duplicate was found
        /// </summary>
        /// <param name="newPerson">The Person object to add to the Person datastore.</param>
        public abstract int Insert(Person newPerson);

        /// <summary>
        /// This method updates a record in the Person datastore.
        /// It returns 0 to indicate the Person was found and updated successfully, or
        ///  -1 to indicate the Person was not updated because the record was not found
        /// </summary>
        /// <param name="existingPerson">The new Person data for the record in the Person datastore.</param>
        public abstract int Update(Person existingPerson);

        /// <summary>
        /// This method deletes a record in the Person datastore.
        /// It returns 0 to indicate the Person was found and deleted successfully, or
        ///  -1 to indicate the Person was not deleted because the record was not found
        /// </summary>
        /// <param name="ID">The Person ID of the Person to delete in the Person datastore.</param>
        public abstract int Delete(string ID);

    } // end class
