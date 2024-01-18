using Datalayer;

namespace DataLayer;
public class UtilizadorDAO : IUtilizadorDAO
{
    private ISqlDataAccess _db;
    public UtilizadorDAO(ISqlDataAccess db)
    {
        _db = db;
    }    
    
    public Task<List<Utilizador>> FindAll()
    {
        string sql = "select * from Utilizador";
        return _db.LoadData<Utilizador, dynamic>(sql, new { });
    }

    public async Task<Utilizador> GetUtilizadorByEmail(String utilizadoremail){
        string sql = "SELECT * FROM Utilizador WHERE Id = @Id";
        var parameters = new { Id = utilizadoremail };
        List<Utilizador> utilizadorList = await _db.LoadData<Utilizador, dynamic>(sql, parameters);
        return utilizadorList.FirstOrDefault()!;
    }

    public async Task<bool> PutUtilizador(Utilizador utilizador){
        string sql = @"
            MERGE INTO Utilizador AS target
            USING (VALUES (@UtilizadorId)) AS source (UtilizadorId)
            ON target.UtilizadorId = source.UtilizadorId
            WHEN MATCHED THEN
                UPDATE SET
                    Email = @Email,
                    Telefone = @Telefone,
                    Rua = @Rua,
                    Localidade = @Localidade,
                    Cidade = @Cidade,
                    CodigoPostal = @CodigoPostal,
                    PaisResidencia = @PaisResidencia,
                    IBAN = @IBAN,
                    PalavraPasse = @PalavraPasse
            WHEN NOT MATCHED THEN
                INSERT (UtilizadorId, Email, Telefone, Rua, Localidade, Cidade, CodigoPostal, PaisResidencia, IBAN, PalavraPasse)
                VALUES (@UtilizadorId, @Email, @Telefone, @Rua, @Localidade, @Cidade, @CodigoPostal, @PaisResidencia, @IBAN, @PalavraPasse);";

        var parameters = new
        {
            utilizador.Email,
            utilizador.Telefone,
            utilizador.Rua,
            utilizador.Localidade,
            utilizador.Cidade,
            utilizador.CodigoPostal,
            utilizador.PaisResidencia,
            utilizador.IBAN,
            utilizador.PalavraPasse
        };

        await _db.SaveData(sql, parameters);

        // Retorna true, pois a operação foi bem-sucedida (não houve exceção)
        return true;
    }
}