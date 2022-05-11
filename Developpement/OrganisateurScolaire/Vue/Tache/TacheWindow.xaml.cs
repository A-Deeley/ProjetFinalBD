using OrganisateurScolaire.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OrganisateurScolaire.Vue.Tache
{
    /// <summary>
    /// Logique d'interaction pour TacheWindow.xaml
    /// </summary>
    public partial class TacheWindow : Window
    {
        public TacheWindow()
        {
            InitializeComponent();
            DetailTacheViewModel vmDatacontext = new();
            DataContext = vmDatacontext;
        }
    }
}
