namespace Sample.App
{
    public class UserSampleEntityModel
    {
        public int Id { get; set; }
        public int? A { get; set; }
        public Nullable<int> B { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Created { get; set; }

        private string UserName { get; set; }
        public string UserName2 { get; private set; }
    }
}
