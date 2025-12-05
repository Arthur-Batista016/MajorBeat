using MajorBeat.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Models
{
    public class Proposta
    {
        public long Id { get; set; }
        public Contratante contratante { get; set; }
        public Musico musico { get; set; }

        public Evento evento { get; set; }

        public StatusProposta statusProposta { get; set; }

        public string valor {  get; set; }
    
    }
}
