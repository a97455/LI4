using System.Drawing;


namespace DataLayer;
public class Pintura {
    public int? Id { get; set; }
    public string Nome { get; set; } = "";
    public float? Altura { get; set; }
    public float? Largura { get; set; }
    public float? Peso { get; set; }
    public string Descricao { get; set; } = "";
    public Bitmap? Foto { get; set; }
    public string Artista { get; set; } = "";
    public int? AnoCriacao { get; set; }
    public bool? Original { get; set; }
    public bool? VerificacaoAutenticidade { get; set; }
    public string? EmailVendedor { get; set; } 
    public int? CodMovimentoArtistico { get; set; }

    public Pintura(int? id, string nome, float? altura, float? largura, float? peso, string descricao,
    Bitmap? foto, string artista, int? anoCriacao, bool? original,
    bool? verificacaoAutenticidade, string? emailVendedor, int? codMovimentoArtistico){
        Id = id;
        Nome = nome;
        Altura = altura;
        Largura = largura;
        Peso = peso;
        Descricao = descricao;
        Foto = foto;
        Artista = artista;
        AnoCriacao = anoCriacao;
        Original = original;
        VerificacaoAutenticidade = verificacaoAutenticidade;
        EmailVendedor = emailVendedor;
        CodMovimentoArtistico = codMovimentoArtistico;
    }
}