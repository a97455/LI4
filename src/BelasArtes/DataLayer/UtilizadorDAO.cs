using Datalayer;

namespace DataLayer;
public class UtilizadorDAO : IUtilizadorDAO
{
    private ISqlDataAccess _db;
    public UtilizadorDAO(ISqlDataAccess db)
    {
        _db = db;
    }    
    
    public Task<List<Utilizador>> FindAll()
    {
        string sql = "select * from Utilizador";
        return _db.LoadData<Utilizador, dynamic>(sql, new { });
    }
}