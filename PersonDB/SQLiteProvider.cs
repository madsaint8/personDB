    public class SQLiteProvider : ProviderBase
    {
        public override int Delete(string ID)
        {
            //
            //Method Name     : int Delete(string ID)
            //Purpose         : Try to delete a row from the Person datastore
            //Re-use          : none
            //Input Parameter : string ID
            //                  - the ID of the Person to delete in the Person datastore
            //Output Type     : - int
            //                    0 : Person found and deleted successfully
            //                   -1 : Person not deleted because the record was
            //                        not found
            //
            Person person;
            int rc = 0;

            try
            {
                using (PersonContext db = new PersonContext())
                {
                    person = db.Persons.FirstOrDefault(b => b.ID.Equals(ID));
                    if (person == null) // not found
                    {
                        rc = -1;
                    } // end if
                    else
                    {
                        db.Persons.Remove(person);
                        db.SaveChanges();
                    } // end else
                } // end using
            } // end try
            catch (Exception ex)
            {
                throw ex;
            } // end catch            
            return rc;
        } // end method

        public override int Insert(Person newPerson)
        {
            //
            //Method Name     : int Insert(Person newPerson)
            //Purpose         : Try to insert a row in the Person datastore
            //Re-use          : none
            //Input Parameter : Person newPerson
            //                  - The Person object to add to the Person datastore
            //Output Type     : - int
            //                    0 : newPerson inserted into datastore
            //                   -1 : newPerson not inserted because a duplicate
            //                        was found
            //
            Person person;
            int rc = 0;
            try
            {
                using (PersonContext db = new PersonContext())
                {
                    person = db.Persons.FirstOrDefault(b => b.ID.Equals(newPerson.ID));
                    if (person == null) // not found
                    {
                        db.Persons.Add(newPerson);
                        db.SaveChanges();
                    } // end if
                    else
                    {
                        rc = -1;
                    } // end else
                } // end using
            } // end try
            catch (Exception ex)
            {
                throw ex;
            } // end catch
            return rc;
        } // end method

        public override List<Person> SelectAll()
        {
            //
            //Method Name     : List<Person> SelectAll()
            //Purpose         : Try to get all the Person objects from the datastore
            //Re-use          : none
            //Input Parameter : None        
            //Output Type     : - List<Person>
            //                    - the List<Person> list that will contain the Person objects loaded from datastore         
            //
            List<Person> list;

            try
            {
                list = new List<Person>();
                using (PersonContext db = new PersonContext())
                {
                    //
                    // The next block of code can be used
                    // to select all the items in db.Persons
                    // and to sort the result on p.ID
                    //
                    // Then a foreach can be used to add the
                    // sorted result to list
                    //
                    // Sorting is optional
                    //
                    //var query = from p in db.Persons
                    //            orderby p.ID
                    //            select p;
                    //foreach (Person item in query)
                    //{
                    //    list.Add(item);
                    //} //end foreach                    

                    foreach (var item in db.Persons)
                    {
                        list.Add(item);
                    } // end foreach
                } // end using
            } //end try
            catch (Exception ex)
            {
                throw ex;
            } // end catch
            return list;
        } // end method

        public override int SelectPerson(string ID, ref Person person)
        {
            //
            //Method Name     : int SelectPerson(string ID, ref Person person)
            //Purpose         : Try to get a single Person object from the datastore
            //Re-use          : none
            //Input Parameter : - string ID
            //                    - The ID of the Person to load from the datastore
            //                  - ref Person person
            //                    - The Person object loaded from the datastore
            //Output Type     : - int
            //                    0 : Person loaded from datastore
            //                   -1 : no Person was loaded from the datastore
            //                        (not found)
            //
            int rc;
            try
            {
                using (PersonContext db = new PersonContext())
                {
                    person = db.Persons.FirstOrDefault(p => p.ID.Equals(ID));
                    if (person == null) // not found
                    {
                        rc = -1;
                    } //end if
                    else
                    {
                        rc = 0;
                    } // end else
                } // end using
            } // end try
            catch (Exception ex)
            {
                throw ex;
            } // end catch
            return rc;
        } // end method

        public override int Update(Person existingPerson)
        {
            //
            //Method Name     : int Update(Person existingPerson)
            //Purpose         : Try to update a row in the datastore
            //Re-use          : none
            //Input Parameter : Person existingPerson
            //                  - The new Person data for the row in the datastore
            //Output Type     : - int
            //                    0 : person found and updated successfully
            //                   -1 : person not updated because the record was
            //                        not found
            //
            Person person;
            int rc = 0;
            try
            {
                using (PersonContext db = new PersonContext())
                {
                    person = db.Persons.FirstOrDefault(p => p.ID.Equals(existingPerson.ID));
                    if (person == null) // not found
                    {
                        rc = -1;
                    } // end if
                    else
                    {
                        person.Age = existingPerson.Age;
                        person.FirstName = existingPerson.FirstName;
                        person.LastName = existingPerson.LastName;
                        db.SaveChanges();
                    } // end else
                } // end using
            } // end try
            catch (Exception ex)
            {
                throw ex;
            } // end catch
            return rc;
        } // end method
    } //end class
