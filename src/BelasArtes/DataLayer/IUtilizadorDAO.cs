using Datalayer;

namespace DataLayer;
public interface IUtilizadorDAO{
   Task<List<Utilizador>> FindAll();
   public Task<List<Utilizador>> GetUtilizadorByEmail(string Email);
    public Task PutUtilizador(Utilizador utilizador);
    public Task<List<int>> ContaUtilizadores();
}