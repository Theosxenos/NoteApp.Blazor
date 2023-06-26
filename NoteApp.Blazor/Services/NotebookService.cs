using NoteApp.Blazor.Models;

namespace NoteApp.Blazor.Services;

public class NotebookService
{
	private List<Notebook> Notebooks { get; set; } = new();
	private int lastNotebookId = -1;
	private Notebook activeNotebook;

	public Action ActiveNotebookChanged;
	public Notebook ActiveNotebook
	{
		get => activeNotebook;
		set
		{
			if (activeNotebook == value) return;
			
			activeNotebook = value;
			ActiveNotebookChanged?.Invoke();
		}
	}

	public int NotebookAmount => Notebooks.Count;

	public NotebookService()
	{
		for (int i = 0; i < 3; i++)
		{
			Notebooks.Add(new Notebook() { NotebookId = i, Name = $"Notebook {i}" });
		}

		ActiveNotebook = Notebooks[0];
	}

	public List<Notebook> GetNotebooks()
	{
		return Notebooks;
	}

	public void AddNewNotebook(string name)
	{
		if (lastNotebookId == -1)
		{
			lastNotebookId = Notebooks.Last().NotebookId;
		}
		Notebook newNotebook = new Notebook() { Name = name, NotebookId = ++lastNotebookId };
		Notebooks.Add(newNotebook);
	}
}
