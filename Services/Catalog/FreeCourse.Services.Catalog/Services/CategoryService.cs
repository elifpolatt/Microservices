using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    internal class CategoryService : ICategoryService
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

        //Kategorileri listele
        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = _categoryCollection.Find(category => true).ToListAsync();

            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        //await: Async metotları beklemek ıcın kullanlır.
        //veri tabanına kategori nesnesi ekleyebilmek ve bunun sonucunda basarılı olarak bır yanıt donmesı ıcın kullanılan metot
        public async Task<Response<CategoryDto>> CreateAsync(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);

            // Dönüş türünü Response<CategoryDto> olarak düzeltin
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
        
        //Kategori nesnesi id ile alınır. bu veri dtoya donusturulur ve kullanlır.
        //basarılı ve basarısızlık durumları da ele alınmıstır.
        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found", 400);
            }

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
