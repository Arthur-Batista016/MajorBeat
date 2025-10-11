using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Models
{
    internal class Mensagem
    {
        public long idMensagem { get; set; }
        public string texto { get; set; }
        public DateTime dataEnvio { get; set; }
        public bool? proposta { get; set; }
        public double? valor { get; set; }
        public Evento evento { get; set; }
        public Chat chat { get; set; }
        public long idRemetente { get; set; }
    }
}
