using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MajorBeat.Enums;

namespace MajorBeat.Models
{
    public class Musico
    {
        public long idMusico { get; set; }
        public string token { get; set; }
        public string nome { get; set; }
        public string? apelido { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string telefone { get; set; }
        public string endereco { get; set; }
        public byte[] fotoPerfil { get; set; }
        public string biografia { get; set; }
        public DateTime? dtCriacao { get; set; }

        public ObservableCollection<string> links { get; set; } = new();
        public TipoMusico? tipoMusico { get; set; }
        public ObservableCollection<NomeInstrumento> nomeInstrumento { get; set; } = new();
        public ObservableCollection<NomeGenero> nomeGeneros { get; set; } = new();
        public Role? role { get; set; }
        public ObservableCollection<string>? mediaUrl { get; set; } = new();

        public ObservableCollection<Avaliacao>? avaliacoes { get; set; } = new();
        public ObservableCollection<Chat>? chats { get; set; } = new();
    }
}
