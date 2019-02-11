using MvvmCross.Plugin.Messenger;


namespace IllyaVirych.Core.Messenger
{
    public class MapMessenger : MvxMessage
    {
        public double LalitudeMarkerResult { get; private set; }
        public double LongitudeMarkerResult { get; private set; }
        public string NameTaskResult { get; private set; }
        public string DescriptionTaskResult { get; private set; }
        public bool StatusTaskResult { get; private set; }
        public int IdTask { get; private set; }

        public MapMessenger(object sender, int idTask, double lalitudeMarkerResult, double longitudeMarkerResult,
            string nameTaskResult, string descriptionTaskResult, bool statusTaskResult)
            :base(sender)
        {
            IdTask = idTask;
            LalitudeMarkerResult = lalitudeMarkerResult;
            LongitudeMarkerResult = longitudeMarkerResult;
            NameTaskResult = nameTaskResult;
            DescriptionTaskResult = descriptionTaskResult;
            StatusTaskResult = statusTaskResult;
        }
    }      
}
