using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        //Base ctor is always called unless specified
        public MovieForm () //: base()
        {
            //Init()
            InitializeComponent ();
        }

        public MovieForm (string title) : this()
        {

            //Handled by ctor chaining
            //InitializeComponent ();
            //Text = title;

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

            ValidateChildren ();
        }

        private void OnSave ( object sender, EventArgs e )
        {
            if (!ValidateChildren ())
                return;

            //object initializer syntax
            var movie = new Movie () {
                
                Title = _txtName.Text,
                Description = _txtDescription.Text,
                ReleaseYear = GetAsInt32 (_txtReleaseYear),
                RunLength = GetAsInt32 (_txtRunLength),
                Rating = _cbRating.Text,
                HasSeen = _cbHasSeen.Checked,
             };

            //validate
            if (!Validate (movie))
                return;

            
                
           
            //Save it
            Movie = movie;
            DialogResult = DialogResult.OK;
            Close ();
        }

        private bool Validate (IValidatableObject movie)
        {
            var results = ObjectValidator.TryValidateObject (movie);
            // var message = movie.Validate ();
            /*var results = new List<ValidationResult> ();
            var context = new ValidationContext (movie);
            if (!Validator.TryValidateObject (movie, context, results))
            {

            }
            var context = new ValidationContext (movie);
            //var results = movie.Validate (context); */
            if (results.Count() > 0)
            {
                foreach (var result in results)
                {
                   
                    MessageBox.Show (this, result.ErrorMessage, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                        
                };
                return false;
            };



            
            /*if (!String.IsNullOrEmpty (message))
            {
                MessageBox.Show (this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }; */

            return true; 
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

        private void OnValidatingName ( object sender, CancelEventArgs e )
        {
            var control = sender as TextBox;

            //Name is required
            if (control.Text == "")
            {
                e.Cancel = true;
                _errors.SetError (control, "Name is required");
            } else
            {
                _errors.SetError (control, "");
            }
                
        }

        private void OnValidatingReleaseYear ( object sender, CancelEventArgs e )
        {
            var control = sender as TextBox;

            var value = GetAsInt32 (control);
            //Name is required
            if (value <= 1900)
            {
                e.Cancel = true;
                _errors.SetError (control, "ReleaseYear must be <= 1900");    
            } else
            {
                _errors.SetError (control, "");
            }
        }

        private void OnValidatingRating ( object sender, CancelEventArgs e )
        {
            var control = sender as ComboBox;

            //Text is required
            if (control.SelectedIndex == -1)
            {
                e.Cancel = true;
                _errors.SetError (control, "Rating is required");
            } else
            {
                _errors.SetError (control, "");
            }
        }

        private void OnValidatingRunLength ( object sender, CancelEventArgs e )
        {
            var control = sender as TextBox;

            var value = GetAsInt32 (control);
            //Name is required
            if (value < 0)
            {
                e.Cancel = true;
                _errors.SetError (control, "Run Length must be >= 0");
            } else
            {
                _errors.SetError (control, "");
            }
        }
    }
}
