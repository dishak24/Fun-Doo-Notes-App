using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRepo
    {
        //For creating label
        public Task<LabelEntity> CreateLabelAsync(int UserId, string LabelName);

        //To get all labels
        public Task<List<LabelEntity>> GetLabelsAsync(int userId);

        // Assign Label to a Note
        public Task<NoteLabelEntity> AssignLableAsync(int NoteId, int lableId);

        //To Remove label from Note
        public Task<bool> RemoveLabelFromNoteAsync(int noteId, int labelId);
    }
}
