using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public AccueilViewModel Accueil { get; init; }
        public TachesViewModel Taches { get; init; }

        public MainViewModel()
        {
            Accueil = new();
            Taches = new();
        }
    }
}
