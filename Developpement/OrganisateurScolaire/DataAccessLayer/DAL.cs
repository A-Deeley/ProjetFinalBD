using OrganisateurScolaire.DataAccessLayer.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.DataAccessLayer
{
    public class DAL
    {
        private CategorieFactory _categorieFactory;
        private RappelFactory _rappelFactory;
        private TacheFactory _tacheFactory;
        private CoursFactory _coursFactory;
        private ProgrammeFactory _programmeFactory;
        private SessionFactory _sessionFactory;

        /// <summary>
        /// Gets the categorie factory instance.
        /// </summary>
        /// <returns>The categorie factory instance.</returns>
        public CategorieFactory CategorieFactory()
        {
            if (_categorieFactory is null)
                _categorieFactory = new CategorieFactory();

            return _categorieFactory;
        }


        /// <summary>
        /// Gets the rappel factory instance.
        /// </summary>
        /// <returns>The rappel factory instance.</returns>
        public RappelFactory RappelFactory()
        {
            if (_rappelFactory is null)
                _rappelFactory = new RappelFactory();

            return _rappelFactory;
        }

        /// <summary>
        /// Gets the tache factory instance.
        /// </summary>
        /// <returns>The tache factory instance.</returns>
        public TacheFactory TacheFactory()
        {
            if (_tacheFactory is null)
                _tacheFactory = new TacheFactory();

            return _tacheFactory;
        }

        /// <summary>
        /// Gets the session factory instance.
        /// </summary>
        /// <returns>The session factory instance.</returns>
        public SessionFactory SessionFactory()
        {
            if (_sessionFactory is null)
                _sessionFactory = new SessionFactory();

            return _sessionFactory;
        }

        /// <summary>
        /// Gets the cours factory instance.
        /// </summary>
        /// <returns>The cours factory instance.</returns>
        public CoursFactory CoursFactory()
        {
            if (_coursFactory is null)
                _coursFactory = new CoursFactory();

            return _coursFactory;
        }

        /// <summary>
        /// Gets the programme factory instance.
        /// </summary>
        /// <returns>The programe factory instance.</returns>
        public ProgrammeFactory ProgrammeFactory()
        {
            if (_programmeFactory is null)
                _programmeFactory = new ProgrammeFactory();

            return _programmeFactory;
        }
    }
}
