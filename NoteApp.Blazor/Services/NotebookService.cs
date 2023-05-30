namespace NoteApp.Blazor.Services;

public class NotebookService
{
	private List<Notebook> Notebooks { get; set; } = new List<Notebook>();

    public Notebook ActiveNotebook { get; set; }

    public NotebookService()
    {
        for(int i = 0; i < 3; i++)
        {
            Notebooks.Add(new Notebook() { NotebookId = i, Name = $"Notebook {i}"});
        }

        ActiveNotebook = Notebooks[0];
        Notebooks[0].IsActive = true;
    }

    public Task<List<Notebook>> GetNotebooksAsync()
    {
        return Task.FromResult(Notebooks);
    }
}
