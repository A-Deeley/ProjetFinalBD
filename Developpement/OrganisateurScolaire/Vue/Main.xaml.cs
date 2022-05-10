﻿using System;
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
using OrganisateurScolaire.ViewModels;
using Syncfusion.Licensing;
using Syncfusion.Windows;

namespace OrganisateurScolaire.Vue
{
    /// <summary>
    /// Logique d'interaction pour Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            MainViewModel vmDatacontext = new();
            DataContext = vmDatacontext;
        }
    }
}
