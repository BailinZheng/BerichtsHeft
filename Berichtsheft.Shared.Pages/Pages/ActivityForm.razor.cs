using BerichtsHeft.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace BerichtsHeft.Client.Pages
{
    public class ActivityFormClass : ComponentBase
    {
        [Inject]
        public NavigationManager NavMan { get; set; }
        [Parameter]
        public string ActivityID { get; set; }
        public Activity Datei = new Activity();

        public void activityanmelden()
        {
            Activity.Add(Datei);
            NavMan.NavigateTo("dateitresor");
        }
        public void changeanmelden()
        {
            NavMan.NavigateTo("dateitresor");
        }
        protected override void OnInitialized()
        {
            //ActivityIndex = ActivityIndex ?? null;

            if (ActivityID != null)
            {
                Datei = Activity.GetDateiInfo(ActivityID);
            }
        }
    }
}
