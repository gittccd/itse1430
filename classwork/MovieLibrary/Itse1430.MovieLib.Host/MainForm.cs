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

            //Programmatic approach
            //_listMovies.Items.Clear ();
            //_listMovies.Items.AddRange (movies);

            //For more complex bindings
            _listMovies.DataSource = movies;
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
                };
            };
        }

        private Movie[] GetMovies()
        {
            //TODO: Filter out empty movies
            var count = 0;
            foreach (var movie in _movies)
                if (movie != null)
                    ++count;

            var index = 0;
            var movies = new Movie[count];
            foreach (var movie in _movies)
                if (movie != null)
                    movies[index++] = movie;

            return movies;
        }

        private Movie[] _movies = new Movie[100];

        private Movie GetSelectedMovie()
        {
            var item = _listMovies.SelectedItem;
            //if (item == null)
            //    return null;

            //movie or null
            return item as Movie;

            /*other approaches
            //C-style cast:  
            (Movie)item;

            //item is Movie;  E is T --> bool



            //old approach 1
            var tempVar = item as Movie;
            if (tempVar != null)...;

            //old approach 2
            if (item is Movie)
            {
                var i = (Movie)item;
                //do something with movie
            }
            //pattern matching
            if (item is Movie movie)
            {

            }; */
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
                //RemoveMovie (form.Movie);
                AddMovie (form.Movie);
                UpdateUI ();
            }
                 
        }



        private void OnMovieDelete ( object sender, EventArgs e )
        {
            //Demo
            var menuItem = sender as Button;
            //var text = menuItem.Text;

            //handle null
            var text = "";
            if (menuItem != null)
                text = menuItem.Text;
            else
                text = "";

            //as expression
            var text2 = (menuItem != null) ? menuItem.Text : "";

            //Null conditional operator
            var text3 = menuItem?.Text ?? "";

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
