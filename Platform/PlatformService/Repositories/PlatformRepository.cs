using PlatformModels.Data;
using PlatformModels.Models;

namespace PlatformService.Repositories {
    public class PlatformRepository : IRepository<Platform> {

        private readonly PlatformDbContext _db;

        public PlatformRepository(PlatformDbContext db) {
            _db = db;
        }



        public void Create(Platform x) {
            if(x == null)
                throw new ArgumentNullException(nameof(x));


            _db.Platforms.Add(x);
        }

        public IEnumerable<Platform> GetAll() {
            return _db.Platforms.ToList();
        }

        public Platform? GetById(int id) {
            return _db.Platforms.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges() {
            return _db.SaveChanges() >= 0;
        }
    }
}
