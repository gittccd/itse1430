using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Itse1430.MovieLib.Host
{
    public partial class MainForm : Form
    {
        public MainForm ()
        {
        
            InitializeComponent ();
           
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
                _movies.Add (form.Movie);
            UpdateUI ();
        }

        private void UpdateUI()
        {
            var movies = _movies.GetAll ();

            //var movie = movies[0];
            //movie.Title = "Bob";

            //Programmatic approach
            //_listMovies.Items.Clear ();
            //_listMovies.Items.AddRange (movies);

            //For more complex bindings
           
            _listMovies.DataSource = movies.ToArray();
        }

        private IMovieDatabase _movies;

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
                _movies.Update (movie.Id, form.Movie);
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
            _movies.Remove (movie.Id);
            UpdateUI ();
        }

        protected override void OnLoad ( EventArgs e )
        {
            base.OnLoad (e);
            _movies = new MemoryMovieDatabase ();
            var count = _movies.GetAll ().Count ();
            if (count == 0)
                MovieDatabaseExtensions.Seed (_movies);

            UpdateUI ();
        }

        private void OnFileExit ( object sender, EventArgs e )
        {
            Close ();
        }
    }
}
