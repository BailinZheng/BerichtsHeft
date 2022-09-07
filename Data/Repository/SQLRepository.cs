using BerichtsHeft.Shared;

namespace BerichtsHeft.Client.Data.Repository
{
    public class SQLRepository : IRepository
    {
        private readonly DB _context;

        public SQLRepository(DB context)
        {
            _context = context;
        }
        public IEnumerable<DateiInfo> CRUD_Create()
        {
            return _context.DateiInfos;
        }

        public IEnumerable<DateiInfo> CRUD

        public void CRUD_Delete(int id)
        {
            var deletedItem = _context.DateiInfos.Find(id);

            if (deletedItem != null)
            {
                _context.DateiInfos.Remove(deletedItem);
                _context.SaveChanges();
            }
        }

        public void CRUD_Edit(DateiInfo changedItem)
        {
            var item = _context.DateiInfos.Attach(changedItem);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
        }

        public void CRUD_Read(string dateiName)
        {
            DateiInfo newItem = new DateiInfo()
            {
                HauptText = dateiName

            };

            _context.DateiInfos.Add(newItem); 
            _context.SaveChanges();
        }
    }
}
