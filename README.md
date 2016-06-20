# Petapoco CRUD Opeartions Helper
CRUD Operations Helper for Petapoco


Say you have a class Contact. Extend DbContext<Contact>

    [PetaPoco.PrimaryKey("Id")]
    [PetaPoco.TableName("Ac_Contact")]
    public class Contact : DbContext<Contact>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public long PhoneNumber { get; set; }
        public int Extension { get; set; }
        public long Fax { get; set; }
        public long CellNumber { get; set; }
        public string Email { get; set; }
    }
    
    
Now you can do things like:

#Create
            var newContact = new Contact();
            newContact.FirstName = "Stewie";
            newContact.LastName = "Griffin";
            newContact.PhoneNumber = 9856875182;
            newContact.Insert();
            
#Update, Delete, Save are similar
            newContact.Insert();
            newContact.Delete();
            newContact.Save();
            
#Query Builder

Find by Where column = value
             
          var contacts = Contact.Where("PhoneNumber", 9856875182); //Returns List
           
Find by Where column (operator) value
              
          var contact = Contact.Where("PhoneNumber", "LIKE", 9856875182).First(); //Use Linq if you need

Find singal item by Primary Key
              
          var contact = Contact.Get(2);
            

#For list of objects i.e. `List<Contact>` (SaveAll, DeleteAll, InsertAll, UpdateAll)
          List<Contacts> contacts =  Contact.Where("PhoneNumber", 9856875182);
          contacts.DeleteAll();
          
          
          

