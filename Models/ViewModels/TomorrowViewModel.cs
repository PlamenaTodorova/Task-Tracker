using System.Windows;

namespace Models.ViewModels
{
    public class TomorrowViewModel : TaskViewModel
    {
        public TomorrowViewModel(TaskViewModel model, bool isFinished)
            :base(model)
        {
            this.IsFinished = isFinished ? Visibility.Hidden : Visibility.Visible;
        }

        public Visibility IsFinished { get; set; }
    }
}
