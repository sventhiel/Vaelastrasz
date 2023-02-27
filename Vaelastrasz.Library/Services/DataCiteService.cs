using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Services
{
    public class DataCiteService
    {
        public CreateDataCiteModel Create(long datasetId, long version = 0)
        {
            return new CreateDataCiteModel();
        }

        public void FindByDoi(string doi)
        {
        }

        public void Find()
        {
        }

        public void Update()
        { }

        public void Delete()
        { }
    }
}