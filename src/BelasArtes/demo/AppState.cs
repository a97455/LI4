using DataLayer;

public class AppState{
    //Atributos para a página com os diversos leilões
    public List<Leilao> ListLeiloesAtivosMostrar{ get; set;} = new List<Leilao>();
    public List<Leilao> ListLeiloesAgendadosMostrar{ get; set;} = new List<Leilao>();
    public bool buttonOrdenarSelecionado{get;set;} = false;
    public bool buttonMudarTipoLeilaoSelecionado{get;set;} = false;


    //Atributos gerais ao site
    public string atual_user {get;set;} = "user1@example.com";
    public int? atual_leilao {get; set;} = null;
}