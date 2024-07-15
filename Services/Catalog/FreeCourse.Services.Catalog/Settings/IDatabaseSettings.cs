namespace FreeCourse.Services.Catalog.Settings
{
    public interface IDatabaseSettings
    {
        //appsettings.json dosyası ıcındekı verı tabanı bılgılerını almak ıcın bu ınterface kullanılacak.

        public string CourseCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
       
    }
}
