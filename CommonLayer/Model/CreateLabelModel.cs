using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text;

namespace CommonLayer.Model
{
    //request models
    public class CreateLabelModel
    {
        [Required(ErrorMessage = "Label Name is required !!")]
        public string LabelName { get; set; } = string.Empty;
    }
}
