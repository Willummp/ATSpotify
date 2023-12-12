using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace ATSpotify
{
  // Classe q estou usando para armazenar a lista de músicas
  public class Dados
  {
    public List<Musica> Musicas { get; set; }
  }


  public class GerenciadorArquivo
  {

    public Dados Dados { get; set; }

    // Construtor
    public GerenciadorArquivo()
    {
      this.IniciarListas();
    }

   
    public void IniciarListas()
    {
      // Verificr se o arquivo de músicas existe, se não, cria um novo
      if (File.Exists("musicas.json") == false)
        File.Create("musicas.json").Close();

      using (StreamReader reader = new StreamReader(File.OpenRead("musicas.json")))
      {
        try
        {
          string content = reader.ReadToEnd();
          this.Dados = JsonSerializer.Deserialize<Dados>(content);
          reader.Close();
        }
        catch
        {
          // Se falhar, inicializa a lista de músicas como vazia
          this.Dados = new Dados { Musicas = new List<Musica>() };
        }
      }
      // Se a lista de músicas for nula, inicializa como uma lista vazia
      if (this.Dados.Musicas == null)
      {
        this.Dados.Musicas = new List<Musica>();
      }
    }

    // Método para salvar a lista de músicas no arquivo
    public void SalvarDados()
    {
      // Se o arquivo existir, deleta o arquivo
      if (File.Exists("musicas.json"))
        File.Delete("musicas.json");

      // Escreve a lista de músicas no arquivo
      using (StreamWriter writer = new StreamWriter(File.Open("musicas.json", FileMode.OpenOrCreate)))
      {
        // Serializa a lista de músicas para JSON e escreve no arquivo
        string json = JsonSerializer.Serialize<Dados>(this.Dados);
        writer.WriteLine(json);
        writer.Flush();
        writer.Close();
      }
    }

    // Método para adicionar uma música à lista
    public void CadastrarMusica(Musica musica)
    {
      // Adiciona a música à lista
      this.Dados.Musicas.Add(musica);
      // Salva a lista de músicas no arquivo
      this.SalvarDados();
    }

    // Método para deletar uma música da lista
    public void DeletarMusica(Musica musica)
    {
      // Remove a música da lista
      this.Dados.Musicas.Remove(musica);
      // Salva a lista de músicas no arquivo
      this.SalvarDados();
    }

    // Método para editar o nome de uma música
    public void EditarNomeMusica(string nomeAntigo, string nomeNovo)
    {
      // Encontra a música na lista de músicas
      Musica musica = this.Dados.Musicas.Find(m => m.nome == nomeAntigo);

      // Se a música foi encontrada, altera o nome
      if (musica != null)
      {
        musica.nome = nomeNovo;
        // Salva a lista de músicas no arquivo
        this.SalvarDados();
      }
      else
      {
        // Se a música não foi encontrada, imprime uma mensagem de erro
        Console.WriteLine("Música não encontrada.");
      }
    }
  }
}