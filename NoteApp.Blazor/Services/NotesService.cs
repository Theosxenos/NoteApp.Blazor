using NoteApp.Blazor.Models;

namespace NoteApp.Blazor.Services;

public class NotesService
{
    private List<Note> notes = new();
    private Note activeNote;

    public Note ActiveNote
    {
        get => activeNote;
        set
        {
            if (activeNote == value) return;

            if (activeNote != null) activeNote.PropertyChanged -= ActiveNoteOnPropertyChanged;
            
            activeNote = value;
            activeNote.PropertyChanged += ActiveNoteOnPropertyChanged;
            ActiveNoteChanged?.Invoke();
        }
    }

    public event Action ActiveNoteChanged;
    public event Action NotePropertiesChanged;
    
    public List<Note> GetNotes() => notes;
    public List<Note> GetNotesFromNotebook(int notebookid)
    {
        var filterednotes = notes.Where(n => n.NotebookId == notebookid).ToList();

        if ((ActiveNote is null || ActiveNote.NotebookId != notebookid) && filterednotes.Count > 0)
        {
            ActiveNote = filterednotes[0];
        }
        
        return filterednotes;
    }

    public Note GetNote(int id) => notes.SingleOrDefault(n => n.NoteId == id);

    public List<Note> SearchNote(string searchterm) => notes.Where(n => n.Name.Contains(searchterm) || n.Content.Contains(searchterm)).ToList();

    public void CreateNote(Note note) => notes.Add(note);

    public void UpdateNote(Note updatednote)
    {
        var note = GetNote(updatednote.NoteId);
        note.Name = updatednote.Name;
        note.Content = updatednote.Content;
        note.NotebookId = updatednote.NotebookId;
    }

    public void DeleteNote(Note deletednote) => notes.Remove(deletednote);

    public void SetActiveNote(int? noteid = null)
    {
        ActiveNote = notes.SingleOrDefault(n => n.NoteId == noteid);
    }
    
    private void ActiveNoteOnPropertyChanged(object? sender, EventArgs e)
    {
        NotePropertiesChanged?.Invoke();
    }
}