using OrganisateurScolaire.DAL.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.DAL
{
    public class DAL
    {
        private CategorieFactory _categorieFactory;
        private RappelFactory _rappelFactory;
        private TacheFactory _tacheFactory;

        /// <summary>
        /// Gets the categorie factory instance.
        /// </summary>
        /// <returns>The categorie factorie instance.</returns>
        public CategorieFactory CategorieFactory()
        {
            if (_categorieFactory is null)
                _categorieFactory = new CategorieFactory();

            return _categorieFactory;
        }


        /// <summary>
        /// Gets the rappel factory instance.
        /// </summary>
        /// <returns>The rappel factorie instance.</returns>
        public RappelFactory RappelFactory()
        {
            if (_rappelFactory is null)
                _rappelFactory = new RappelFactory();

            return _rappelFactory;
        }

        /// <summary>
        /// Gets the tache factory instance.
        /// </summary>
        /// <returns>The tache factorie instance.</returns>
        public TacheFactory TacheFactory()
        {
            if (_tacheFactory is null)
                _tacheFactory = new TacheFactory();

            return _tacheFactory;
        }
    }
}
