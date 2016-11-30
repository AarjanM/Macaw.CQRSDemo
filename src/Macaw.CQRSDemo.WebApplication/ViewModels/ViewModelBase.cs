namespace Macaw.CQRSDemo.WebApplication.ViewModels
{
    public class ViewModelBase
    {
        public ViewModelBase()
        {
            Title = "CQRS Demo";
        }

        public string Title { get; set; }
    }
}