using CommunityToolkit.Mvvm.ComponentModel;
using MajorBeat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorBeat.ViewModels.Users
{
    public partial class ChatViewModel:ObservableObject
    {
        public ChatViewModel()
        {
            musicosPadrao();
            Contacts();
        }

        [ObservableProperty]
        public bool noContacts = true;

        [ObservableProperty]
        public bool hasContacts = true;

        [ObservableProperty]
        public ObservableCollection<Musico> musicos;

        [ObservableProperty]
        public ObservableCollection<string> searchs;

        [ObservableProperty]
        public ObservableCollection<string> imagens = new ObservableCollection<string>
        {
            "chaticon",
            "chaticon2"
        };

        private string userEntry;

        public string UserEntry
        {
            get => userEntry;
            set
            {
                if (userEntry != value)
                {
                    userEntry = value;
                    OnPropertyChanged(nameof(UserEntry));
                    searchContacts();
                }
            }
        }

        private ObservableCollection<string> filteredSearchs = new ObservableCollection<string>();
        public ObservableCollection<string> FilteredSearchs
        {
            get => filteredSearchs;
            set
            {
                filteredSearchs = value;
                OnPropertyChanged(nameof(FilteredSearchs));
            }
        }

        public async Task musicosPadrao()
        {
            Musicos = new ObservableCollection<Musico>
        {
        new Musico
        {
            nome = "João Silva",
          
        },

        new Musico
        {
            nome = "Eduardo Queiroz",
 
        }
        };
            
        }

        public async Task searchContacts()
        {

            if (string.IsNullOrWhiteSpace(UserEntry))
            {
                FilteredSearchs = new ObservableCollection<string>(searchs);
            }
            else
            {
                var filtrados = searchs
                    .Where(i => i.ToString().ToLowerInvariant().Contains(UserEntry, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                FilteredSearchs = new ObservableCollection<string>(filtrados);


            }


        }

        public async Task Contacts()
        {
            if (musicos.Count.Equals(0))
            {
                HasContacts = false;
                NoContacts = true;
            }
            else
            {

                NoContacts = false;
                HasContacts = true;
                
            }
        }

    }
}
