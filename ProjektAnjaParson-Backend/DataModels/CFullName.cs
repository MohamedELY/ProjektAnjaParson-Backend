using ProjektAnjaParson_Backend.Models;
using ProjektAnjaParson_Backend.Contollers;

namespace ProjektAnjaParson_Backend.DataModels
{
    public class CFullName
    {
        public int Id { get; set; }

        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        /// <summary>
        /// Creates a full name and returns its ID.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>int</returns>
       public static int CreateFullName(string firstName, string lastName)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                // Add first and lastname to database

                /*var fisrtNameController = new FirstNameController();
                var lastNameController = new LastNameController();
                fisrtNameController.Post(firstName);
                lastNameController.Post(lastName);*/

                var existFname = db.FirstNames.SingleOrDefault(c => c.FirstName1.ToLower() == firstName.ToLower());
                if (existFname == null)
                {
                    db.FirstNames.Add(new FirstName() { FirstName1 = firstName });

                    db.SaveChanges();
                }
                
                Console.WriteLine("First Name Has been Saved to DB");

                var existLname = db.LastNames.SingleOrDefault(c => c.LastName1.ToLower() == lastName.ToLower());
                if (existLname == null)
                {
                    db.LastNames.Add(new LastName() { LastName1 = lastName });

                    db.SaveChanges();
                }

                // Add fullname to database

                /*var firstNameId = fisrtNameController.Get(firstName);
                var lastNameId = lastNameController.Get(lastName);
                var fullNameController = new FullNameController();
                fullNameController.Post(lastNameId.Id, firstNameId.Id);*/
                
                var getFirstNameByName = db.FirstNames.SingleOrDefault(c => c.FirstName1 == firstName);
                Console.WriteLine("Retrieving First Name From DB");
                var getLastNameByName = db.LastNames.SingleOrDefault(c => c.LastName1 == lastName);
                Console.WriteLine("Retrieving Last Name From DB");
                var existsFullName = db.FullNames.SingleOrDefault(c => c.FirstNameId == getFirstNameByName.Id && c.LastNameId == getLastNameByName.Id);
                if (existsFullName == null)
                {
                    var data = db.FullNames;
                    data.Add(new FullName() { FirstNameId = getFirstNameByName.Id, LastNameId = getLastNameByName.Id });

                    db.SaveChanges();
                }


                var getFullNameID = (from f in db.FullNames
                                        orderby f.Id descending
                                        select f.Id).FirstOrDefault();

                return getFullNameID;
            }
        }
    }

}
