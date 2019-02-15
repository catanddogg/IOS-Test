using SQLite;

namespace IllyaVirych.Core.Services
{
    [Table("TaskItem")]
    public class TaskItem
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string NameTask { get; set; }
        public string DescriptionTask { get; set; }
        public bool StatusTask { get; set; }
        public string UserId { get; set; }
        public double LalitudeMarker { get; set; }
        public double LongitudeMarker { get; set; }


        public TaskItem(int id, string nameTask, string descriptionTask, bool statusTaask, string userId, double lalitudeMarker, double longitudeMarker)
        {
            Id = id;
            NameTask = nameTask;
            DescriptionTask = descriptionTask;
            StatusTask = statusTaask;
            UserId = userId;
            LalitudeMarker = lalitudeMarker;
            LongitudeMarker = longitudeMarker;
        }
        public TaskItem()
        {
        }
    }
}
