using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class CollaboratorsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollaboratorId { get; set; }
        public string Email { get; set; }

        [ForeignKey("NoteUser")]
        public int UserId { get; set; }

        // [JsonIgnore]--BCoz we want only UserId column.
        // If we not write JsonIgnote then it takes all coloumns from UserEntity that we dont want.
        [JsonIgnore]
        public virtual UserEntity NoteUser { get; set; }



        [ForeignKey("CollaborateNote")]
        public int NoteId { get; set; }

        // [JsonIgnore]--BCoz we want only NoteId column.
        // If we not write JsonIgnote then it takes all coloumns from NoteEntity that we dont want.
        [JsonIgnore]
        public virtual NotesEntity CollaborateNote { get; set; }
    }
}
