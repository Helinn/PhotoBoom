namespace PhotoBoom.Settings
{
    public interface IGalleryDatabaseSettings
    {
        string GalleryCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}