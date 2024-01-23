namespace DataLayer;
public class MovimentoArtistico {
        public int Id { get; set; }
        public string Nome { get; set; } = "";

        public MovimentoArtistico(){}

        public MovimentoArtistico(int id, string nome){
            Id = id;
            Nome=nome;
        }
}