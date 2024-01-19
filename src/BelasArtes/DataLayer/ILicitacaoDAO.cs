namespace DataLayer;
public interface ILicitacaoDAO{
   Task<List<Licitacao>> FindAll();
   public Task<List<Licitacao>> GetLicitacaoById(int? licitacaoId);
   public Task<List<int>> NumbLicitacoesByLeilao(int? idLeilao);
   public Task PutLicitacao(Licitacao licitacao);
   public Task<List<int>> MaiorLicitacaoByLeilao(int? idLeilao);
    public Task<List<int>> ContaLicitacoes();
}