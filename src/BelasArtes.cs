using System;

public class BelasArtes : IBelasArtes  {
	private LeilaoDAO leiloes;
	private String estadosLeilao;
	private String movimentosArtisticos;
	private UtilizadorDAO utilizadores;
	private PinturaDAO pinturas;
	
	
	public bool AutenticarUtilizador(ref string email, ref string palavraPasse) {
		throw new System.NotImplementedException("Not implemented");
	}
	public List<Leilao> FiltrarArtista(ref string artista) {
		throw new System.NotImplementedException("Not implemented");
	}
	public List<Leilao> GetHistoricoCompras(ref string email) {
		throw new System.NotImplementedException("Not implemented");
	}
	public List<Leilao> GetHistoricoVendas(ref string email) {
		throw new System.NotImplementedException("Not implemented");
	}
	public bool LicitarPintura(ref int codUtilizador, ref float valor) {
		throw new System.NotImplementedException("Not implemented");
	}
	public int NumeroLeiloesDecorrer() {
		throw new System.NotImplementedException("Not implemented");
	}
	public int NumeroLeiloesTerminados() {
		throw new System.NotImplementedException("Not implemented");
	}
	public int NumeroLicitacoes() {
		throw new System.NotImplementedException("Not implemented");
	}
	public int NumeroUtilizadores() {
		throw new System.NotImplementedException("Not implemented");
	}
	public List<Leilao> OrdenarLeiloesDecorrer(ref int codModoOrdenacao) {
		throw new System.NotImplementedException("Not implemented");
	}
	public bool RegistarLeilao(ref int codPintura, ref Date dataInicio, ref Date dataFim, ref float precoInicial) {
		throw new System.NotImplementedException("Not implemented");
	}
	public bool RegistarPintura(ref string nome, ref float altura, ref float largura, ref float peso, ref string descricao, ref string: artista, ref bool autenticidade, ref Year anoCriacao, ref int codMovimentoArtistico) {
		throw new System.NotImplementedException("Not implemented");
	}
	public bool RegistarUtilizador(ref string nome, ref string rua, ref string codigoPostal, ref string cidade, ref string localidade, ref string paisResidencia, ref string numeroIdentificacaoGovernamental, ref string email, ref int numeroTelemovel, ref string iBAN, ref string palavraPasse) {
		throw new System.NotImplementedException("Not implemented");
	}
}
