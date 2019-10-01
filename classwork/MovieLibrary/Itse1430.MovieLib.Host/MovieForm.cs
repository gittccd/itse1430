﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Itse1430.MovieLib.Host
{
    public partial class MovieForm : Form
    {
        public MovieForm ()
        {
            InitializeComponent ();
        }

        public Movie Movie { get; set; }

        protected override void OnLoad ( EventArgs e )
        {
            //call base type
            //OnLoad(e);
            base.OnLoad (e);

            if (Movie != null)
            {
                _txtName.Text = Movie.Title;
                _txtDescription.Text = Movie.Description;
                _txtReleaseYear.Text = Movie.ReleaseYear.ToString ();
                _txtRunLength.Text = Movie.RunLength.ToString ();
                _cbRating.Text = Movie.Rating;
                _cbHasSeen.Checked = Movie.HasSeen;
            };
        }

        private void OnSave ( object sender, EventArgs e )
        {
            var movie = new Movie ();
            //movie.set_title(_txtName.Text);
            movie.Title = _txtName.Text;
            movie.Description = _txtDescription.Text;
            movie.ReleaseYear = GetAsInt32 (_txtReleaseYear);
            movie.RunLength = GetAsInt32 (_txtRunLength);
            movie.Rating = _cbRating.Text;
            movie.HasSeen = _cbHasSeen.Checked;

            //validate
            var message = movie.Validate ();
            if (!String.IsNullOrEmpty (message))
                return;
           
            //Save it
            Movie = movie;
            DialogResult = DialogResult.OK;
            Close ();
        }

        private int GetAsInt32(TextBox control)
        {
            if (Int32.TryParse (control.Text, out var result))
                return result;
            return 0;
        }

        private void _btnCancel_Click ( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close ();
        }
    }
}