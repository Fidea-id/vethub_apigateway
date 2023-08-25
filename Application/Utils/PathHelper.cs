using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils
{
    public enum UploadPathType
    {
        Files,
        Images
    }
    public enum TemplateType
    {
        SuratKematian,
        SuratPersetujuanTindakan,
        SuratRujukan,
        SuratTidakSetuju
    }

    public static class PathHelper
    {
        public static string GetUploadPath(UploadPathType typeUpload, string pathFolder)
        {
            var type = "";
            if(typeUpload == UploadPathType.Files)
            {
                type = "d";
            }
            else
            {
                type = "v";
            }
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", type, pathFolder);

            return folderPath;
        }

        public static string GetTemplatePath(TemplateType fileType)
        {
            var type = "";
            if (fileType == TemplateType.SuratRujukan)
            {
                type = "SuratRujukan.docx";
            }
            else if (fileType == TemplateType.SuratTidakSetuju)
            {
                type = "SuratPermintaanPulangAtauTidakSetujuRawatInap.docx";
            }
            else if (fileType == TemplateType.SuratPersetujuanTindakan)
            {
                type = "SuratPersetujuanTindakan.docx";
            }
            else if (fileType == TemplateType.SuratKematian)
            {
                type = "SuratKeteranganKematian.docx";
            }
            else
            {
                type = "";
            }
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Template", "Document");
            string filePath = Path.Combine(folderPath, type);

            return filePath;
        }

        public static string GetGenerateOutputPath(TemplateType code, string pathFolder)
        {
            var type = "";
            if (code == TemplateType.SuratRujukan)
            {
                type = "SuratRujukan";
            }
            else if (code == TemplateType.SuratTidakSetuju)
            {
                type = "SuratTidakSetuju";
            }
            else if (code == TemplateType.SuratPersetujuanTindakan)
            {
                type = "SuratPersetujuanTindakan";
            }
            else if (code == TemplateType.SuratKematian)
            {
                type = "SuratKematian";
            }
            else
            {
                type = "";
            }

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Generate", pathFolder, type);

            return folderPath;
        }
    }
}
