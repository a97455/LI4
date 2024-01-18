using System.Drawing;
using Datalayer;

namespace DataLayer{
	public class BelasArtes : IBelasArtes  {
		public ILeilaoDAO leiloes;
		public IUtilizadorDAO utilizadores;
		public IPinturaDAO pinturas;
		public ILicitacaoDAO licitacoes;
		public ISqlDataAccess _db;

		public BelasArtes(ISqlDataAccess db)
		{
			leiloes= new LeilaoDAO(db);
			utilizadores= new UtilizadorDAO(db);
			pinturas= new PinturaDAO(db);
			licitacoes= new LicitacaoDAO(db);
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

		public List<Leilao> Filtrar(ref string artista, ref int codido_tipo_leilao ,ref List<int> movimentos_artistico){
		    try {
		        Task<List<Leilao>> task1 = leiloes.FindAll();
		        List<Leilao> leiloesList = task1.Result;
				
				foreach (Leilao leilao in leiloesList) {
					Task<Pintura> task2 = pinturas.GetPinturaById(leilao.CodPintura);
					Pintura pintura = task2.Result;

					bool cumpre_parametro_filtragem = false;
					if(leilao.CodEstado==codido_tipo_leilao){
						if(pintura.Artista.Equals(artista)){
							cumpre_parametro_filtragem=true;
						}else{
							foreach(int movimento_artistico in movimentos_artistico){
								if(movimento_artistico == pintura.CodMovimentoArtistico){
									cumpre_parametro_filtragem=true;
								}
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
			try {
			    Task<List<Leilao>> task1 = leiloes.FindAll();
			    List<Leilao> leiloesList = task1.Result;

				foreach (Leilao leilao in leiloesList) {
					if(leilao.EmailComprador==null || leilao.CodEstado!=3 || !leilao.EmailComprador.Equals(email)){
						leiloesList.Remove(leilao);
					}
				}
			    return leiloesList;
			} catch (Exception ex) {
			    Console.WriteLine($"An error occurred: {ex.Message}");
			    return new List<Leilao>();
			}
		}
		public List<Leilao> GetHistoricoVendas(ref string email){
			try {
			    Task<List<Leilao>> task1 = leiloes.FindAll();
			    List<Leilao> leiloesList = task1.Result;

				foreach (Leilao leilao in leiloesList){
					Task<Pintura> task2 = pinturas.GetPinturaById(leilao.CodPintura);
					Pintura pintura = task2.Result;
					if(pintura.EmailVendedor==null || leilao.CodEstado!=3 || !pintura.EmailVendedor.Equals(email)){
						leiloesList.Remove(leilao);
					}
				}
			    return leiloesList;
			} catch (Exception ex) {
			    Console.WriteLine($"An error occurred: {ex.Message}");
			    return new List<Leilao>();
			}
		}
		public bool LicitarPintura(ref int cod_leilao,ref string mail_licitador, ref float valor){
			try{
			   	Task<Leilao> task1 = leiloes.GetLeilaoById(cod_leilao);
			   	Leilao leilao = task1.Result;

				if(leilao.CodEstado==2){
					Task<int> task2 = licitacoes.MaiorLicitacaoByLeilao(cod_leilao);
					int? maximoLicitacao = task2.Result;

					if (valor!=0){
						if(valor>=maximoLicitacao + maximoLicitacao*0.05){
							Licitacao nova_licitacao = new Licitacao(null,valor,mail_licitador);
							licitacoes.PutLicitacao(nova_licitacao);
							return true;
						}
					}else{
						if(valor>=leilao.PrecoInicial){
							Licitacao nova_licitacao = new Licitacao(null,valor,mail_licitador);
							licitacoes.PutLicitacao(nova_licitacao);
							return true;
						}
					}
				}
				
				return false;
			}catch (Exception ex){
			   Console.WriteLine($"An error occurred: {ex.Message}");
			   return false;
			}
		}
		public int NumeroLeiloesDecorrer() {
		    try {
		        Task<int> task = leiloes.ContaLeileosDeUmDadoTipo(2);
		        int count = task.Result;
		        return count;
		    } catch{
				return 0;
		    }
		}
		public int NumeroLeiloesTerminados() {
			try{
			    Task<int> task = leiloes.ContaLeileosDeUmDadoTipo(2);
			    int count = task.Result;
			    return count;
			}catch{
				return 0;
			}
		}
		
		public int NumeroLicitacoes() {
			throw new NotImplementedException("Not implemented");
		}
		public int NumeroUtilizadores() {
			throw new NotImplementedException("Not implemented");
		}
		public List<Leilao> OrdenarLeiloesDecorrer(List<Leilao> leiloes,ref int codModoOrdenacao) {
			if (codModoOrdenacao==1){ //crescente de tempo restante
				List<(DateTime,int)> sortingList = new List<(DateTime,int)>();
				foreach(Leilao leilao in leiloes){
					DateTime dataFim = this.leiloes.GetLeilaoById(leilao.Id).Result.DataFim;
					sortingList.Add((dataFim,licitacoes.NumbLicitacoesByLeilao(leilao.Id).Result));
				}
				List<Leilao> sortedLeiloes = leiloes
					.Select((leilao, index) => new { Leilao = leilao, SortingTuple = sortingList[index] })
					.OrderBy(item => item.SortingTuple.Item1)
					.ThenBy(item => item.SortingTuple.Item2)
					.Select(item => item.Leilao)
					.ToList();
				return sortedLeiloes;
			}else if (codModoOrdenacao==2){ //decrescente de tempo restante
				List<(DateTime,int)> sortingList = new List<(DateTime,int)>();
				foreach(Leilao leilao in leiloes){
					DateTime dataFim = this.leiloes.GetLeilaoById(leilao.Id).Result.DataFim;
					sortingList.Add((dataFim,licitacoes.NumbLicitacoesByLeilao(leilao.Id).Result));
				}
				List<Leilao> sortedLeiloes = leiloes
					.Select((leilao, index) => new { Leilao = leilao, SortingTuple = sortingList[index] })
					.OrderByDescending(item => item.SortingTuple.Item1)
					.ThenBy(item => item.SortingTuple.Item2)
					.Select(item => item.Leilao)
					.ToList();
				return sortedLeiloes;
			}else{ //default
				List<(int,DateTime)> sortingList = new List<(int,DateTime)>();
				foreach(Leilao leilao in leiloes){
					DateTime dataFim = this.leiloes.GetLeilaoById(leilao.Id).Result.DataFim;
					sortingList.Add((licitacoes.NumbLicitacoesByLeilao(leilao.Id).Result,dataFim));
				}
				List<Leilao> sortedLeiloes = leiloes
					.Select((leilao, index) => new { Leilao = leilao, SortingTuple = sortingList[index] })
					.OrderByDescending(item => item.SortingTuple.Item1)
					.ThenBy(item => item.SortingTuple.Item2)
					.Select(item => item.Leilao)
					.ToList();
				return sortedLeiloes;
			}
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