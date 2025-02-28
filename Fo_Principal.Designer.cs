namespace Youtube.Video.Downloader
{
    partial class Fo_Principal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fo_Principal));
            txt_link = new TextBox();
            btn_download = new Button();
            Grid_musicas = new DataGridView();
            btn_addlist = new Button();
            btn_excluir = new Button();
            Pbar = new ProgressBar();
            cbx_playlist = new CheckBox();
            label1 = new Label();
            lbl_total = new Label();
            label2 = new Label();
            lbl_baixadas = new Label();
            btn_limpar = new Button();
            ID = new DataGridViewTextBoxColumn();
            Titulo = new DataGridViewTextBoxColumn();
            linkorId_video = new DataGridViewTextBoxColumn();
            Tempo = new DataGridViewTextBoxColumn();
            id_playlist = new DataGridViewTextBoxColumn();
            baixada = new DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)Grid_musicas).BeginInit();
            SuspendLayout();
            // 
            // txt_link
            // 
            txt_link.Font = new Font("Segoe UI", 11F);
            txt_link.Location = new Point(12, 4);
            txt_link.Name = "txt_link";
            txt_link.Size = new Size(640, 27);
            txt_link.TabIndex = 0;
            txt_link.TextChanged += txt_link_TextChanged;
            // 
            // btn_download
            // 
            btn_download.Font = new Font("Segoe UI", 11F);
            btn_download.Location = new Point(437, 447);
            btn_download.Name = "btn_download";
            btn_download.Size = new Size(368, 38);
            btn_download.TabIndex = 1;
            btn_download.Text = "Download";
            btn_download.UseVisualStyleBackColor = true;
            btn_download.Click += Btn_download_Click;
            // 
            // Grid_musicas
            // 
            Grid_musicas.AllowUserToAddRows = false;
            Grid_musicas.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Grid_musicas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Grid_musicas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Grid_musicas.Columns.AddRange(new DataGridViewColumn[] { ID, Titulo, linkorId_video, Tempo, id_playlist, baixada });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            Grid_musicas.DefaultCellStyle = dataGridViewCellStyle2;
            Grid_musicas.Location = new Point(12, 78);
            Grid_musicas.MultiSelect = false;
            Grid_musicas.Name = "Grid_musicas";
            Grid_musicas.ReadOnly = true;
            Grid_musicas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Grid_musicas.Size = new Size(793, 334);
            Grid_musicas.TabIndex = 2;
            // 
            // btn_addlist
            // 
            btn_addlist.Font = new Font("Segoe UI", 11F);
            btn_addlist.Location = new Point(12, 37);
            btn_addlist.Name = "btn_addlist";
            btn_addlist.Size = new Size(419, 35);
            btn_addlist.TabIndex = 3;
            btn_addlist.Text = "Adicionar a Lista";
            btn_addlist.UseVisualStyleBackColor = true;
            btn_addlist.Click += btn_addlist_Click;
            // 
            // btn_excluir
            // 
            btn_excluir.Font = new Font("Segoe UI", 11F);
            btn_excluir.Location = new Point(437, 37);
            btn_excluir.Name = "btn_excluir";
            btn_excluir.Size = new Size(368, 35);
            btn_excluir.TabIndex = 4;
            btn_excluir.Text = "Excluir da Lista";
            btn_excluir.UseVisualStyleBackColor = true;
            btn_excluir.Click += btn_excluir_Click;
            // 
            // Pbar
            // 
            Pbar.Location = new Point(9, 418);
            Pbar.Name = "Pbar";
            Pbar.Size = new Size(793, 23);
            Pbar.TabIndex = 5;
            // 
            // cbx_playlist
            // 
            cbx_playlist.AutoSize = true;
            cbx_playlist.Font = new Font("Segoe UI", 11F);
            cbx_playlist.Location = new Point(658, 4);
            cbx_playlist.Name = "cbx_playlist";
            cbx_playlist.Size = new Size(154, 24);
            cbx_playlist.TabIndex = 6;
            cbx_playlist.Text = "Playlist do Youtube";
            cbx_playlist.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(9, 447);
            label1.Name = "label1";
            label1.Size = new Size(126, 20);
            label1.TabIndex = 7;
            label1.Text = "Total de músicas: ";
            // 
            // lbl_total
            // 
            lbl_total.AutoSize = true;
            lbl_total.Font = new Font("Segoe UI", 11F);
            lbl_total.Location = new Point(132, 447);
            lbl_total.Name = "lbl_total";
            lbl_total.Size = new Size(17, 20);
            lbl_total.TabIndex = 8;
            lbl_total.Text = "0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(9, 467);
            label2.Name = "label2";
            label2.Size = new Size(189, 20);
            label2.TabIndex = 9;
            label2.Text = "Total de músicas baixadas: ";
            // 
            // lbl_baixadas
            // 
            lbl_baixadas.AutoSize = true;
            lbl_baixadas.Font = new Font("Segoe UI", 11F);
            lbl_baixadas.Location = new Point(195, 467);
            lbl_baixadas.Name = "lbl_baixadas";
            lbl_baixadas.Size = new Size(17, 20);
            lbl_baixadas.TabIndex = 10;
            lbl_baixadas.Text = "0";
            // 
            // btn_limpar
            // 
            btn_limpar.Font = new Font("Segoe UI", 11F);
            btn_limpar.Location = new Point(300, 447);
            btn_limpar.Name = "btn_limpar";
            btn_limpar.Size = new Size(131, 38);
            btn_limpar.TabIndex = 11;
            btn_limpar.Text = "Limpar lista";
            btn_limpar.UseVisualStyleBackColor = true;
            btn_limpar.Click += btn_limpar_Click;
            // 
            // ID
            // 
            ID.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            ID.Width = 50;
            // 
            // Titulo
            // 
            Titulo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Titulo.HeaderText = "Título Música";
            Titulo.Name = "Titulo";
            Titulo.ReadOnly = true;
            // 
            // linkorId_video
            // 
            linkorId_video.HeaderText = "id_video";
            linkorId_video.Name = "linkorId_video";
            linkorId_video.ReadOnly = true;
            linkorId_video.Visible = false;
            // 
            // Tempo
            // 
            Tempo.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Tempo.HeaderText = "Tempo";
            Tempo.Name = "Tempo";
            Tempo.ReadOnly = true;
            Tempo.Width = 82;
            // 
            // id_playlist
            // 
            id_playlist.HeaderText = "id_playlist";
            id_playlist.Name = "id_playlist";
            id_playlist.ReadOnly = true;
            id_playlist.Visible = false;
            // 
            // baixada
            // 
            baixada.HeaderText = "Baixada";
            baixada.Name = "baixada";
            baixada.ReadOnly = true;
            // 
            // Fo_Principal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 494);
            Controls.Add(btn_limpar);
            Controls.Add(lbl_baixadas);
            Controls.Add(label2);
            Controls.Add(lbl_total);
            Controls.Add(label1);
            Controls.Add(cbx_playlist);
            Controls.Add(Pbar);
            Controls.Add(btn_excluir);
            Controls.Add(btn_addlist);
            Controls.Add(Grid_musicas);
            Controls.Add(btn_download);
            Controls.Add(txt_link);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Fo_Principal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Youtube Mp3 Dowloader";
            ((System.ComponentModel.ISupportInitialize)Grid_musicas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_link;
        private Button btn_download;
        private DataGridView Grid_musicas;
        private Button btn_addlist;
        private Button btn_excluir;
        private ProgressBar Pbar;
        private CheckBox cbx_playlist;
        private Label label1;
        private Label lbl_total;
        private Label label2;
        private Label lbl_baixadas;
        private Button btn_limpar;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Titulo;
        private DataGridViewTextBoxColumn linkorId_video;
        private DataGridViewTextBoxColumn Tempo;
        private DataGridViewTextBoxColumn id_playlist;
        private DataGridViewCheckBoxColumn baixada;
    }
}
