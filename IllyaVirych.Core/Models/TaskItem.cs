using SQLite;

namespace IllyaVirych.Core.Services
{
    [Table("TaskItem")]
    public class TaskItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NameTask { get; set; }
        public string DescriptionTask { get; set; }
        public bool StatusTask { get; set; }
        public string UserId { get; set; }
        public double LalitudeGoogleMarker { get; set; }
        public double LongitudeGoogleMarker { get; set; }


        public TaskItem(int id, string nameTask, string descriptionTask, bool statusTaask, string userId, double lalitudeGoogleMarker, double longitudeGoogleMarker)
        {
            Id = id;
            NameTask = nameTask;
            DescriptionTask = descriptionTask;
            StatusTask = statusTaask;
            UserId = userId;
            LalitudeGoogleMarker = lalitudeGoogleMarker;
            LongitudeGoogleMarker = longitudeGoogleMarker;
        }
        public TaskItem()
        {

        }
    }
}
