    internal class Program
    {
        public static List<Person> personList;
        public static PersonBL pBL;

        static void Initialise()
        {
            //
            //Method Name     : void Initialise() 
            //Purpose         : Initialise and instantiate global variables
            //Re-use          : none
            //Input Parameter : none
            //Output Type     : none
            //
            personList = new List<Person>();
            pBL = new PersonBL("SQLiteProvider");
            //pBL = new PersonBL("MySQLProvider");
            //pBL = new PersonBL("XMLProvider");
        } // end method

        //--------------------------------------------------------------------------------
        //
        // Display menus
        //
        //--------------------------------------------------------------------------------

        public static void ShowMainMenu()
        {
            //
            //Method Name     : void ShowMainMenu() 
            //Purpose         : Display the main menu
            //Re-use          : none
            //Input Parameter : none
            //Output Type     : none
            //
            WriteLine();
            WriteLine("Please select an option:");
            WriteLine("========================");
            WriteLine("1. Person Maintenance");
            WriteLine("X. Exit");
            WriteLine();
        } // end method ShowMainMenu()

        public static void ShowPersonMaint()
        {
            //
            //Method Name     : void ShowPersonMaint() 
            //Purpose         : Display the Person Maintenance menu
            //Re-use          : none
            //Input Parameter : none
            //Output Type     : none
            //
            WriteLine();
            WriteLine("Person Maintenance: Please select an option:");
            WriteLine("=============================================");
            WriteLine("1. List Person");
            WriteLine("2. Add Person");
            WriteLine("3. Remove Person");
            WriteLine("4. Update Person");
            WriteLine("R. Return");
            WriteLine();
        } // end method ShowPersonMaint()

        //--------------------------------------------------------------------------------
        //
        // Process menus
        //
        //--------------------------------------------------------------------------------
        public static void ProcessPersonMenu()
        {
            //
            //Method Name     : void ProcessPersonMenu() 
            //Purpose         : Invoke appropriate method to handle user menu selection
            //Re-use          : ShowPersonMaint();PersonList();PersonAdd();PersonRemove();
            //                  PersonUpdate()
            //Input Parameter : none
            //Output Type     : none
            //
            char choice = '0';
            ConsoleKeyInfo cki;

            WriteLine();
            ShowPersonMaint();

            cki = ReadKey();
            WriteLine();
            choice = cki.KeyChar;

            while (choice != 'r' && choice != 'R')
            {
                switch (choice)
                {
                    case '1':
                        PersonList();
                        break;
                    case '2':
                        PersonAdd();
                        break;
                    case '3':
                        PersonRemove();
                        break;
                    case '4':
                        PersonUpdate();
                        break;
                    case 'R':
                    case 'r':
                        break;
                    default:
                        WriteLine("Invalid input");
                        break;
                } // end switch
                WriteLine();
                ShowPersonMaint();

                cki = ReadKey();
                WriteLine();
                choice = cki.KeyChar;
            } // end while
        } // end method


        //--------------------------------------------------------------------------------
        //
        // Person related methods
        //
        //--------------------------------------------------------------------------------

        public static void PersonUpdate()
        {
            //
            //Method Name     : void PersonUpdate() 
            //Purpose         : Update existing person info
            //Re-use          : none
            //Input Parameter : none
            //Output Type     : none
            //
            string ID = "";
            string age = "";
            string firstName = "";
            string lastName = "";
            int rc = 0;
            bool change = false;
            Person person = new Person();

            Write("Please enter the person ID: ");
            ID = ReadLine().ToUpper();
            rc = pBL.SelectPerson(ID, ref person);
            if (rc == 0)
            {
                person.ID = ID;
                WriteLine(person);
                Write("New age or press enter not to change: ");
                age = ReadLine();
                if (age.Length != 0)
                {
                    person.Age = Convert.ToInt32(age);
                    change = true;
                } // end if

                Write("New first name or press enter not to change: ");
                firstName = ReadLine();
                if (firstName.Length != 0)
                {
                    person.FirstName = firstName;
                    change = true;
                } // end if

                Write("New last name or press enter not to change: ");
                lastName = ReadLine();
                if (lastName.Length != 0)
                {
                    person.LastName = lastName;
                    change = true;
                } // end if

                if (change)
                {
                    rc = pBL.Update(person);
                    if (rc == -1)
                    {
                        WriteLine(ID + " NOT updated since it is not in the DB");
                    } // end if
                    else
                    {
                        WriteLine(ID + " updated");
                    } // end else
                } // end if
                else
                {
                    WriteLine("Nothing selected to update");
                } // end else

            } // end if
            else
            {
                WriteLine(ID + " NOT found");
            } // end else
        } // end method

        public static void PersonAdd()
        {
            //
            //Method Name     : void PersonAdd() 
            //Purpose         : Get new Person info and try to add it to DB
            //Re-use          : none
            //Input Parameter : none
            //Output Type     : none
            //
            string ID = "";
            string firstName = "";
            string lastName = "";
            int age = 0;
            int rc = 0;
            Person person = null;

            WriteLine("Please supply the following person info:");
            Write("ID: ");
            ID = ReadLine().ToUpper();
            rc = pBL.SelectPerson(ID, ref person);
            if (rc == 0)
            {
                WriteLine(ID + " NOT added since it is already in the DB");
            } // end if
            else
            {
                Write("Age: ");
                age = Convert.ToInt32(ReadLine());
                Write("First Name: ");
                firstName = ReadLine();
                Write("Last Name: ");
                lastName = ReadLine();

                rc = pBL.Insert(new Person(ID, age, firstName, lastName));
                if (rc == -1)
                {
                    WriteLine(ID + " NOT added since it is already in the DB");
                } // end if
                else
                {
                    WriteLine(ID + " added");
                } // end else
            } // end else


        } // end method

        public static void PersonRemove()
        {
            //
            //Method Name     : void PersonRemove() 
            //Purpose         : Try to remove a Person record from the DB
            //Re-use          : none
            //Input Parameter : none
            //Output Type     : none
            //
            string code = "";
            int rc = 0;

            if (personList.Count > 0)
            {
                Write("Please enter the person ID: ");
                code = ReadLine().ToUpper();
                rc = pBL.Delete(code);
                if (rc == 0)
                {
                    WriteLine(code + " removed from DB");
                } // end if
                else
                {
                    WriteLine(code + " NOT removed since it is not in the DB");
                } // end else
            } // end if
            else
            {
                WriteLine("No person record to remove from DB");
            } // end else
        } // end method

        public static void PersonList()
        {
            //
            //Method Name     : void PersonList() 
            //Purpose         : Display the Person records in the DB
            //Re-use          : none
            //Input Parameter : none
            //Output Type     : none
            //
            personList = pBL.SelectAll();
            if (personList.Count == 0)
            {
                WriteLine("No Person records found");
            } // end if
            else
            {
                foreach (Person pRef in personList)
                {
                    WriteLine(pRef);
                } // end foreach
            } // end if
        } // end method

        //--------------------------------------------------------------------------------
        //
        // Main
        //
        //--------------------------------------------------------------------------------
        public static void Main(string[] args)
        {
            //
            //Method Name     : void Main(string[] args)
            //Purpose         : Main entry into program
            //Re-use          : ShowMainMenu(); ProcessPersonMenu()
            //Input Parameter : string[] args
            //                  - command line args - not used
            //Output Type     : none
            //

            char choice = '0';
            ConsoleKeyInfo cki;

            try
            {
            Initialise();


                WriteLine();
                ShowMainMenu();

                cki = ReadKey();
                WriteLine();
                choice = cki.KeyChar;

                while (choice != 'x' && choice != 'X')
                {
                    switch (choice)
                    {
                        case '1':
                            ProcessPersonMenu();
                            break;
                        case 'x':
                        case 'X':
                            break;
                        default:
                            WriteLine("Invalid input");
                            break;
                    } // end switch

                    WriteLine();
                    ShowMainMenu();

                    cki = ReadKey();
                    WriteLine();
                    choice = cki.KeyChar;
                } // end while
            } // end try 
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            } // end catch
        } // end method Main()        
    }    // end class
