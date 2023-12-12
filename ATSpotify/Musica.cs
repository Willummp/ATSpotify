namespace ATSpotify;

public class Musica
{
  public string nome { get; set; }
  public string duracao { get; set; }
  public List<Artista> artistas { get; set; }

  public void CadastroMusicaAoArtista(Artista artista)
  {
    if (this.artistas == null){
      this.artistas = new List<Artista>();
    }
    this.artistas.Add(artista);
  }
}