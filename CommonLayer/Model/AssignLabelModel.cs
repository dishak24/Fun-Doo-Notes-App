using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace CommonLayer.Model
{
    //request models
    public class AssignLabelModel
    {
        //[Required(ErrorMessage = "Note Id is required !!")]
        //[JsonPropertyName("noteId")]
        public int NoteId { get; set; }

        //[Required(ErrorMessage = "Label Id is required !!")]
        //[JsonPropertyName("labelId")]
        public int LabelId { get; set; }

    }
}
