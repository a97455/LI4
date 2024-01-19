/* using System.Drawing;
using Datalayer;

namespace DataLayer{
	public class BelasArtes {
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

		public async bool AutenticarUtilizador(string email, string palavraPasse) {
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

		public List<Leilao> Filtrar(string artista, int codido_tipo_leilao , List<int> movimentos_artistico){
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

		public List<Leilao> GetHistoricoCompras(string email) {
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
		public List<Leilao> GetHistoricoVendas(string email){
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
		public bool LicitarPintura(int cod_leilao,string mail_licitador, float valor){
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
			    Task<int> task = leiloes.ContaLeileosDeUmDadoTipo(3);
			    int count = task.Result;
			    return count;
			}catch{
				return 0;
			}
		}

		public Task<List<int>> NumeroLicitacoes() {
			return licitacoes.ContaLicitacoes();
		}
		
		public int NumeroUtilizadores() {
			try{
    			Task<int> task = utilizadores.ContaUtilizadores();
    			int count = task.Result;
    		return count;
			}catch{
				return 0;
			}
		}

		public List<Leilao> OrdenarLeiloesDecorrer(List<Leilao> leiloes,int codModoOrdenacao) {
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
			}else if(codModoOrdenacao==3){ //crescente do valor da ultima licitacao
				List<int> sortingList = new List<int>();
				foreach(Leilao leilao in leiloes){
					sortingList.Add(licitacoes.MaiorLicitacaoByLeilao(leilao.Id).Result);
				}
				List<Leilao> sortedLeiloes = leiloes
					.Select((leilao, index) => new { Leilao = leilao, valorOrdenacao = sortingList[index] })
					.OrderBy(item => item.valorOrdenacao)
					.Select(item => item.Leilao)
					.ToList();
				return sortedLeiloes;
			}else if(codModoOrdenacao==4){ //decrescente do valor da ultima licitacao
				List<int> sortingList = new List<int>();
				foreach(Leilao leilao in leiloes){
					sortingList.Add(licitacoes.MaiorLicitacaoByLeilao(leilao.Id).Result);
				}
				List<Leilao> sortedLeiloes = leiloes
					.Select((leilao, index) => new { Leilao = leilao, valorOrdenacao = sortingList[index] })
					.OrderByDescending(item => item.valorOrdenacao)
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
		public bool RegistarLeilao(int codPintura, DateTime dataInicio, DateTime dataFim, float precoInicial) {
			if (DateTime.Now>= dataInicio && DateTime.Now<= dataFim){
				Leilao leilao = new Leilao(null,dataInicio,dataFim,precoInicial,null,codPintura,2);
				return leiloes.PutLeilao(leilao).Result;		
			}else{
				Leilao leilao = new Leilao(null,dataInicio,dataFim,precoInicial,null,codPintura,1);
				return leiloes.PutLeilao(leilao).Result;		
			}
		}
		public bool RegistarPintura(string nome, float altura, float largura, float peso, 
		string descricao,Bitmap? foto,string artista, bool autenticidade, int anoCriacao, int codMovimentoArtistico) {
			Pintura pintura = new Pintura(null,nome,altura,largura,peso,descricao,foto,artista,anoCriacao,autenticidade,
			false,null,codMovimentoArtistico);
			return pinturas.PutPintura(pintura).Result;
		}
		public bool RegistarUtilizador(string nome, string rua, string codigoPostal, string cidade, 
		string localidade, string paisResidencia, string numeroIdentificacaoGovernamental, string email, 
		int numeroTelemovel, string iBAN, string palavraPasse) {
			Utilizador utilizador = new Utilizador(nome,numeroTelemovel,rua,localidade,cidade,codigoPostal,
			paisResidencia,iBAN,palavraPasse);
			return utilizadores.PutUtilizador(utilizador).Result;
		}
	}
} */