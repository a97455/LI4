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

    public Task<List<Leilao>> LeiloesByEstado(int CodEstado){
        string sql = "select * from Leilao where CodEstado=@codEstado";
        var parameters = new {CodEstado};
        return _db.LoadData<Leilao, dynamic>(sql,parameters);
    }

    public Task<List<Leilao>> LeiloesAcabadosQueUserComprou(string emailComprador){
        string sql = "SELECT * FROM Leilao WHERE EmailComprador = @emailComprador AND CodEstado = 3";
        var parameters = new { emailComprador };
        return _db.LoadData<Leilao, dynamic>(sql, parameters);
    }


    public Task<List<Leilao>> LeiloesAcabadosQueUserVendeu(string emailVendedor){
        string sql = "SELECT * FROM Leilao " +
             "INNER JOIN Pintura ON Leilao.CodPintura = Pintura.Id " +
             "WHERE Leilao.CodEstado = 3 AND Pintura.EmailVendedor = @emailVendedor";
        var parameters = new {emailVendedor};
        return _db.LoadData<Leilao, dynamic>(sql,parameters);
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
            USING (VALUES (@Id)) AS source (id)
            ON target.id = source.id
            WHEN MATCHED THEN
                UPDATE SET 
                    target.DataInicio = @DataInicio,
                    target.DataFim = @DataFim,
                    target.PrecoInicial = @PrecoInicial,
                    target.EmailComprador = @EmailComprador,
                    target.CodPintura = @CodPintura,
                    target.CodEstado = @CodEstado
            WHEN NOT MATCHED THEN
                INSERT (DataInicio, DataFim, PrecoInicial, EmailComprador, CodPintura, CodEstado)
                VALUES (@DataInicio, @DataFim, @PrecoInicial, @EmailComprador, @CodPintura, @CodEstado);";

        var parameters = new{
            leilao.Id,
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

    public async Task<int> GetMaxLeilaoId(){
        string sql = "SELECT MAX(Id) FROM Leilao";
        var result = await _db.ExecuteScalar<int, dynamic>(sql, new { });
        return result;
    }
}