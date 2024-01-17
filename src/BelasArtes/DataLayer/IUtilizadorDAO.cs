using Datalayer;

namespace DataLayer;
public interface IUtilizadorDAO{
   Task<List<Utilizador>> FindAll();
}