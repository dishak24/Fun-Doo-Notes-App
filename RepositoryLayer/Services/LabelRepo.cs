using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class LabelRepo : ILabelRepo
    {
        private readonly FundooDBContext context;

        public LabelRepo(FundooDBContext context)
        {
            this.context = context;
        }

        //For creating label
        //async only in method with body
        public async Task<LabelEntity> CreateLabelAsync(int UserId, string LabelName)
        {
            var result = context.Users.FirstOrDefault( u => u.UserId == UserId);

            if (result != null)
            {
                LabelEntity label = new LabelEntity
                {
                    LabelName = LabelName,
                    UserId = UserId
                };

                context.Labels.Add(label);
                await context.SaveChangesAsync();
                return label;

            }
            else
            {
                return null;
            }
        }

        //To get all labels
        public async Task<List<LabelEntity>> GetLabelsAsync(int userId)
        {
            //var result = context.Users.FirstOrDefault(u => u.UserId == userId);
            return await context.Labels.Where(u => u.UserId == userId).ToListAsync();
        }

        // Assign Label to a Note
        public async Task<NoteLabelEntity> AssignLableAsync(int NoteId, int lableId)
        {
            var Note = await context.Notes.FindAsync(NoteId);
            var Lable = await context.Labels.FindAsync(lableId);

            if (Note == null || Lable == null)
            {
                return null;
            }
            else
            {
                NoteLabelEntity addLableToNote = new NoteLabelEntity { NoteId = NoteId, LabelId = lableId };
                context.NoteLabels.Add(addLableToNote);
                await context.SaveChangesAsync();
                return addLableToNote;
            }
        }

        public async Task<LabelEntity> UpdateLabelAsync(int userId, int labelId, string newLabelName)
        {
            var label = await context.Labels
                .FirstOrDefaultAsync(l => l.LabelId == labelId && l.UserId == userId);

            if (label != null)
            {
                label.LabelName = newLabelName;
                await context.SaveChangesAsync();
                return label;
            }
            else
            {
                return null;
            }
        }


        //delete label
        public async Task<bool> DeleteLabelAsync(int userId, int labelId)
        {
            var label = await context.Labels
                .FirstOrDefaultAsync(l => l.LabelId == labelId && l.UserId == userId);

            if (label != null)
            {
                context.Labels.Remove(label);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }




        //To Remove label from Note
        public async Task<bool> RemoveLabelFromNoteAsync(int noteId, int labelId)
        {
            var noteLabel = await context.NoteLabels.FindAsync(noteId, labelId);
            if (noteLabel != null)
            {
                context.NoteLabels.Remove(noteLabel);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
