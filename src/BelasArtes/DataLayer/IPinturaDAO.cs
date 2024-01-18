namespace DataLayer;
public interface IPinturaDAO{
   Task<List<Pintura>> FindAll();
   public Task<Pintura> GetPinturaById(int pinturaId);
   public Task<bool> PutPintura(Pintura pintura);
}
