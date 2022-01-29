using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using CarFactory_Domain;
using CarFactory_Factory;

namespace CarFactory_Storage
{
    public class GetRubberQuery : IGetRubberQuery
    {
        private readonly IStorageProvider _storageProvider;

        public GetRubberQuery(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        public IEnumerable<Part> Get()
        {
            using SQLiteConnection conn = _storageProvider.GetConnection();
            using SQLiteCommand cmd = new SQLiteCommand(conn);
            
            cmd.CommandText = @$"Select * from manufacturer_part where partId={(int)PartType.Rubber}";
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            var parts = new List<Part>();
            while (rdr.Read())
            {
                parts.Add(new Part() { Manufacturer = (Manufacturer)rdr.GetInt32(1), PartType = (PartType)rdr.GetInt32(2)});
            }

            return parts;
        }
    }
}