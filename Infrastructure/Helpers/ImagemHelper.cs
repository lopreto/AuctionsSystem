using AuctionsSystem.Infrastructure.Enums;
using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace AuctionsSystem.Infrastructure.Helpers
{
    public static class ImagemHelper
    {
        public static string Save(ImageType imageType, HttpServerUtilityBase server, HttpPostedFileBase imageFile)
        {
            string imagePath = string.Empty;
            string path = string.Empty;

            switch (imageType)
            {
                case ImageType.Perfil:
                    path = ConfigurationManager.AppSettings["Perfil"];
                    break;
                case ImageType.Productos:
                    path = ConfigurationManager.AppSettings["Productos"];
                    break;
                default:
                    break;
            }

            string pathMaped = server.MapPath(path);
            if (!Directory.Exists(pathMaped))
            {
                Directory.CreateDirectory(pathMaped);
            }

            if (imageFile != null)
            {
                string fileName = $"{DateTime.Now.Ticks}.jpg";
                imagePath = path + fileName;
                imageFile.SaveAs(pathMaped + fileName);
            }
            return imagePath;
        }
    }
}
