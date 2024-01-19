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

    public Task<List<Utilizador>> GetUtilizadorByEmail(String utilizadoremail){
        string sql = "SELECT * FROM Utilizador WHERE Id = @Id";
        var parameters = new { Id = utilizadoremail };
        return _db.LoadData<Utilizador, dynamic>(sql, parameters);
    }

    public Task<List<int>> ContaUtilizadores(){
        string sql = "SELECT COUNT(*) FROM Utilizador";
        var parameters = new { };
        return _db.LoadData<int, dynamic>(sql, parameters);
    }

    public Task PutUtilizador(Utilizador utilizador){
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

        var parameters = new {
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

        // Retorna true, pois a operação foi bem-sucedida (não houve exceção)
        return _db.SaveData(sql, parameters);
    }
}