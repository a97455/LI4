namespace DataLayer;
public interface ILeilaoDAO{
   Task<List<Leilao>> FindAll();
   public Task<Leilao> GetLeilaoById(int? leilaoId);
   public Task<bool> PutLeilao(Leilao leilao);
   public Task<int> ContaLeileosDeUmDadoTipo(int tipo_leilao);
}
