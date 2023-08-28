namespace DevStudyNotes.API.Models
{
    public class AddStudyNoteInputModel
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public bool IsPublic { get; set; }
    }
}
