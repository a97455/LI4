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

    public async Task<Licitacao> GetLicitacaoById(int licitacaoId){
        string sql = "SELECT * FROM Licitacao WHERE Id = @Id";
        var parameters = new { Id = licitacaoId };
        List<Licitacao> licitacaoList = await _db.LoadData<Licitacao, dynamic>(sql, parameters);
        return licitacaoList.FirstOrDefault()!;
    }

    public async Task<int> NumbLicitacoesByLeilao(int idLeilao){
        string sql = "SELECT COUNT(*) FROM Licitacao WHERE id_leilao = @id_leilao";
        var parameters = new { id_leilao = idLeilao };
        List<int> count = await _db.LoadData<int, dynamic>(sql, parameters);
        return count.FirstOrDefault()!;
    }

    public async Task<int?> MaiorLicitacaoByLeilao(int idLeilao) {
    try {
        string sql = "SELECT MAX(Valor) FROM Licitacao WHERE id_leilao = @id_leilao;";
        var parameters = new { id_leilao = idLeilao };

        var result = await _db.LoadData<int?, dynamic>(sql, parameters);

        int? maxValue = result.FirstOrDefault();

        return maxValue;
    } catch{
        return 0;
    }
}


    public async Task<bool> PutLicitacao(Licitacao licitacao){
        string sql = @"
            MERGE INTO Licitacao AS target
            USING (VALUES (DEFAULT)) AS source (Id)
            ON target.Id = source.Id
            WHEN MATCHED THEN
                UPDATE SET
                    Valor = @Valor,
                    EmailLicitador = @EmailLicitador
            WHEN NOT MATCHED THEN
                INSERT (Valor, EmailLicitador)
                VALUES (@Valor, @EmailLicitador);";

        var parameters = new
        {
            licitacao.Valor,
            licitacao.EmailLicitador
        };

        await _db.SaveData(sql, parameters);

        // Retorna true, pois a operação foi bem-sucedida (não houve exceção)
        return true;
    }
}