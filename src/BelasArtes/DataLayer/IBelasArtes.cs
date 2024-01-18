namespace DataLayer{
	public interface IBelasArtes {
	bool AutenticarUtilizador(ref string email, ref string palavraPasse);
	List<Leilao> FiltrarArtista(ref string artista);
	List<Leilao> GetHistoricoCompras(ref string email);
	List<Leilao> GetHistoricoVendas(ref string email);
	bool LicitarPintura(ref int codUtilizador, ref float valor);
	int NumeroLeiloesDecorrer();
	int NumeroLeiloesTerminados();
	int NumeroLicitacoes();
	int NumeroUtilizadores();
	List<Leilao> OrdenarLeiloesDecorrer(ref int codModoOrdenacao);
	bool RegistarLeilao(ref int codPintura, ref DateTime dataInicio, ref DateTime dataFim, ref float precoInicial);
	bool RegistarPintura(ref string nome, ref float altura, ref float largura, ref float peso, ref string descricao,
	    ref string artista, ref bool autenticidade, ref int anoCriacao, ref int codMovimentoArtistico);
    bool RegistarUtilizador(ref string nome, ref string rua, ref string codigoPostal, ref string cidade, ref string localidade,
    	ref string paisResidencia, ref string numeroIdentificacaoGovernamental, ref string email,
	    ref int numeroTelemovel, ref string iBAN, ref string palavraPasse);
	}
}