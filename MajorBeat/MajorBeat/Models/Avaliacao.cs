using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Models
{
    public class Avaliacao
    {
        public long id { get; set; }

        public double nota { get; set; }

        public string? comentario { get; set; }

        public DateTime? data { get; set; }
        public long idAvaliador { get; set; }

        public long idRecebedor { get; set; }

        public Musico idMusico { get; set; }

        public Contratante idContratante { get; set; }
    }
}
