using System.Windows;

namespace Models.ViewModels
{
    public class TomorrowViewModel : TaskViewModel
    {
        public TomorrowViewModel(TaskViewModel model, bool isFinished)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.PicturePath = model.PicturePath;
            this.Type = model.Type;
            this.Description = model.Description;
            this.Deadline = model.Deadline;
            this.IsFinished = isFinished ? Visibility.Hidden : Visibility.Visible;
        }

        public Visibility IsFinished { get; set; }
    }
}
