using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApp2
{
    class Constants
    {
        public static Form1 form;
        public static bool IsButtonActive = false;
        public const bool online = false;
        public const string gmRequestUri = "/api/gamecontroller/";
        public const string requestUri = "/api/player/";
        public const string mediaType = "application/json";
    }
}
