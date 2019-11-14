using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itse1430.MovieLib
{
    /// <summary>Represents movie data.</summary>
    /// <remarks></remarks>
    //[Required]
    public class Movie : IValidatableObject
    {

        public int Id { get; set; }

        /// <summary>Gets or sets the title of the movie.</summary>///
       
        [RequiredAttribute(AllowEmptyStrings = false)]
        public string Title
        {
            //null coalescing
            get { return _title ?? ""; }
            set { _title = value; }
        }

        public string Description
        {
            // get { return _description ?? ""; }
            get => _description ?? "";
            set => _description = value;
        }

        [Required(AllowEmptyStrings = false)]
        public string Rating
        {
            get => _rating ?? "";
            //set { _rating = value; }
            set => _rating = value;
        }

        [Display(Name = "Release Year")]
        [Range(1900, Int32.MaxValue, ErrorMessage = "Release year must be >= 1900")]
        public int ReleaseYear { get; set; } = 1900; //Auto property
        /*{
            get { return _releaseYear; }
            set { _releaseYear = value; }
        }*/


        //Full property...
        [RangeAttribute(0, Int32.MaxValue, ErrorMessage = "Run length must be >= 0")]
        public int RunLength { get; set; }
        /*{
            get { return _runLength; }
            set { _runLength = value; }
        }*/

        public bool HasSeen { get; set; }
        /*{
            get { return _hasSeen; }
            set { _hasSeen = value; }
        }*/

        //value is 1939, read only, public
        //public int ReleaseYearForColor {get; } = 1939;
        public const int ReleaseYearForColor = 1939;
        //private readonly int _releaseYearForColor = 1939;
        public bool IsBlackAndWhite
            => ReleaseYear <= ReleaseYearForColor;
        /*public bool IsBlackAndWhite2
            => ReleaseYear <= ReleaseYearForColor; */


        //Mixed accessibility - property must be most visible
        [Obsolete("Do not use", true)]
        public string TestAccessibility
        {
            get => "";
            private set {}
        }

        //never make fields public!!
        private string _title = "";
        private string _description = "";
        //private int _releaseYear = 1900;
        private string _rating = "";
        //private bool _hasSeen;
        //private int _runLength;

        /// <summary>
        /// Validates the movie.</summary>
        /// <returns>An error msg if validation fails or empty string otherwise.</returns>
        /// 
        public override string ToString ()
            => $"{Title} ({ReleaseYear})";


        /*public string Validate()
        {
            var title = "";
            //name is required
            if (String.IsNullOrEmpty (this.Title))
                return "Title is required.";
            //releaseyear >= 1900
            if (ReleaseYear < 1900)
                return "Release Year must be >= 1900.";
            //run length >= 0
            if (RunLength < 0)
                return "Run Length must be >= 0";
            //rating is required
            if (String.IsNullOrEmpty (_rating))
                return "Rating is required";
            return "";
        }*/

        public IEnumerable<ValidationResult> Validate ( ValidationContext validationContext )
        {
            
            return Enumerable.Empty<ValidationResult> ();

            
        }

#if DEBUG
        private void Foo ()
        {
        }
#endif

        /*#region Private Members

        //Fields - data to be stored
        //Never make fields public!!
        private string _title = "";
        private string _description = "";
        private string _rating = "";

        #endregion        */
       
        
    }

    
}
