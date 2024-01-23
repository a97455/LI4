namespace DataLayer;
public interface IPinturaDAO{
   Task<List<Pintura>> FindAll();
   public Task<List<MovimentoArtistico>> FindAllMovimentosArtisticos();
   public Task<List<Pintura>> GetPinturaById(int? Id);
   public Task<int> GetMaxPinturaId();
   public Task PutPintura(Pintura pintura);
}
