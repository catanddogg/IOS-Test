using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace IllyaVirych.Core.Messenger
{
    public class GoogleMapMessenger : MvxMessage
    {
        public GoogleMapMessenger(object sender, int idTask, double lalitudeGoogleMarkerResult, double longitudeGoogleMarkerResult,
            string nameTaskResult, string descriptionTaskResult, bool statusTaskResult)
            :base(sender)
        {
            IdTask = idTask;
            LalitudeGoogleMarkerResult = lalitudeGoogleMarkerResult;
            LongitudeGoogleMarkerResult = longitudeGoogleMarkerResult;
            NameTaskResult = nameTaskResult;
            DescriptionTaskResult = descriptionTaskResult;
            StatusTaskResult = statusTaskResult;
        }

        public double LalitudeGoogleMarkerResult { get; private set; }
        public double LongitudeGoogleMarkerResult { get; private set; }
        public string NameTaskResult { get; private set; }
        public string DescriptionTaskResult { get; private set; }
        public bool StatusTaskResult { get; private set; }
        public int IdTask { get; private set; }
    }      

}
