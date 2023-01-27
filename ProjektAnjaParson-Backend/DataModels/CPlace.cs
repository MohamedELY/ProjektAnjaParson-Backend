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

        public static int CreataLocation(string location, string country)
        {
            using (var db = new AppDbContext.ApdatabaseContext())
            {

                var countryController = new CountryController();
                countryController.Post(country);
                var locationController = new LocationController();
                var newCountry = countryController.Get(country);
                locationController.Post(location, newCountry.Id);



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
