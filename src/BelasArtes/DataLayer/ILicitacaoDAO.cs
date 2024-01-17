namespace DataLayer;
public interface ILicitacaoDAO{
   Task<List<Licitacao>> FindAll();
}
