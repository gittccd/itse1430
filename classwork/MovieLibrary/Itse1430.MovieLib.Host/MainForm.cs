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

        //private string OrderByTitle (Movie movie)
        //{
        //    return movie.Title;
        //}

        //private int OrderByReleaseYear (Movie movie)
        //{
        //    return movie.ReleaseYear;
        //}

        private void UpdateUI() 
        {
            var movies = from m in _movies.GetAll ()
                         orderby m.Title, m.ReleaseYear
                         select m;

           /* var movies = _movies.GetAll ()
            
                                //.OrderBy (OrderByTitle)
                                .OrderBy (m => m.Title)
                                //.ThenBy (OrderByReleaseYear);
                                .ThenBy (m => m.ReleaseYear); */


            PlayWithEnumerable (movies);

     

            
            //For more complex bindings
           
            _listMovies.DataSource = movies.ToArray();
        }

        private void PlayWithEnumerable ( IEnumerable<Movie> movies )
        {
            Movie firstOne = movies.FirstOrDefault ();
            Movie lastOne = movies.LastOrDefault ();
            //Movie onlyOne = movies.SingleOrDefault();

            /*var coolMovies = movies.Where (m => m.ReleaseYear > 1979
                                                 && m.ReleaseYear < 2000); */

            int id = 1;
            var temp1 = new NestedType { id = id };
            var otherMovies = movies.Where (temp1.WhereCondition);
            var lastId = id;
        }

        private sealed class NestedType
        {
            public int id { get; set; }

            public bool WhereCondition (Movie m)
            {
                return m.Id > ++id;
            }
                  
        
        }


        private IMovieDatabase _movies;

        private Movie GetSelectedMovie()
        {
            var item = _listMovies.SelectedItem;
            //if (item == null)
            //    return null;
            //movie or null
            return item as Movie;

            //var firstMovie = _listMovies.SelectedItems.OfType<Movie> ()
            //                                         .OfType<Movie> ()
            //                                         .FirstOrDefault ();
           
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

            //seed movies
            _movies = new MemoryMovieDatabase ();
            var count = _movies.GetAll ().Count ();
            if (count == 0)
                //MovieDatabaseExtensions.Seed (_movies);
                _movies.Seed ();

            UpdateUI ();
        }

        private void OnFileExit ( object sender, EventArgs e )
        {
            Close ();
        }
    }
}
