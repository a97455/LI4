using DataLayer;

public class AppState{
    //Atributos gerais ao site
    public string atual_user {get;set;} = "";
    public int? atual_leilao {get; set;} = null;


    //Atributos para a página com os diversos leilões (ativos e agendados)
    public List<Leilao> ListLeiloesAtivosMostrar{ get; set;} = new List<Leilao>();
    public List<Leilao> ListLeiloesAgendadosMostrar{ get; set;} = new List<Leilao>();
    public bool buttonOrdenarValorSelecionado{get;set;} = false;
    public bool buttonOrdenarTempoSelecionado{get;set;} = false;
    public int leilaoAtivoMostrar {get;set;} = 0;
    public int leilaoAgendadoMostrar {get;set;} = 0;


    //Atributos para o profile
    public int leilaoCompradoMostrar {get;set;} = 0;
    public int leilaoVendidoMostrar {get;set;} = 0;
}