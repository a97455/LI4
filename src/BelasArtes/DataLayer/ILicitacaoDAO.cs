namespace DataLayer;
public interface ILicitacaoDAO{
   Task<List<Licitacao>> FindAll();
   public Task<Licitacao> GetLicitacaoById(int licitacaoId);
   public Task<bool> PutLicitacao(Licitacao licitacao);

   public Task<int?> MaiorLicitacaoByLeilao(int idLeilao);

}
