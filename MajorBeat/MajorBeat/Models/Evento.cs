using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MajorBeat.Enums;

namespace MajorBeat.Models
{
    public class Evento
    {
        public long idEvento { get; set; }

        public string nome{ get; set; }

        public TipoMusico tipoMusico { get; set; }
        public StatusEvento status { get; set; }
        public TipoEvento tipoEvento { get; set; }

        public DateTime data { get; set; }

        public string endereco { get; set; }

        public ObservableCollection<string> imagemLocalEvento { get; set; } = new();

        public TimeSpan horaInicio { get; set; }
        public TimeSpan horaFim { get; set; }

        public string? descricao { get; set; }
        public string titulo { get; set; }
        public ObservableCollection<NomeInstrumento> nomeInstrumento { get; set; } = new();

        public ObservableCollection<NomeGenero> generos { get; set; } = new();

        public Musico musico { get; set; }

        public Contratante contratante { get; set; }

        public ObservableCollection<Avaliacao> avaliacoes { get; set; } = new();
    }
}
