using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.ModelsJeff
{
    public class MediaFile
    {
        // 1. Guarda o objeto ORIGINAL que o FilePicker nos deu.
        public FileResult OriginalFile { get; set; }

        // 2. Propriedades "helper" para o XAML (Carrossel)

        // Helper para o XAML saber se é vídeo
        public bool IsVideo => OriginalFile.ContentType.StartsWith("video/");

        // Helper para o XAML (MediaElement)
        public string FullPath => OriginalFile.FullPath;

        // Helper para o XAML (Image) - COM LÓGICA DE SEGURANÇA
        public ImageSource PreviewImageSource
        {
            get
            {
                // Se for um vídeo, retorna nulo (para o <Image> não quebrar)
                if (this.IsVideo)
                {
                    return null;
                }

                // Se for imagem, carrega o preview com segurança
                return ImageSource.FromFile(OriginalFile.FullPath);
            }
        }
    }
}

