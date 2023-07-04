using NoteApp.Blazor.Models;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace NoteApp.Blazor.Services;

public class NotesService
{
	private List<Note> notes = new();
	private List<Note> Notes => notes;

	private Note activeNote;
	public Note ActiveNote
	{
		get => activeNote;
		set
		{
			if (activeNote == value) return;

			//if (activeNote != null) activeNote.PropertyChanged -= ActiveNoteOnPropertyChanged;

			activeNote = value;
			//activeNote.PropertyChanged += ActiveNoteOnPropertyChanged;
			//ActiveNoteChanged?.Invoke();
		}
	}

	public event Action ActiveNoteChanged;
	public event Action NotifyPropertyChanged;

	public List<Note> GetNotesFromNotebook(int notebookid)
	{
		var filterednotes = Notes.FindAll(n => n.NotebookId == notebookid);

		if ((ActiveNote is null || ActiveNote.NotebookId != notebookid) && filterednotes.Count > 0)
		{
			ActiveNote = filterednotes[0];
		}

		return filterednotes;
	}

	public Note GetNote(int id) => Notes.SingleOrDefault(n => n.NoteId == id);

	public List<Note> SearchNote(string searchterm = "")
	{
		searchterm = searchterm.Trim();
		if (string.IsNullOrEmpty(searchterm))
		{
			return Notes;
		}

		List<Note> filteredNotes;
		var haswildcard = searchterm.Contains("*");

		if (haswildcard)
		{
			var pattern = searchterm.Replace("*", ".?");
			filteredNotes = Notes.FindAll(n => Regex.IsMatch(n.Name, pattern) || Regex.IsMatch(n.Content, pattern));
		} else
		{
			filteredNotes = Notes.Where(n => n.Name.Contains(searchterm) || n.Content.Contains(searchterm)).ToList();
		}

		if(filteredNotes.Count > 0)
			ActiveNote = filteredNotes[0];

		return filteredNotes;
	}

	public void CreateNote(Note note)
	{
		notes.Add(note);
		//NotifyPropertyChanged?.Invoke();
	}

	public void UpdateNote(Note updatednote)
	{
		var note = GetNote(updatednote.NoteId);
		note.Name = updatednote.Name;
		note.Content = updatednote.Content;
		note.NotebookId = updatednote.NotebookId;

		NotifyPropertyChanged?.Invoke();
	}

	public void DeleteNote(Note deletednote) => notes.Remove(deletednote);

	public void SetActiveNote(int? noteid = null)
	{
		ActiveNote = notes.SingleOrDefault(n => n.NoteId == noteid);
	}

	private void ActiveNoteOnPropertyChanged(object? sender, EventArgs e)
	{
		NotifyPropertyChanged?.Invoke();
	}
}