using MajorBeat.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.Models
{
    public class Proposta
    {
        public long idProposta { get; set; }
        public Contratante contratante { get; set; }
        public Musico musico { get; set; }

        public Evento evento { get; set; }

        public StatusProposta statusProposta { get; set; }

        public double valor {  get; set; }
        public long? idRemetente { get; set; }


        public long? idRecebedor { get; set; }

        public long? idEvento { get; set; }

        public string ValorFormatado
        {
            get
            {
                // Agora, você está chamando double.ToString(...), que funciona.
                return valor.ToString("C2", new CultureInfo("pt-BR"));
            }
        }

        public string TituloNotificacao
        {
            get
            {
                // Verifica se contratante e evento existem
                if (contratante != null && evento != null)
                {
                    if(Preferences.Get("role", string.Empty) == "musico") { 
                    // Retorna a string formatada
                    return $"{contratante.nome} enviou uma proposta para {evento.nome}!";
                    }
                    else {
                        return $"{musico.nome} enviou uma proposta para {evento.nome}!";
                    }
                }
                return "Notificação de Proposta";
            }
        }


    }
}
