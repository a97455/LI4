namespace DataLayer;
public class LeilaoDAO : ILeilaoDAO
{
    private ISqlDataAccess _db;
    public LeilaoDAO(ISqlDataAccess db)
    {
        _db = db;
    }
    public Task<List<Leilao>> FindAll()
    {
        string sql = "select * from Leilao";
        return _db.LoadData<Leilao, dynamic>(sql, new { });
    }
}