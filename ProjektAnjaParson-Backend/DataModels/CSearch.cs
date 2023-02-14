namespace ProjektAnjaParson_Backend.DataModels
{
    public class CSearch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public bool? Rating { get; set; }
        /*public override bool Equals(object? other) => Equals(other as CSearch);
        public override int GetHashCode() => (this.Id, this.Name).GetHashCode();*/

        public override bool Equals(object obj)
        {
            CSearch item = obj as CSearch;

            if (item == null)
            {
                return false;
            }

            return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            // Which is preferred?

            return this.Name.GetHashCode();

            //return this.FooId.GetHashCode();
        }
    }
}
