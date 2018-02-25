using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace FitnessApp.WEB.Helpers
{
    public class ImageHelper
    {
        private static readonly string imageFolder = WebConfigurationManager.AppSettings["ImageFolder"];
        public static string GetTrainingTemplateIconPath(string endPath)
        {   
            if(endPath != null)
                return $"{imageFolder}{endPath}";

            return null;
        }

        public static string GetAvatarPath(string endPath)
        {
            if (endPath != null)
                return $"{imageFolder}{endPath}";

            return $"{imageFolder}user.png";
        }
    }
}