namespace DataLayer;
public class LicitacaoDAO : ILicitacaoDAO
{
    private ISqlDataAccess _db;
    public LicitacaoDAO(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<List<Licitacao>> FindAll()
    {
        string sql = "select * from Licitacao";
        return _db.LoadData<Licitacao, dynamic>(sql, new { });
    }
}