namespace NoteApp.Blazor;

public class Notebook
{
    public int NotebookId { get; set; }
    public string Name { get; set; }
    public List<Note> Notes { get; set; } = new List<Note>();
    public bool IsActive { get; set; } = false;

    public Notebook()
    {
        
    }

    public Notebook(string name)
    {
        Name = name;
    }

}
