using NoteApp.Blazor.Models;

namespace NoteApp.Blazor.Services;

public class NotesService
{
    private List<Note> Notes { get; set; } = new();
    private Note activeNote;

    public Note ActiveNote
    {
        get => activeNote;
        set
        {
            if (activeNote == value) return;
            
            activeNote = value;
            ActiveNoteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public EventHandler ActiveNoteChanged;
    
    public List<Note> GetNotes() => Notes;
    public List<Note> GetNotesFromNotebook(int notebookid)
    {
        var notesFromNotebook = Notes.Where(n => n.NotebookId == notebookid).ToList();

        if (notesFromNotebook.Count > 0)
        {
            ActiveNote = notesFromNotebook[0];
            ActiveNoteChanged?.Invoke(this, EventArgs.Empty);
        }
        
        return notesFromNotebook;
    }

    public Note GetNote(int id) => Notes.SingleOrDefault(n => n.NoteId == id);

    public List<Note> SearchNote(string searchterm) => Notes.Where(n => n.Name.Contains(searchterm) || n.Content.Contains(searchterm)).ToList();

    public void CreateNote(Note note) => Notes.Add(note);

    public void UpdateNote(Note updatednote)
    {
        var note = GetNote(updatednote.NoteId);
        note.Name = updatednote.Name;
        note.Content = updatednote.Content;
        note.NotebookId = updatednote.NotebookId;
    }

    public void DeleteNote(Note deletednote) => Notes.Remove(deletednote);
}