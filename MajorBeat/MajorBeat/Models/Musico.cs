using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MajorBeat.Enums;
using Newtonsoft.Json;

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
        public byte[]? fotoPerfil { get; set; }
        public string biografia { get; set; }
        public DateTime? dtCriacao { get; set; }

        public ObservableCollection<string> links { get; set; } = new();
        public TipoMusico? tipoMusico { get; set; }
        public ObservableCollection<NomeInstrumento> nomeInstrumentos { get; set; } = new();
        public ObservableCollection<NomeGenero> nomeGeneros { get; set; } = new();
        public Role? role { get; set; }
        public ObservableCollection<string>? mediaUrl { get; set; } = new();

        public ObservableCollection<Avaliacao>? avaliacoes { get; set; } = new();
        public ObservableCollection<Chat>? chats { get; set; } = new();

        public string? linkLinkdin { get; set; }
        public string? linkInsta { get; set; }
        public string? linkTwitter { get; set; }
        public string? linkFacebook { get; set; }

        public string NotaFormatada
        {
            get
            {
                // Verifica se a lista existe e não está vazia
                if (this.avaliacoes?.Count > 0)
                {
                    var primeiraAvaliacao = this.avaliacoes[0];

                    // Verifica se a nota do primeiro item é maior que zero
                    if (primeiraAvaliacao != null && primeiraAvaliacao.nota > 0)
                    {
                        // Retorna a nota formatada (ex: 4.5)
                        // (Nota: Assumindo que 'nota' é uma propriedade na classe Avaliacao)
                        return primeiraAvaliacao.nota.ToString("F1", System.Globalization.CultureInfo.InvariantCulture);
                    }
                }

                // Retorna o texto de fallback em qualquer outro caso (null, vazia, nota zero)
                return "X";
            }
        }

    }
}
