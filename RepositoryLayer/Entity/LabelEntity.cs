using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class LabelEntity
    {
        //P
        [Key]
        //identity(1,1)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }
        public string LabelName { get; set; } = string.Empty;

        //FK
        // Correct Foreign Key Reference
        public int UserId { get; set; }

        [JsonIgnore] // Prevents circular reference
        public UserEntity User { get; set; }

        // Many-to-Many Relationship with Notes
        [JsonIgnore] // Prevents circular reference
        public ICollection<NoteLabelEntity> NoteLabels { get; set; } = new List<NoteLabelEntity>();
    }
}
