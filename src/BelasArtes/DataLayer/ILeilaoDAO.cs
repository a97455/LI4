namespace DataLayer;
public interface ILeilaoDAO{
   Task<List<Leilao>> FindAll();
   public Task<Leilao?> GetById(int leilaoId);
   public Task<bool> PutLeilao(Leilao leilao);
}
