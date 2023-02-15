
namespace ProjektAnjaParson_Backend.Helpers
{
    public static class HelperMethods
    {
        /// <summary>
        /// Takes an array of any number of ID's as parameter and checks if they are valid (positive integers).
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>bool</returns>
        public static bool CheckIfIdsAreValid(params int?[] ids)
        {
            foreach(var id in ids)
            {
                if (id < 1)
                    return false;
            }

            return true;
        }

        public static bool CheckIfStringsAreValid(params string?[] strings)
        {
            foreach(var str in strings) 
            {
                if (string.IsNullOrEmpty(str))
                    return false;
            }
            return true;
        }
    }
}
