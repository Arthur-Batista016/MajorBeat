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
        public long contratanteId { get; set; }
        public long musicoId { get; set; }

        public long eventoId { get; set; }

        public StatusProposta statusProposta;
    
    }
}
