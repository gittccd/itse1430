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

        public Movie Movie;

        private void BtnSave_Click ( object sender, EventArgs e )
        {
            var movie = new Movie ();
            movie.title = _txtName.Text;
            movie.description = _txtDescription.Text;
            movie.releaseYear = GetAsInt32 (_txtReleaseYear);
            movie.runLength = GetAsInt32 (_txtRunLength);
            movie.rating = _cbRating.Text;
            movie.hasSeen = _cbHasSeen.Checked;

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
