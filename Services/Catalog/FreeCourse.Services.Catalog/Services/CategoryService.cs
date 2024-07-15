using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    internal class CategoryService
    {

        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        //veri tabanı baglantısı
        public CategoryService( IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        //Katergorileri listele
        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = _categoryCollection.Find(category => true).ToListAsync();

            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }
    }
}
