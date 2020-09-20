using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PhotoBoom.Entities;
using PhotoBoom.Settings;


namespace PhotoBoom.Repositories
{
    public class GalleryRepository : BaseRepository<Photo>, IGalleryRepository
    {
        public GalleryRepository(IOptions<GalleryDatabaseSettings> options) : base(options)
        {
        }   
    }  
} 