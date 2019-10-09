using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itse1430.MovieLib
{
    public class MovieDatabase
    {
        public Movie Add ( Movie movie )
        {
            movie.Id = ++_id;
            _movies.Add (movie);

            return movie;
            //Add to array
            /*for (var index = 0; index < _movies.Count; ++index)
            {
                if (_movies[index] == null)
                {
                    _movies[index] = movie;
                    return;
                }
            }*/
        }

        public void Remove ( int id )
        {
            var movie = FindMovie (id);

            if (movie != null)
                _movies.Remove (movie);

            _movies.Remove (movie);
           
        }

        public Movie Get (int id)
        {
            return FindMovie (id);
        }

        public Movie[] GetAll ()
        {
            var index = 0;
            var movies = new Movie[_movies.Count];
            foreach (var movie in _movies)
                if (movie != null)
                    movies[index++] = movie;

            return movies;
        }

        public void Update(int id, Movie newMovie)
        {
            var existing = FindMovie (id);
            if (existing == null)
                return; //error

            existing.Description = newMovie.Description;
            existing.HasSeen = newMovie.HasSeen;
            existing.Rating = newMovie.Rating;
            existing.ReleaseYear = newMovie.ReleaseYear;
            existing.RunLength = newMovie.RunLength;
            existing.Title = newMovie.Title;
        }

        private Movie FindMovie(int id)
        {
            foreach (var movie in _movies)
                if (movie.Id == id)
                    return movie;
            return null;
        }

        //private Movie[] _movies = new Movie[100];
        private List<Movie> _movies = new List<Movie> ();

        private int _id = 0;

    }
}
