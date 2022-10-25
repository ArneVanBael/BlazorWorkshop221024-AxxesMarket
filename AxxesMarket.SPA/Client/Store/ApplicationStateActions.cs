using BlazorState;

namespace AxxesMarket.SPA.Client.Store;

public partial class ApplicationState
{
    public class LoadInitialApplicationStateAction: IAction
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string ApplicationName { get; set; }

        public LoadInitialApplicationStateAction(string userName, string userEmail, string applicationName)
        {
            UserName = userName;
            UserEmail = userEmail;
            ApplicationName = applicationName;
        }
    }

    public class ToggleSidebarMenuAction : IAction
    {
    }

    public class OpenCloseModalAction: IAction
    {

        public bool IsOpen;

        public OpenCloseModalAction(bool isOpen)
        {
            IsOpen = isOpen;
        }
    }
}
