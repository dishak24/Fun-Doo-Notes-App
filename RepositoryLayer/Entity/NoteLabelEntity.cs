using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class NoteLabelEntity
    {
        public int NoteId { get; set; }

        [JsonIgnore]
        public NotesEntity Note { get; set; } = null!;
        public int LabelId { get; set; }

        [JsonIgnore]
        public LabelEntity Label { get; set; } = null!;

    }
}
