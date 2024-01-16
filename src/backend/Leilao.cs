using System;
using Utilizador;

namespace Leilao{
	public class Leilao {
		private int id;
		private DateTime dataInicio;
		private DateTime dataFim;
		private float precoInicial;
		private String emailComprador;
		private int codPintura;
		private int codEstado;

		private LicitacaoDAO licitacoes;
		private UtilizadorDAO utilizadores;
	}
}