namespace DataLayer;
public interface IPinturaDAO{
   Task<List<Pintura>> FindAll();
}
