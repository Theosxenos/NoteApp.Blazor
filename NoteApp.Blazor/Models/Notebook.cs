namespace NoteApp.Blazor.Models;

public class Notebook
{
    public int NotebookId { get; set; }
    public string Name { get; set; }
    public List<Note> Notes { get; set; } = new List<Note>();

    public Notebook()
    {
        
    }

    public Notebook(string name)
    {
        Name = name;
    }

}
