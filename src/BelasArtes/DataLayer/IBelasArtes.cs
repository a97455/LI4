using System.Drawing;

namespace DataLayer{
	public interface IBelasArtes {
	bool AutenticarUtilizador(ref string email, ref string palavraPasse);
	public List<Leilao> Filtrar(ref string artista, ref int codido_tipo_leilao ,ref List<int> movimentos_artistico);
	List<Leilao> GetHistoricoCompras(ref string email);
	List<Leilao> GetHistoricoVendas(ref string email);
	public bool LicitarPintura(ref int cod_leilao,ref string mail_licitador, ref float valor);
	int NumeroLeiloesDecorrer();
	int NumeroLeiloesTerminados();
	int NumeroLicitacoes();
	int NumeroUtilizadores();
	List<Leilao> OrdenarLeiloesDecorrer(List<Leilao> leiloes,ref int codModoOrdenacao);
	bool RegistarLeilao(ref int codPintura, ref DateTime dataInicio, ref DateTime dataFim, ref float precoInicial);
	bool RegistarPintura(ref string nome, ref float altura, ref float largura, ref float peso, ref string descricao, 
		ref Bitmap? foto,ref string artista, ref bool autenticidade, ref int anoCriacao, ref int codMovimentoArtistico);
    bool RegistarUtilizador(ref string nome, ref string rua, ref string codigoPostal, ref string cidade, ref string localidade,
    	ref string paisResidencia, ref string numeroIdentificacaoGovernamental, ref string email,
	    ref int numeroTelemovel, ref string iBAN, ref string palavraPasse);
	}
}