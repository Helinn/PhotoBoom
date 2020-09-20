namespace PhotoBoom.Settings
{
    public class GalleryDatabaseSettings : IGalleryDatabaseSettings
    {
        public GalleryDatabaseSettings()
        {
        }

        public GalleryDatabaseSettings(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            GalleryCollectionName = "Gallery";
        }
        
        public string GalleryCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}