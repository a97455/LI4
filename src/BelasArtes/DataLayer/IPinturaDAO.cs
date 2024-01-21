namespace DataLayer;
public interface IPinturaDAO{
   Task<List<Pintura>> FindAll();
   public Task<List<Pintura>> GetPinturaById(int? Id);
   public Task PutPintura(Pintura pintura);
}
