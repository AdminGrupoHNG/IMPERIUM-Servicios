using System;
using System.Threading.Tasks;

namespace UI_Servicios.Tools
{
    static class ToolHelper
    {
        public static string downloadsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\";
        public static string nameExcelFile = "Propuesta_tecnica.xls";
        public static string nameWordFile = "Propuesta_tecnica.docx";
        public static string imagePngFile = "Propuesta_tecnica.png";

    }
}
