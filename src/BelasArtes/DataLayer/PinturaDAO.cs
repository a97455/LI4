namespace DataLayer;
public class PinturaDAO : IPinturaDAO{
    private ISqlDataAccess _db;
    public PinturaDAO(ISqlDataAccess db){
        _db = db;
    }
    public Task<List<Pintura>> FindAll()
    {
        string sql = "select * from Pintura";
        return _db.LoadData<Pintura, dynamic>(sql, new { });
    }

    public Task<List<MovimentoArtistico>> FindAllMovimentosArtisticos(){
        string sql = "select * from Movimento_Artistico";
        return _db.LoadData<MovimentoArtistico, dynamic>(sql, new { });
    }

    public Task<List<Pintura>> GetPinturaById(int? Id){
        string sql = "SELECT * FROM Pintura WHERE Id = @Id";
        var parameters = new {Id};
        return _db.LoadData<Pintura, dynamic>(sql, parameters);
    }

    public Task PutPintura(Pintura pintura){
        string sql = @"
            MERGE INTO Pintura AS target
            USING (VALUES (@Id)) AS source (Id)
            ON target.Id = source.Id
            WHEN MATCHED THEN
                UPDATE SET
                    Nome = @Nome,
                    Altura = @Altura,
                    Largura = @Largura,
                    Peso = @Peso,
                    Descricao = @Descricao,
                    Foto = @Foto,
                    Artista = @Artista,
                    AnoCriacao = @AnoCriacao,
                    Original = @Original,
                    VerificacaoAutenticidade = @VerificacaoAutenticidade,
                    EmailVendedor = @EmailVendedor,
                    CodMovimentoArtistico = @CodMovimentoArtistico
            WHEN NOT MATCHED THEN
                INSERT (Nome, Altura, Largura, Peso, Descricao, Foto, Artista, AnoCriacao, Original, VerificacaoAutenticidade, EmailVendedor, CodMovimentoArtistico)
                VALUES (@Nome, @Altura, @Largura, @Peso, @Descricao, @Foto, @Artista, @AnoCriacao, @Original, @VerificacaoAutenticidade, @EmailVendedor, @CodMovimentoArtistico);";

        var parameters = new{
            pintura.Id,
            pintura.Nome,
            pintura.Altura,
            pintura.Largura,
            pintura.Peso,
            pintura.Descricao,
            pintura.Foto,
            pintura.Artista,
            pintura.AnoCriacao,
            pintura.Original,
            pintura.VerificacaoAutenticidade,
            pintura.EmailVendedor,
            pintura.CodMovimentoArtistico
        };
        
        // Retorna true, pois a operação foi bem-sucedida (não houve exceção)
        return _db.SaveData(sql, parameters);
    }

    public async Task<int> GetMaxPinturaId(){
        string sql = "SELECT MAX(Id) FROM Pintura";
        var result = await _db.ExecuteScalar<int, dynamic>(sql, new { });
        return result;
    }

}