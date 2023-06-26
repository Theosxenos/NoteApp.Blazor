using System.ComponentModel;

namespace NoteApp.Blazor.Models;

public class Note
{
    private string name = string.Empty;
    private string content = string.Empty;
    public int NoteId { get; set; }

    public string Name
    {
        get => name;
        set
        {
            if(name == value) return;
            
            name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string Content
    {
        get => content;
        set
        {
            if (content == value) return;
            
            content = value;
            OnPropertyChanged(nameof(Content));
        }
    }

    public int NotebookId { get; set; }

    public event EventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyname)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
    }
}
