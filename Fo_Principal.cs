using System.Globalization;
using System.Text;
using YoutubeExplode;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos.Streams;

namespace Youtube.Video.Downloader
{
    public partial class Fo_Principal : Form
    {
        public Fo_Principal()
        {
            InitializeComponent();
        }
        int musicasBaixadas = 0;
        int i = 1;
        private string LimparNomeParaArquivo(string nome)
        {
            nome = nome.Normalize(NormalizationForm.FormC);
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                nome = nome.Replace(c, '-');
            }
            return nome;
        }
        private bool ExisteInformacaoNoGrid(string link)
        {
            link = link.Replace("https://www.youtube.com/watch?v=", "");
            foreach (DataGridViewRow row in Grid_musicas.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString() == link)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private async void Btn_download_Click(object sender, EventArgs e)
        {
            if (Grid_musicas.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Lista de musicas para download vazia!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int totalMusicas = Grid_musicas.Rows.Count;
            int musicasBaixadas = 0;
            foreach (DataGridViewRow item in Grid_musicas.Rows)
            {
                var nome = item.Cells[1].Value.ToString();
                var _id_video = item.Cells[2].Value.ToString();
                string link = "https://www.youtube.com/watch?v=" + _id_video;
                try
                {
                    var youtube = new YoutubeClient();
                    var video = await youtube.Videos.GetAsync(link);
                    var manifestvideo = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                    var nomemusic = LimparNomeParaArquivo(video.Title);
                    string diretoriodownload = Application.StartupPath + @"Musicas\";
                    var localdownload = Path.Combine(diretoriodownload, $"{nomemusic}.mp3");
                    if (System.IO.File.Exists(localdownload))
                    {
                        MessageBox.Show("Música já baixada!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Grid_musicas.Rows.Remove(item);
                        AtualizarBarraDeProgresso(musicasBaixadas);
                        continue;
                    }
                    var infovideo = manifestvideo.GetAudioOnlyStreams().GetWithHighestBitrate();
                    var localarquivo = Path.Combine(Path.GetTempPath(), $"{nomemusic}.mp3");
                    await youtube.Videos.Streams.DownloadAsync(infovideo, localarquivo);
                    var arquivomp3 = Path.Combine(Path.GetTempPath(), $"{nomemusic}.mp3");
                    if (!Directory.Exists("Musicas"))
                    {
                        Directory.CreateDirectory("Musicas");
                    }
                    string outputfile = $"{diretoriodownload}" + $"{nomemusic}.mp3";
                    System.IO.File.Move(arquivomp3, outputfile);
                    musicasBaixadas++;
                    AtualizarBarraDeProgresso(musicasBaixadas);
                    lbl_baixadas.Text = Convert.ToString(musicasBaixadas);
                    item.Cells[5].Value = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (ex.Message.Contains("429"))
                    {
                        MessageBox.Show("Erro 429 - Limite de requisições atingido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var result = MessageBox.Show("Deseja continuar?", "Erro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            if (Grid_musicas.Rows.Count == 0)
            {
                MessageBox.Show("Playlist já baixada!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Download Finalizado com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Grid_musicas.Rows.Clear();
            Pbar.Value = 0;
            i = 0;
            musicasBaixadas = 0;
        }
        private async void btn_addlist_Click(object sender, EventArgs e)
        {
            string diretoriodownload = Application.StartupPath + @"Musicas\";
            string link = txt_link.Text;
            if (txt_link.Text == "")
            {
                MessageBox.Show("Informe um link para poder incluir na lista!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (cbx_playlist.Checked)
            #region 1 - Caso seja uma playlist 
            {
                try
                {
                    var id_playlist_link = link.Replace("https://www.youtube.com/playlist?list=", "");
                    if (ExisteInformacaoNoGrid(id_playlist_link))
                    {
                        MessageBox.Show("Playlist já existente na lista!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txt_link.Text = "";
                        cbx_playlist.Checked = false;
                        return;
                    }
                    var youtube = new YoutubeClient();
                    await foreach (var video in youtube.Playlists.GetVideosAsync(link))
                    {
                        var id_video = video.Id;
                        var id_playlist = video.PlaylistId;
                        var titulo = LimparNomeParaArquivo(video.Title);
                        var minutos = video.Duration;
                        var localdownload = Path.Combine(diretoriodownload, $"{titulo}.mp3");
                        if (System.IO.File.Exists(localdownload))
                        {
                            continue;
                        }
                        Grid_musicas.Rows.Add(i, titulo, id_video, minutos, id_playlist);
                        i++;
                        txt_link.Text = null;
                        cbx_playlist.Checked = false;
                        lbl_total.Text = Convert.ToString(i);
                    }
                    if (Grid_musicas.Rows.Count == 0)
                    {
                        MessageBox.Show("Todas as músicas desta playlist já estão baixadas!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_link.Text = "";
                        cbx_playlist.Checked = false;
                        return;
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Erro na requisição: {ex.Message}");
                    Console.WriteLine($"Erro na requisição: {ex.Message}");
                    return;
                }
            }
            #endregion
            else
            #region 2- Caso não seja uma playlist
            {
                #region Faz a consulta via Framework e adiciona no Grid
                var youtube = new YoutubeClient();
                //obtem video youtube//
                var video = await youtube.Videos.GetAsync(link);
                //manifesto do video para framework
                var manifestvideo = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                string titulo = LimparNomeParaArquivo(video.Title);
                string id_video = video.Id;
                if (ExisteInformacaoNoGrid(id_video))
                {
                    MessageBox.Show("Música já existente na lista!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_link.Text = "";
                    return;
                }
                var minutos = video.Duration;
                var localdownload = Path.Combine(diretoriodownload, $"{titulo}.mp3");
                if (System.IO.File.Exists(localdownload) || ExisteInformacaoNoGrid(id_video))
                {
                    MessageBox.Show($"{titulo} já baixada!!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_link.Text = "";
                    return;
                }
                try
                {
                    Grid_musicas.Rows.Add(i, titulo, id_video, minutos);
                    txt_link.Text = null;
                    lbl_total.Text = Convert.ToString(i);
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Erro na requisição: {ex.Message}");
                    Console.WriteLine($"Erro na requisição: {ex.Message}");
                    return;
                }
                #endregion
            }
            #endregion
        }
        private void btn_excluir_Click(object sender, EventArgs e)
        {
            if (Grid_musicas.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in Grid_musicas.SelectedRows)
                {
                    Grid_musicas.Rows.Remove(row);
                    i--;
                    lbl_total.Text = Convert.ToString(i);
                }
            }
        }
        private void AtualizarBarraDeProgresso(int valor)
        {
            if (valor <= Pbar.Maximum)
            {
                Pbar.Value = valor;
            }
        }
        private void txt_link_TextChanged(object sender, EventArgs e)
        {
            string link = txt_link.Text;
            if (link.Contains("https://www.youtube.com/watch?v="))
            {
                cbx_playlist.Checked = false;
            }
            else if (link.Contains("https://www.youtube.com/playlist?list="))
            {
                cbx_playlist.Checked = true;
            }
        }

        private void btn_limpar_Click(object sender, EventArgs e)
        {
            Grid_musicas.Rows.Clear();
            i = 0;
            lbl_total.Text = Convert.ToString(i);
        }
    }
}