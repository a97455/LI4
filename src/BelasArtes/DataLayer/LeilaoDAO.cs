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
    
    public async Task<Leilao> GetLeilaoById(int leilaoId)
    {
        string sql = "select * from Leilao where Id = @LeilaoId";
        var parameters = new { LeilaoId = leilaoId };

        List<Leilao> leilaoList = await _db.LoadData<Leilao, dynamic>(sql, parameters);
        return leilaoList.FirstOrDefault()!;
    }

    public async Task<bool> PutLeilao(Leilao leilao){
        string mergeSql = @"
            MERGE INTO Leilao AS target
            USING (VALUES (DEFAULT)) AS source (id)
            ON target.id = source.id
            WHEN MATCHED THEN
                UPDATE SET 
                    target.data_inicio = @data_inicio,
                    target.data_fim = @data_fim,
                    target.preco_inicial = @preco_inicial,
                    target.comprador_email = @comprador_email,
                    target.pintura_id = @pintura_id,
                    target.id_estado = @id_estado
            WHEN NOT MATCHED THEN
                INSERT (data_inicio, data_fim, preco_inicial, comprador_email, pintura_id, id_estado)
                VALUES (@data_inicio, @data_fim, @preco_inicial, @comprador_email, @pintura_id, @id_estado);";

        var parameters = new{
            leilao.DataInicio,
            leilao.DataFim,
            leilao.PrecoInicial,
            leilao.EmailComprador,
            leilao.CodPintura,
            leilao.CodEstado
        };

        await _db.SaveData(mergeSql, parameters);

        // If rowsAffected is greater than 0, the operation was successful
        return true;
    }
}