namespace DataLayer;
public interface ILeilaoDAO{
   Task<List<Leilao>> FindAll();
}
