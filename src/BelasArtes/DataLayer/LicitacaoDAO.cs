namespace DataLayer;
public class LicitacaoDAO : ILicitacaoDAO{
    private ISqlDataAccess _db;
    public LicitacaoDAO(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<List<Licitacao>> FindAll(){
        string sql = "select * from Licitacao";
        return _db.LoadData<Licitacao, dynamic>(sql, new { });
    }

    public Task<List<Licitacao>> GetLicitacaoById(int? licitacaoId){
        string sql = "SELECT * FROM Licitacao WHERE Id = @Id";
        var parameters = new { Id = licitacaoId };
        return _db.LoadData<Licitacao, dynamic>(sql, parameters);
    }

    public Task<List<int>> NumbLicitacoesByLeilao(int? idLeilao){
        string sql = "SELECT COUNT(*) FROM Licitacao WHERE id_leilao = @id_leilao";
        var parameters = new { id_leilao = idLeilao };
        return _db.LoadData<int, dynamic>(sql, parameters);
    }

    public Task<List<int>> MaiorLicitacaoByLeilao(int? idLeilao) {
        string sql = "SELECT MAX(Valor) FROM Licitacao WHERE id_leilao = @id_leilao;";
        var parameters = new { id_leilao = idLeilao };
        return  _db.LoadData<int, dynamic>(sql, parameters);
    }

    public Task<List<int>> ContaLicitacoes(){
        string sql = "SELECT COUNT(*) FROM Licitacao";
        var parameters = new { };
        return _db.LoadData<int, dynamic>(sql, parameters);
    }


    public Task PutLicitacao(Licitacao licitacao){
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

        var parameters = new{
            licitacao.Valor,
            licitacao.EmailLicitador
        };

        // Retorna true, pois a operação foi bem-sucedida (não houve exceção)
        return _db.SaveData(sql, parameters);;
    }
}