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

    public Task<List<Utilizador>> GetUtilizadorByEmail(string Email){
        string sql = "SELECT * FROM Utilizador WHERE Email = @Email";
        var parameters = new {Email};
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
            USING (VALUES (@Email)) AS source (Email)
            ON target.Email = source.Email
            WHEN MATCHED THEN
                UPDATE SET
                    Email = @Email,
                    Nome = @Nome,
                    Telefone = @Telefone,
                    Rua = @Rua,
                    Localidade = @Localidade,
                    Cidade = @Cidade,
                    CodigoPostal = @CodigoPostal,
                    PaisResidencia = @PaisResidencia,
                    NIG = @NIG,
                    IBAN = @IBAN,
                    PalavraPasse = @PalavraPasse
            WHEN NOT MATCHED THEN
                INSERT (Email, Nome, Telefone, Rua, Localidade, Cidade, CodigoPostal, PaisResidencia, NIG, IBAN, PalavraPasse)
                VALUES (@Email, @Nome, @Telefone, @Rua, @Localidade, @Cidade, @CodigoPostal, @PaisResidencia, @NIG, @IBAN, @PalavraPasse);";

        var parameters = new {
            utilizador.Email,
            utilizador.Nome,
            utilizador.Telefone,
            utilizador.Rua,
            utilizador.Localidade,
            utilizador.Cidade,
            utilizador.CodigoPostal,
            utilizador.PaisResidencia,
            utilizador.NIG,
            utilizador.IBAN,
            utilizador.PalavraPasse
        };

        // Retorna true, pois a operação foi bem-sucedida (não houve exceção)
        return _db.SaveData(sql, parameters);
    }
}