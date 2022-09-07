using BerichtsHeft.Shared;

namespace BerichtsHeft.Client.Data.Repository
{
    public interface IRepository
    {
        IEnumerable<DateiInfo> CRUD_Create();

        void CRUD_Read(string dateiName);

        void CRUD_Edit(DateiInfo changedItem);

        void CRUD_Delete(int id);
    }
}
