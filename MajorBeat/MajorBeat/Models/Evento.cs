using MajorBeat.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Models
{
    internal class Evento
    {
        public long IdEvento { get; set; }

        public string Nome { get; set; }

        public TipoMusico TipoMusico { get; set; }
        public StatusEvento Status { get; set; }
        public TipoEvento TipoEvento { get; set; }

        public DateTime Data { get; set; }

        public string Endereco { get; set; }

        public ObservableCollection<byte[]> ImagemLocalEvento { get; set; } = new();

        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        public string? Descricao { get; set; }
        public string Titulo { get; set; }
        public ObservableCollection<NomeInstrumento> NomeInstrumento { get; set; } = new();

        public ObservableCollection<NomeGenero> NomeGenero { get; set; } = new();

        public Musico Musico { get; set; }

        public Contratante Contratante { get; set; }

        public ObservableCollection<Avaliacao> Avaliacoes { get; set; } = new();
    }
}
