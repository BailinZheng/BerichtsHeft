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
        public string ActivityIndex { get; set; }
        public DateiInfo Datei = new DateiInfo();

        public void activityanmelden()
        {
            DateiInfo.Activities.Add(Datei);
            NavMan.NavigateTo("dateitresor");
        }
        protected override void OnInitialized()
        {
            //ActivityIndex = ActivityIndex ?? null;

            if (ActivityIndex != null)
            {
                Datei = DateiInfo.GetDateiInfo(ActivityIndex);
            }
        }
    }
}
