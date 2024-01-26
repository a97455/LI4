namespace DataLayer;
public class LicitacaoDAO : ILicitacaoDAO{
    private ISqlDataAccess _db;
    public LicitacaoDAO(ISqlDataAccess db){
        _db = db;
    }

    public Task<List<Licitacao>> FindAll(){
        string sql = "select * from Licitacao";
        return _db.LoadData<Licitacao, dynamic>(sql, new { });
    }

    public Task<List<Licitacao>> GetLicitacaoById(int? Id){
        string sql = "SELECT * FROM Licitacao WHERE Id = @Id";
        var parameters = new { Id = Id };
        return _db.LoadData<Licitacao, dynamic>(sql, parameters);
    }
    
    public Task<List<Licitacao>> LicitacoesDoLeilaoOrdenadasPorValor(int? IdLeilao){
        string sql = "SELECT * FROM Licitacao WHERE IdLeilao = @IdLeilao Order BY Valor DESC";
        var parameters = new {IdLeilao};
        return _db.LoadData<Licitacao, dynamic>(sql, parameters);
    }   

    public Task<List<int>> NumbLicitacoesByLeilao(int? IdLeilao){
        string sql = "SELECT COUNT(*) FROM Licitacao WHERE IdLeilao = @IdLeilao";
        var parameters = new {IdLeilao};
        return _db.LoadData<int, dynamic>(sql, parameters);
    }

    public Task<List<int>> MaiorLicitacaoByLeilao(int? IdLeilao) {
        string sql = "SELECT COALESCE(MAX(Valor), 0) FROM Licitacao WHERE IdLeilao = @IdLeilao";
        var parameters = new {IdLeilao};
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
            USING (VALUES (@Id)) AS source (Id)
            ON target.Id = source.Id
            WHEN MATCHED THEN
                UPDATE SET
                    Valor = @Valor,
                    EmailLicitador = @EmailLicitador,
                    Data = @Data,
                    IdLeilao = @IdLeilao
            WHEN NOT MATCHED THEN
                INSERT (Valor, EmailLicitador, Data, IdLeilao)
                VALUES (@Valor, @EmailLicitador, @Data, @IdLeilao);";

        var parameters = new{
            licitacao.Id, 
            licitacao.Valor,
            licitacao.EmailLicitador,
            licitacao.Data,
            licitacao.IdLeilao
        };

        // Retorna true, pois a operação foi bem-sucedida (não houve exceção)
        return _db.SaveData(sql, parameters);
    }
}