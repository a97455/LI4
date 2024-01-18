namespace DataLayer;
public interface ILicitacaoDAO{
   Task<List<Licitacao>> FindAll();
   public Task<Licitacao> GetLicitacaoById(int? licitacaoId);
   public Task<int> NumbLicitacoesByLeilao(int? idLeilao);
   public Task<bool> PutLicitacao(Licitacao licitacao);
   public Task<int> MaiorLicitacaoByLeilao(int? idLeilao);
    public Task<int> ContaLicitacoes();
}