using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Models
{
    internal class Chat
    {
        public long id { get; set; }

        public DateTime dataInicio { get; set; }

        public Musico musico { get; set; }

        public Contratante contratante { get; set; }

        public ObservableCollection<Mensagem> mensagens {  get; set; }

    }
}
