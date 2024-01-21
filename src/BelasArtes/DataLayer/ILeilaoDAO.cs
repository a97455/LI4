namespace DataLayer;
public interface ILeilaoDAO{
   Task<List<Leilao>> FindAll();
   public Task<List<Leilao>> LeiloesByEstado(int codEstado);
   public Task<List<Leilao>> GetLeilaoById(int? Id);
   public Task PutLeilao(Leilao leilao);
   public Task<List<int>> ContaLeiloesDeUmDadoTipo(int codEstado);
}
