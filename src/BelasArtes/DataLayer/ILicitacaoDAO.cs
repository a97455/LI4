namespace DataLayer;
public interface ILicitacaoDAO{
   Task<List<Licitacao>> FindAll();
   public Task<Licitacao> GetLicitacaoById(int licitacaoId);
   public Task<bool> UpsertLicitacao(Licitacao licitacao);

}
