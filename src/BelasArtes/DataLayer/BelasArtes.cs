using System.Drawing;
using Datalayer;

namespace DataLayer{
	public class BelasArtes : IBelasArtes  {
		public ILeilaoDAO leiloes;
		public IUtilizadorDAO utilizadores;
		public IPinturaDAO pinturas;
		public ILicitacaoDAO licitacao;
		public ISqlDataAccess _db;

		public BelasArtes(ISqlDataAccess db)
		{
			leiloes= new LeilaoDAO(db);
			utilizadores= new UtilizadorDAO(db);
			pinturas= new PinturaDAO(db);
			licitacao= new LicitacaoDAO(db);
			_db = db;
		}

		public bool AutenticarUtilizador(ref string email, ref string palavraPasse) {
		    try {
		        Task<Utilizador> task = utilizadores.GetUtilizadorByEmail(email);
		        Utilizador utilizador = task.Result;
				if (utilizador.PalavraPasse.Equals(palavraPasse)){
					return true;
				}else{
					return false;
				}
		    } catch (Exception ex) {
		        Console.WriteLine($"An error occurred: {ex.Message}");
		        return false;
		    }
		}

		public List<Leilao> Filtrar(ref string artista, ref List<int> movimentos_artistico){
		    try {
		        Task<List<Leilao>> task1 = leiloes.FindAll();
		        List<Leilao> leiloesList = task1.Result;
				
				foreach (Leilao leilao in leiloesList) {
					Task<Pintura> task2 = pinturas.GetPinturaById(leilao.CodPintura);
					Pintura pintura = task2.Result;

					bool cumpre_parametro_filtragem = false;
					if(pintura.Artista.Equals(artista)){
						cumpre_parametro_filtragem=true;
					}else{
						foreach(int movimento_artistico in movimentos_artistico){
							if(movimento_artistico == pintura.CodMovimentoArtistico){
								cumpre_parametro_filtragem=true;
							}
						}
					}

					if (!cumpre_parametro_filtragem){
						leiloesList.Remove(leilao);
					}
				}
		        return leiloesList;
		    } catch (Exception ex) {
		        Console.WriteLine($"An error occurred: {ex.Message}");
		        return new List<Leilao>();
		    }
		}

		public List<Leilao> GetHistoricoCompras(ref string email) {
			throw new NotImplementedException("Not implemented");
		}
		public List<Leilao> GetHistoricoVendas(ref string email) {
			throw new NotImplementedException("Not implemented");
		}
		public bool LicitarPintura(ref int codUtilizador, ref float valor) {
			throw new NotImplementedException("Not implemented");
		}
		public int NumeroLeiloesDecorrer() {
			throw new NotImplementedException("Not implemented");
		}
		public int NumeroLeiloesTerminados() {
			throw new NotImplementedException("Not implemented");
		}
		public int NumeroLicitacoes() {
			throw new NotImplementedException("Not implemented");
		}
		public int NumeroUtilizadores() {
			throw new NotImplementedException("Not implemented");
		}
		public List<Leilao> OrdenarLeiloesDecorrer(ref int codModoOrdenacao) {
			throw new NotImplementedException("Not implemented");
		}
		public bool RegistarLeilao(ref int codPintura, ref DateTime dataInicio, ref DateTime dataFim, ref float precoInicial) {
			if (DateTime.Now>= dataInicio && DateTime.Now<= dataFim){
				Leilao leilao = new Leilao(null,dataInicio,dataFim,precoInicial,null,codPintura,2);
				return leiloes.PutLeilao(leilao).Result;		
			}else{
				Leilao leilao = new Leilao(null,dataInicio,dataFim,precoInicial,null,codPintura,1);
				return leiloes.PutLeilao(leilao).Result;		
			}
		}
		public bool RegistarPintura(ref string nome, ref float altura, ref float largura, ref float peso, 
		ref string descricao,ref Bitmap? foto,ref string artista, ref bool autenticidade, ref int anoCriacao, ref int codMovimentoArtistico) {
			Pintura pintura = new Pintura(null,nome,altura,largura,peso,descricao,foto,artista,anoCriacao,autenticidade,
			false,null,codMovimentoArtistico);
			return pinturas.PutPintura(pintura).Result;
		}
		public bool RegistarUtilizador(ref string nome, ref string rua, ref string codigoPostal, ref string cidade, 
		ref string localidade, ref string paisResidencia, ref string numeroIdentificacaoGovernamental, ref string email, 
		ref int numeroTelemovel, ref string iBAN, ref string palavraPasse) {
			Utilizador utilizador = new Utilizador(nome,numeroTelemovel,rua,localidade,cidade,codigoPostal,
			paisResidencia,iBAN,palavraPasse);
			return utilizadores.PutUtilizador(utilizador).Result;
		}
	}
}