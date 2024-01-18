using Datalayer;

namespace DataLayer;
public interface IUtilizadorDAO{
   Task<List<Utilizador>> FindAll();
   public Task<Pintura> GetUtilizadorById(int utilizadorId);
   public Task<bool> UpsertUtilizador(Utilizador utilizador);
}