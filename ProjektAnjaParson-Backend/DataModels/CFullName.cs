using ProjektAnjaParson_Backend.Models;
using ProjektAnjaParson_Backend.Contollers;

namespace ProjektAnjaParson_Backend.DataModels
{
    public class CFullName
    {
        public int Id { get; set; }

        public string? LastName { get; set; }

        public string? FirstName { get; set; }


       public static int CreateFullName(string firstName, string lastName)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {
                // Add first and lastname to database

                var fisrtNameController = new FirstNameController();
                var lastNameController = new LastNameController();
                fisrtNameController.Post(firstName);
                lastNameController.Post(lastName);
                
                
                // Add fullname to database

                var firstNameId = fisrtNameController.Get(firstName);
                var lastNameId = lastNameController.Get(lastName);
                var fullNameController = new FullNameController();
                fullNameController.Post(lastNameId.Id, firstNameId.Id);

                var getFullNameID = (from f in db.FullNames
                                    orderby f.Id descending
                                    select f.Id).FirstOrDefault(); ;

                return getFullNameID;




            }
        }
    }

}
