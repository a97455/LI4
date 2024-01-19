namespace DataLayer;
public interface ILeilaoDAO{
   Task<List<Leilao>> FindAll();
   public Task<List<Leilao>> GetLeilaoById(int? leilaoId);
   public Task PutLeilao(Leilao leilao);
   public Task<List<int>> ContaLeiloesDeUmDadoTipo(int tipo_leilao);
}
