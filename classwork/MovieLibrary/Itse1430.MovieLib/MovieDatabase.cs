using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Itse1430.MovieLib
{
    public abstract class MovieDatabase : IMovieDatabase
    {


        public Movie Add ( Movie movie )
        {
            //TODO: validation
            //if (movie == null)
            //  return null;
            //throw new Exception ("Movie is null");
            if (movie == null)
                throw new ArgumentNullException (nameof(movie));

            //if (!String.IsNullOrEmpty (movie.Validate ()))
            //var context = new ValidationContext (movie);

            //var results = movie.Validate (context);

            var results = ObjectValidator.TryValidateObject (movie);

            if (results.Count () > 0)
                //return null;
                throw new ValidationException (results.FirstOrDefault().ErrorMessage);

            //name must be unique
            var existing = GetByNameCore (movie.Title);
            if (existing != null)
                //return null;
                throw new ArgumentException ("Movie must be unique");

            return AddCore (movie);
        }

        /// <summary>
        /// add movie to database
        /// </summary>
        /// <param name="movie"></param>
        /// <returns>updated movie</returns>
        /// 
        protected abstract Movie AddCore ( Movie movie );
        public void Remove ( int id )
        {
            //validate the id
            if (id <= 0)
                throw new ArgumentOutOfRangeException (nameof(id), "Id must be > 0.");

            RemoveCore (id);
          
        }

        protected abstract void RemoveCore ( int id );

        public Movie Get ( int id )
        {
            //TODO: validate
            if (id <= 0)
                throw new ArgumentOutOfRangeException (nameof (id),
                                                       "Id must be > 0.");

            return GetCore (id);
            
        }

        protected abstract Movie GetCore ( int id );

        public IEnumerable<Movie> GetAll ()
        {
            => GetAllCore () ?? Enumerable.Empty<Movie> ();
            
        }

        protected abstract IEnumerable<Movie> GetAllCore ();

        public void Update ( int id, Movie newMovie )
        {
            //validate parameters
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0.");
            if (newMovie == null)
                throw new ArgumentNullException(nameof(newMovie));

            //if (!String.IsNullOrEmpty(newMovie.Validate()))
            var results = ObjectValidator.TryValidateObject(newMovie);
           
            if (results.Count () > 0)
                throw new ValidationException(results.FirstOrDefault().ErrorMessage);

            var existing = GetByNameCore (newMovie.Title);
            if (existing != null && existing.Id != id)
                throw new ArgumentException("Movie must be unique.");

            try
            {
                UpdateCore (id, newMovie);
            } catch (IOException ex)
            {
                throw new Exception ("An error occurred updating the movie.", ex);
            };
           
        }

        protected abstract Movie UpdateCore ( int id, Movie newMovie );



        protected abstract Movie GetByNameCore ( string name );
                  
        
    }
}
