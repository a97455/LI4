using DataLayer;

public class AppState{
    //Atributos para a página com os diversos leilões
    public List<Leilao> ListLeiloesMostrar{ get; set;} = new List<Leilao>();
    public bool buttonOrdenarSelecionado{get;set;} = false;
}