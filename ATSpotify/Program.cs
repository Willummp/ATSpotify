using ATSpotify;
using System;
using System.Collections.Generic;

internal class Program
{
    private static GerenciadorArquivo gerenciador;

    private static void Main(string[] args)
    {
        var stopKey = "0";
        var selectedOption = "";
        gerenciador = new GerenciadorArquivo();

        while (selectedOption != stopKey)
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1 - Cadastrar Musica");
            Console.WriteLine("2 - Cadastrar Artista a uma Musica");
            Console.WriteLine("3 - Deletar Musica");
            Console.WriteLine("4 - Deletar Artista de uma Musica");
            Console.WriteLine("5 - Editar Musica");
            Console.WriteLine("6 - Listar Musicas e seus Artistas");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("Digite a opção desejada: ");
            Console.WriteLine();
            selectedOption = Console.ReadLine();

            ExecutarOpcao(selectedOption);
        }
    }

    static void ExecutarOpcao(string opcao)
    {
        switch (opcao)
        {
            case "1":
                CadastrarMusica();
                break;
            case "2":
                CadastrarArtistaAMusica();
                break;
            case "3":
                DeletarMusica();
                break;
            case "4":
                DeletarArtistaDeMusica();
                break;
            case "5":
                EditarNomeMusica();
                break;
            case "6":
                ListarMusicasEArtistas();
                break;
            case "0":
                gerenciador.SalvarDados();
                Console.WriteLine("Saindo...");
                break;
            default:
                Console.WriteLine("Opção inválida");
                break;
        }
    }

    private static void CadastrarMusica()
    {
        Musica musica = new Musica();
        Console.WriteLine("Digite o nome da musica: ");
        musica.nome = Console.ReadLine();
        Console.WriteLine("Digite a duração da musica: ");
        musica.duracao = Console.ReadLine();
        gerenciador.CadastrarMusica(musica);
    }

    private static void CadastrarArtistaAMusica()
    {
        Console.WriteLine("Digite o nome da musica: ");
        string nomeMusica = Console.ReadLine();
        Musica musica = gerenciador.Dados.Musicas.Find(m => m.nome == nomeMusica);
        if (musica == null)
        {
            Console.WriteLine("Musica não encontrada");
            return;
        }
        Console.WriteLine("Digite o nome do artista: ");
        string nomeArtista = Console.ReadLine();
        Artista artista = new Artista { nome = nomeArtista };

        musica.CadastroMusicaAoArtista(artista);
        gerenciador.SalvarDados();
    }

    private static void DeletarMusica()
    {
        Console.WriteLine("Digite o nome da musica: ");
        string nomeMusica = Console.ReadLine();
        Musica musica = gerenciador.Dados.Musicas.Find(m => m.nome == nomeMusica);
        if (musica == null)
        {
            Console.WriteLine("Musica não encontrada");
            return;
        }
        gerenciador.DeletarMusica(musica);
    }

    private static void DeletarArtistaDeMusica()
    {
        Console.WriteLine("Digite o nome do artista: ");
        string nomeArtista = Console.ReadLine();
        foreach (var musica in gerenciador.Dados.Musicas)
        {
            Artista artista = musica.artistas.Find(a => a.nome == nomeArtista);
            if (artista != null)
            {
                musica.artistas.Remove(artista);
                Console.WriteLine("Artista removido das músicas: " + musica.nome);
            }
        }
        gerenciador.SalvarDados();
    }

    private static void EditarNomeMusica()
    {
        Console.WriteLine("Digite o nome da musica: ");
        string nomeMusica = Console.ReadLine();
        Musica musica = gerenciador.Dados.Musicas.Find(m => m.nome == nomeMusica);
        Console.WriteLine("Digite o novo nome da musica: ");
        string novoNomeMusica = Console.ReadLine();
        gerenciador.EditarNomeMusica(nomeMusica, novoNomeMusica);
    }

    private static void ListarMusicasEArtistas()
    {
        Console.WriteLine("Musicas e seus Artistas: ");
        Console.WriteLine();
        foreach (var musica in gerenciador.Dados.Musicas)
        {
            Console.WriteLine("Musica: " + musica.nome);
            Console.WriteLine("Artistas: ");
            foreach (var artista in musica.artistas)
            {
                if (artista != null)
                {
                    Console.Write(artista.nome);
                }

                if (artista.nome != musica.artistas[musica.artistas.Count - 1].nome)
                {
                    Console.Write(" feat. ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}