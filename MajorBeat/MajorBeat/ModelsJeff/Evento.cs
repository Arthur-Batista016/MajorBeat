using MajorBeat.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.ModelsJeff
{
    public class Evento
    {
        public string nome { get; set; }
        public string endereco { get; set; }
        public string descricao { get; set; }
        public List<NomeInstrumento> instrumentos { get; set; }
        public List<NomeGenero> generos { get; set; }
        public TipoEvento tipoEvento { get; set; }
        public TipoMusico tipoMusico { get; set; }
        public DateTime data { get; set; }
        public byte[] imagemLocalEvento { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public List<string> mediaUrl { get; set; }

    }
}
