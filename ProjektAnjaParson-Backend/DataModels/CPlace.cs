using ProjektAnjaParson_Backend.Contollers;
using ProjektAnjaParson_Backend.Models;

namespace ProjektAnjaParson_Backend.DataModels
{
    public class CPlace
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public string? Address { get; set; }

        public string? Category { get; set; }

        public string? Country { get; set; }

        public string? Pic { get; set; }

        public static int CreataLocation(string locationName, string countryName)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {

                /*var countryController = new CountryController();
                countryController.Post(country);*/

                // Country POST
                var existCountry = db.Countries.SingleOrDefault(c => c.Name.ToLower() == countryName.ToLower());
                if (existCountry == null)
                {
                    db.Countries.Add(new Country() { Name = countryName });
                    db.SaveChanges();
                }


                /*var locationController = new LocationController();
                var newCountry = countryController.Get(country);*/
                
                /*locationController.Post(location, newCountry.Id);*/

               // Location GET
                var getCountry = db.Countries.SingleOrDefault(c => c.Name.ToLower() == countryName.ToLower());
                
                Console.WriteLine("Retriving Country From DB");

                // Location POST
                var locationExist = db.Locations.SingleOrDefault(c => c.Name.ToLower() == locationName.ToLower());
                if (locationExist == null)
                {
                    db.Locations.Add(new Location() { Name = locationName, CountryId = getCountry.Id });
                    db.SaveChanges();
                }


                var getLocationID = (from l in db.Locations
                                         orderby l.Id descending
                                         select l.Id).FirstOrDefault();

                return getLocationID;
            }
        }

        public static int GetCategoryID(string category)
        {
            var categoryController = new CategoryController();
            var cat = categoryController.Get(category);

            return cat.Id;
        }

        
    }
}
