namespace DataLayer;
public class PinturaDAO : IPinturaDAO
{
    private ISqlDataAccess _db;
    public PinturaDAO(ISqlDataAccess db)
    {
        _db = db;
    }
    public Task<List<Pintura>> FindAll()
    {
        string sql = "select * from Pintura";
        return _db.LoadData<Pintura, dynamic>(sql, new { });
    }
}