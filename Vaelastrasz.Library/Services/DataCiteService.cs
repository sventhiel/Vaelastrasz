using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Services
{
    public class DataCiteService
    {
        private readonly Configuration _config;

        public DataCiteService(Configuration config) 
        { 
            _config= config;
        }

        public CreateDataCiteModel Create()
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