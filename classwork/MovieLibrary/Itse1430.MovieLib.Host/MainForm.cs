using System;
using System.Windows.Forms;

namespace Itse1430.MovieLib.Host
{
    public partial class MainForm : Form
    {
        public MainForm ()
        {
            //int x = 10;
            InitializeComponent ();

            //Itse1430.MovieLib.Movie
            Movie movie = new Movie ();
            movie.Title = "Jaws";
            movie.Description = movie.Title;  //??
        }

        private void ToolStripSeparator1_Click ( object sender, EventArgs e )
        {

        }

        private void OnMovieAdd ( object sender, EventArgs e )
        {
            var form = new MovieForm ();

            //modeless :does not block main window
            //form.Show ();

            //show the new movie form modally
            if (form.ShowDialog (this) == DialogResult.OK)
                AddMovie (form.Movie);
            UpdateUI ();
        }

        private void UpdateUI()
        {
            var movies = GetMovies ();
            _listMovies.Items.AddRange (movies);
        }


        private void AddMovie(Movie movie)
        {
            //Add to array
            for (var index = 0; index < _movies.Length; ++index)
            {
                if (_movies[index] == null)
                {
                    _movies[index] = movie;
                    return;
                }
            }
        }

        private void RemoveMovie(Movie movie)
        {
            //Remove from array
            for (var index = 0; index < _movies.Length; ++index)
            {
                //this won't work
                if (_movies[index] == movie)
                {
                    _movies[index] = null;
                    return;
                }
            }
        }

        private Movie[] GetMovies()
        {
            //TODO: Filter out empty movies
            return _movies;
        }

        private Movie[] _movies = new Movie[100];

        private Movie GetSelectedMovie()
        {
            return _movies[0];
        }

        private void OnHelpAbout ( object sender, EventArgs e )
        {
            var form = new AboutBox1 ();
            form.ShowDialog (this);
        }


        private void OnMovieEdit ( object sender, EventArgs e )
        {
            var movie = GetSelectedMovie ();
            if (movie == null)
                return;

            var form = new MovieForm ();
            form.Movie = movie;

            if (form.ShowDialog (this) == DialogResult.OK)
            {
                RemoveMovie (movie);
                AddMovie (form.Movie);
                UpdateUI ();
            }
                 
        }



        private void OnMovieDelete ( object sender, EventArgs e )
        {
            var movie = GetSelectedMovie ();
            if (movie == null)
                return;

            //confirmation
            var msg = $"Are you sure you want to delete {movie.Title}?";
            var result = MessageBox.Show (msg, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            //Delete it
            RemoveMovie (movie);
            UpdateUI ();

        }

        private void OnFileExit ( object sender, EventArgs e )
        {
            Close ();
        }
    }
}
