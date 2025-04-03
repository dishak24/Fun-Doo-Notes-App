using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    //request models
    public class AssignLabelModel
    {
        [Required(ErrorMessage = "Note Id is required !!")]
        public int NoteId { get; set; }

        [Required(ErrorMessage = "Label Id is required !!")]
        public int LabelId { get; set; }

    }
}
