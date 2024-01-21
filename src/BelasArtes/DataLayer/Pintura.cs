using System.Drawing;


namespace DataLayer;
public class Pintura {
    public int? Id { get; set; }
    public string Nome { get; set; } = "";
    public double? Altura { get; set; }
    public double? Largura { get; set; }
    public double? Peso { get; set; }
    public string Descricao { get; set; } = "";
<<<<<<< HEAD
    public byte[]? Foto { get; set; }
=======
    public Byte[]? Foto { get; set; }
>>>>>>> d94d33ad15fe0f0771df3d4d70c91e4d9de8f9c7
    public string Artista { get; set; } = "";
    public int? AnoCriacao { get; set; }
    public bool? Original { get; set; }
    public bool? VerificacaoAutenticidade { get; set; }
    public string? EmailVendedor { get; set; } 
    public int? CodMovimentoArtistico { get; set; }

<<<<<<< HEAD
    public Pintura(int? id, string nome, double? altura, double? largura, double? peso, string descricao,
    byte[]? foto, string artista, int? anoCriacao, bool? original,
=======
    public Pintura(){}

    public Pintura(int? id, string nome, float? altura, float? largura, float? peso, string descricao,
    Byte[]? foto, string artista, int? anoCriacao, bool? original,
>>>>>>> d94d33ad15fe0f0771df3d4d70c91e4d9de8f9c7
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