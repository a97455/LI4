using Datalayer;

namespace DataLayer;
public interface IUtilizadorDAO{
   Task<List<Utilizador>> FindAll();
   public Task<Utilizador> GetUtilizadorByEmail(String utilizadoremail);
   public Task<bool> PutUtilizador(Utilizador utilizador);
}