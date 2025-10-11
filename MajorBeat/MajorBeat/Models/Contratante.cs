using MajorBeat.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Models
{
    internal class Contratante
    {
        public long IdContratante { get; set; }

        public string Nome { get; set; }

        public string? Apelido { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        public byte[] FotoPerfil { get; set; }

        public string? Biografia { get; set; }

        public DateTime DtCriacao { get; set; }

        public ObservableCollection<string> Links { get; set; } = new();

        public string NomeEmpresa { get; set; }

        public TipoContratante TipoContratante { get; set; }

        public Role Role { get; set; }

        public ObservableCollection<string>? MediaUrl { get; set; } = new();

        public ObservableCollection<Avaliacao> Avaliacoes { get; set; } = new();

        public ObservableCollection<Chat> Chats { get; set; } = new();
    }
}
