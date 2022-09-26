using BerichtsHeft.DataAccess;
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

        
        public void changeanmelden()
        {
            NavMan.NavigateTo("dateitresor");
        }
        public void SaveToDb()
        {
            BerichtsHeft.DataAccess.Db.InsertActivityTable(
            Datei.ID, Datei.HauptText, Datei.WochenTag, Datei.Name, Datei.Fach,
            Datei.AbgabeType, Datei.DateBlock, Datei.DauertMin, Datei.DateOfReport);
            NavMan.NavigateTo($"dateitresor");
        }
        protected override void OnInitialized()
        {
            //ActivityIndex = ActivityIndex ?? null;

            if (ActivityID != null)
            {
                Datei = Db.GetActivity(ActivityID);
            }
        }
    }
}
