namespace DataLayer;
public class LeilaoDAO : ILeilaoDAO{
    private ISqlDataAccess _db;
    public LeilaoDAO(ISqlDataAccess db){
        _db = db;
    }

    public Task<List<Leilao>> FindAll()
    {
        string sql = "select * from Leilao";
        return _db.LoadData<Leilao, dynamic>(sql, new { });
    }
    
    public Task<List<Leilao>> GetLeilaoById(int? Id)
    {
        string sql = "select * from Leilao where Id = @Id";
        var parameters = new {Id};
        return _db.LoadData<Leilao, dynamic>(sql, parameters);
    }

    public Task PutLeilao(Leilao leilao){
        string mergeSql = @"
            MERGE INTO Leilao AS target
            USING (VALUES (DEFAULT)) AS source (id)
            ON target.id = source.id
            WHEN MATCHED THEN
                UPDATE SET 
                    target.DataInicio = @DataInicio,
                    target.DataFim = @DataFim,
                    target.PrecoFinal = @PrecoFinal,
                    target.EmailComprador = @EmailComprador,
                    target.CodPintura = @CodPintura,
                    target.CodEstado = @CodEstado
            WHEN NOT MATCHED THEN
                INSERT (DataInicio, DataFim, PrecoFinal, EmailComprador, CodPintura, CodEstado)
                VALUES (@DataInicio, @DataFim, @PrecoFinal, @EmailComprador, @CodPintura, @CodEstado);";

        var parameters = new{
            leilao.DataInicio,
            leilao.DataFim,
            leilao.PrecoInicial,
            leilao.EmailComprador,
            leilao.CodPintura,
            leilao.CodEstado
        };

        // If rowsAffected is greater than 0, the operation was successful
        return _db.SaveData(mergeSql, parameters);
    }

    public Task<List<int>> ContaLeiloesDeUmDadoTipo(int CodEstado){
        string sql = "SELECT COUNT(*) FROM Leilao WHERE CodEstado = @CodEstado";

        // Fornecendo um objeto vazio como parâmetros, já que a consulta não tem parâmetros
        var parameters = new {CodEstado};

        // Obtemos o primeiro item da lista ou zero se a lista estiver vazia
        return _db.LoadData<int, dynamic>(sql, parameters);
    }
}