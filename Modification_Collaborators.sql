-- Drop existing foreign key if it doesn't cascade
ALTER TABLE Collaborators
DROP CONSTRAINT FK_Collaborators_Notes_NoteId;


-- Recreate with ON DELETE CASCADE
ALTER TABLE Collaborators
ADD CONSTRAINT FK_Collaborators_Notes_NoteId
FOREIGN KEY (NoteId) REFERENCES Notes(NotesId) ON DELETE CASCADE;


SELECT 
    f.name AS ForeignKey,
    delete_referential_action_desc AS OnDeleteAction
FROM sys.foreign_keys AS f
JOIN sys.foreign_key_columns AS fc ON f.object_id = fc.constraint_object_id
WHERE OBJECT_NAME(f.parent_object_id) = 'Collaborators';


-----------------------------------------------------------------------------------------------

ALTER TABLE NoteLabels
DROP CONSTRAINT FK_NoteLabels_Labels_LabelId;

ALTER TABLE NoteLabels
ADD CONSTRAINT FK_NoteLabels_Labels_LabelId
FOREIGN KEY (LabelId) REFERENCES Labels(LabelId)
ON DELETE CASCADE;
