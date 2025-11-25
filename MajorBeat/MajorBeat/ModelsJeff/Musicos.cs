using MajorBeat.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.ModelsJeff
{
    public class Musico
    {
        public long idMusico { get; set; }
        public string token { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string endereco { get; set; }
        public string senha { get; set; }
        public string biografia { get; set; }
        public string? apelido { get; set; }
        public TipoMusico tipoMusico { get; set; }
        public List<NomeInstrumento> nomeInstrumentos { get; set; }
        public List<NomeGenero> nomeGeneros { get; set; }
        public byte[] FotoBytes { get; set; }
        public string linkLinkdin { get; set; }
        public string linkInsta { get; set; }
        public string linkTwitter { get; set; }
        public string linkFacebook { get; set; }
        public List<string> RedesSociais { get; set; }
        public List<string> mediaUrl { get; set; }
    }
}
