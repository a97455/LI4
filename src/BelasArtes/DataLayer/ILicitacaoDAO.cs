namespace DataLayer;
public interface ILicitacaoDAO{
   Task<List<Licitacao>> FindAll();
   public Task<List<Licitacao>> GetLicitacaoById(int? Id);
   public Task<List<int>> NumbLicitacoesByLeilao(int? IdLeilao);
   public Task PutLicitacao(Licitacao licitacao);
   public Task<List<int>> MaiorLicitacaoByLeilao(int? IdLeilao);
    public Task<List<int>> ContaLicitacoes();
    public Task<List<Licitacao>> LicitacoesDoLeilaoOrdenadasPorValor(int? IdLeilao);
}