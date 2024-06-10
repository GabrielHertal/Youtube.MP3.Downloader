﻿using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using static System.Net.WebRequestMethods;
using System;
using MediaToolkit;
using VideoLibrary;
using Xabe.FFmpeg;
using System.Diagnostics;
using OpenQA.Selenium.Chrome;
using Microsoft.Extensions.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.IO.IsolatedStorage;
using MediaToolkit.Model;
using MediaToolkit.Options;
using System.ComponentModel.DataAnnotations;

namespace Youtube.Video.Downloader
{
    public partial class Fo_Principal : Form
    {
        public IConfiguration _config;
        public string chaveapi;
        public string hostapi;
        public Fo_Principal()
        {
            InitializeComponent();
            _config = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json")
                .Build();  
            FFmpeg.SetExecutablesPath(@"C:\Users\Gabriel\Desktop\Projeto orçamento C#\Youtube.Video.Downloader\bin\Debug\net7.0-windows\Util\ffmpeg-master-latest-win64-gpl\bin\ffmpeg.exe");
        }
        int i = 0;
        public int j = 0;
        private string LimparNomeParaArquivo(string nome)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                nome = nome.Replace(c, '-');
            }
            return nome;
        }
        private bool ExisteInformacaoNoGrid(string link)
        {
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
            foreach (DataGridViewRow item in Grid_musicas.Rows)
            {
                string nome = item.Cells[1].Value.ToString();
                string _id_video = item.Cells[2].Value.ToString();
                string link = "https://www.youtube.com/watch?v=" + _id_video;
                if (!Directory.Exists("Musicas Baixadas"))
                {
                    Directory.CreateDirectory("Musicas Baixadas");
                }
                string path = Path.Combine(Application.StartupPath, "Musicas Baixadas\\");
                var youtube = YouTube.Default;
                var video = await youtube.GetVideoAsync(link);
                string videoFilePath = Path.Combine(path, video.FullName);
                await System.IO.File.WriteAllBytesAsync(videoFilePath, await video.GetBytesAsync());

                string outputFilePath = Path.Combine(path, $"{nome}.mp3");
                try
                {
                    var conversion = await FFmpeg.Conversions.New()
                        .AddParameter($"-i \"{videoFilePath}\"")
                        .AddParameter($"-vn \"{outputFilePath}\"")
                        .SetOutputFormat(Format.mp3)
                        .Start();

                    MessageBox.Show("Conversão concluída: " + outputFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro durante a conversão: " + ex.Message);
                }
                // Limpeza pós-conversão, se necessário
                if (System.IO.File.Exists(videoFilePath))
                {
                    System.IO.File.Delete(videoFilePath);
                }
                // Atualização da UI após a conversão
                Pbar.Value = 0;
                Grid_musicas.Rows.Clear();
            }
        }
                /////////////////////////////Verifica qual a chave da API esta selecionada//////////////////////////////////
                //if (cbx_chave.SelectedIndex == -1)
                //{
                //    MessageBox.Show("Selecione uma ChaveAPI", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //else if (cbx_chave.SelectedIndex == 0)
                //{
                //    chaveapi = _config["ChaveApi:X-RapidAPI-Key1"];
                //}
                //else if (cbx_chave.SelectedIndex == 1)
                //{
                //    chaveapi = _config["ChaveApi:X-RapidAPI-Key2"];
                //}
                //else
                //{
                //    chaveapi = _config["ChaveApi:X-RapidAPI-Key3"];
                //}
                /////////////////////////////Verifica qual a Host da API esta selecionada//////////////////////////////////
                //if (cbx_host.SelectedIndex == -1)
                //{
                //    MessageBox.Show("Selecione um HostAPI!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //else if (cbx_host.SelectedIndex == 0)
                //{
                //    hostapi = _config["HostApi:X-RapidAPI-Host1"];
                //}
                //else
                //{
                //    hostapi = _config["HostApi:X-RapidAPI-Host2"];
                //}
                //foreach (DataGridViewRow item in Grid_musicas.Rows)
                //{
                //    string nome = item.Cells[1].Value.ToString();
                //    string _id_video = item.Cells[2].Value.ToString();
                //    string nome_limpo = LimparNomeParaArquivo(nome);
                //    string uri;
                //    var client = new HttpClient();
                //    //////////Verifica qual a chave da API e dependendo altera a URI que faz a requisição///////////////
                //    if (hostapi == "youtube-mp3-downloader2.p.rapidapi.com")
                //    {
                //        uri = ("https://youtube-mp3-downloader2.p.rapidapi.com/ytmp3/ytmp3/?url=" + _id_video);
                //    }
                //    else
                //    {
                //        uri = ("https://youtube-mp3-download1.p.rapidapi.com/dl?id=" + _id_video);
                //    }
                //    var request = new HttpRequestMessage
                //    {
                //        Method = HttpMethod.Get,
                //        RequestUri = new Uri(uri),
                //        Headers =
                //        {
                //        { "X-RapidAPI-Key", chaveapi },
                //        { "X-RapidAPI-Host", hostapi },
                //        },
                //    };
                //    try
                //    {
                //        using var response = await client.SendAsync(request);
                //        response.EnsureSuccessStatusCode();
                //        var responsebody = await response.Content.ReadAsStringAsync();
                //        dynamic jsonresponse = JsonConvert.DeserializeObject<dynamic>(responsebody);
                //        /////////////////Verifica qual a chave da API e qual ação deve fazer///////////////
                //        if (hostapi == "youtube-mp3-downloader2.p.rapidapi.com")
                //        {
                //            string urlaudio = jsonresponse.dlink;
                //            using var httpClient = new HttpClient();
                //            {
                //                var audiolink = await httpClient.GetByteArrayAsync(urlaudio);
                //                if (!Directory.Exists("Musicas"))
                //                {
                //                    Directory.CreateDirectory("Musicas");
                //                }
                //                using (var arquivoaudio = System.IO.File.Create("Musicas/" + nome_limpo + ".mp3"))
                //                {
                //                    await arquivoaudio.WriteAsync(audiolink);
                //                    System.Threading.Thread.Sleep(500);
                //                }
                //                Barradeprogresso();
                //            }
                //        }
                //        //////////////////API que faz o download do video abrindo o Chrome e clicando no botão download////////////////////////
                //        else
                //        {
                //            var options = new ChromeOptions();
                //            if (!Directory.Exists("Musicas"))
                //            {
                //                Directory.CreateDirectory("Musicas");
                //            }
                //            string diretoriodownload = @"C:\Users\Gabriel\Desktop\Projeto orçamento C#\Youtube.Video.Downloader\Musicas";
                //            options.AddUserProfilePreference("download.default_directory", diretoriodownload);
                //            string linkdownload = jsonresponse.link;
                //            using (var driver = new ChromeDriver())
                //            {
                //                driver.Navigate().GoToUrl(linkdownload);
                //                var dowloadbutton = driver.FindElement(By.ClassName("dlbtn"));
                //                System.Threading.Thread.Sleep(1500);
                //                dowloadbutton.Click();
                //                System.Threading.Thread.Sleep(2500);
                //            }
                //            Barradeprogresso();
                //        }
                //    }
                //    catch (HttpRequestException ex)
                //    {
                //        MessageBox.Show($"Erro na requisição: {ex.Message}");
                //        Console.WriteLine($"Erro na requisição: {ex.Message}");
                //        return;
                //    }
                //}
                //MessageBox.Show("Downloads finalizados com sucesso!");
                //Console.WriteLine("Downloads finalizados com sucesso!");
        private async void btn_addlist_Click(object sender, EventArgs e)
        {
            ///////////////////////////Verifica qual a chave da API esta selecionada//////////////////////////////////
            if (cbx_chave.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione uma ChaveAPI", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cbx_chave.SelectedIndex == 0)
            {
                chaveapi = _config["ChaveApi:X-RapidAPI-Key1"];
            }
            else if (cbx_chave.SelectedIndex == 1)
            {
                chaveapi = _config["ChaveApi:X-RapidAPI-Key2"];
            }
            else
            {
                chaveapi = _config["ChaveApi:X-RapidAPI-Key3"];
            }
            ///////////////////////////Verifica qual a Host da API esta selecionada//////////////////////////////////
            if (cbx_host.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um HostAPI!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cbx_host.SelectedIndex == 0)
            {
                hostapi = _config["HostApi:X-RapidAPI-Host1"];
            }
            else
            {
                hostapi = _config["HostApi:X-RapidAPI-Host2"];
            }
            string link = txt_link.Text;
            if (txt_link.Text == "")
            {
                MessageBox.Show("Informe um link para poder incluir na lista!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ///////////////////////////////////////Caso seja uma playlist///////////////////////////////////
            if (cbx_playlist.Checked)
            {
                string quebrastring = "https://www.youtube.com/playlist?list=";
                string id_playlist = link.Replace(quebrastring, "");
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://youtube138.p.rapidapi.com/playlist/videos/?id=" + id_playlist + "&hl=en&gl=US"),
                    Headers =
                    {
                  { "X-RapidAPI-Key", chaveapi },
                  { "X-RapidAPI-Host", "youtube138.p.rapidapi.com" },
                    },
                };
                try
                {
                    using var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    dynamic jsonresponse = JsonConvert.DeserializeObject<dynamic>(body);
                    foreach (var item in jsonresponse.contents)
                    {
                        var id = item.index;
                        string titulo = item.video.title;
                        string _id_video = item.video.videoId;
                        string id_video = null;
                        if (hostapi == "youtube-mp3-download1.p.rapidapi.com")
                        {
                            id_video = item.video.videoId;
                        }
                        //////////////////Monta link para o video e adiciona na lista////////////////////////////
                        else
                        {
                            id_video = "https://www.youtube.com/watch?v=" + _id_video;
                        }
                        var temp_segundos = Convert.ToDouble(item.video.lengthSeconds);
                        var minutos = TimeSpan.FromSeconds(temp_segundos);
                        Grid_musicas.Rows.Add(id, titulo, id_video, minutos);
                        txt_link.Text = null;
                        j++;
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Erro na requisição: {ex.Message}");
                    Console.WriteLine($"Erro na requisição: {ex.Message}");
                    return;
                }
            }
            ////////////////////////////////////Caso não seja uma playlist////////////////////////////////////////////////////////
            else
            {
                string quebrastring = "https://www.youtube.com/watch?v=";
                string id_video = link.Replace(quebrastring, "");
                if (ExisteInformacaoNoGrid(id_video) == true)
                {
                    MessageBox.Show("Link já existente na lista!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://youtube138.p.rapidapi.com/video/details/?id=" + id_video + "&hl=en&gl=US"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", chaveapi },
                        { "X-RapidAPI-Host", "youtube138.p.rapidapi.com" },
                    },
                };
                try
                {
                    using var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    dynamic jsonresponse = JsonConvert.DeserializeObject<dynamic>(body);
                    string titulo = jsonresponse.title;
                    var temp_segundos = Convert.ToDouble(jsonresponse.lengthSeconds);
                    var minutos = TimeSpan.FromSeconds(temp_segundos);
                    Grid_musicas.Rows.Add(i, titulo, id_video, minutos);
                    txt_link.Text = null;
                    i++;
                    j++;
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Erro na requisição: {ex.Message}");
                    Console.WriteLine($"Erro na requisição: {ex.Message}");
                    return;
                }
            }
        }
        private void btn_excluir_Click(object sender, EventArgs e)
        {
            if (Grid_musicas.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in Grid_musicas.SelectedRows)
                {
                    Grid_musicas.Rows.Remove(row);
                    i--;
                    j--;
                }
            }
        }
        private void Barradeprogresso() 
        {
            Pbar.Minimum = 0;
            Pbar.Maximum = j;
            for(int  i = 0; i <= j; i++) 
            {
                MessageBox.Show(i.ToString());
                Pbar.Value = i;
                return;
            }
        }
    }
}