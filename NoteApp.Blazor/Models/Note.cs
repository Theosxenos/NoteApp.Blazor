namespace NoteApp.Blazor.Models;

public class Note
{
    public int NoteId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int NotebookId { get; set; }
}
