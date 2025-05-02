using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class NotesModel
    {
        //[Required(ErrorMessage = "Title is required !!")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
    }
}
