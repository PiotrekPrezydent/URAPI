using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Xls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http;

namespace URAPI
{
    public class Schedule
    {
        public readonly Major Major;
        public readonly Collage Collage;
        public readonly string Name;
        //learn how to get that clear
        public readonly string CleanName;

        public string ScheduleType => _scheduleLink.Substring(_scheduleLink.LastIndexOf(".")+1);

        readonly string _scheduleLink;

        public Schedule(Major major,string name, string scheduleLink)
        {
            Major = major;
            Collage = Major.Collage;
            Name = name;
            CleanName = "WIP";
            _scheduleLink = scheduleLink;
        }

        public async Task<int> GetPagesCount() 
        {
            if (ScheduleType != "pdf")
                return int.MinValue;

            HttpClient client = new HttpClient();
            byte[] bytes = await client.GetByteArrayAsync(_scheduleLink);
            MemoryStream ms = new(bytes);
            PdfDocument pdf = new();
            pdf.LoadFromStream(ms);

            return pdf.Pages.Count;
        }

        public async Task<Image?> ScheduleAsImage(int page = 0)
        {
            Image i = default;
            HttpClient client = new HttpClient();
            byte[] bytes = await client.GetByteArrayAsync(_scheduleLink);
            MemoryStream ms = new(bytes);

            if (ScheduleType == "pdf")
            {
                PdfDocument pdf = new();
                pdf.LoadFromStream(ms);
                i = pdf.SaveAsImage(page, PdfImageType.Bitmap, 300, 300);
            }
            else if (ScheduleType == "xlsx" || ScheduleType == "xls")
            {
                Workbook wb = new();
                wb.LoadFromStream(ms);
                i = wb.SaveAsImage(page, 300, 300);
            }
            else
                return null;

            i.RotateFlip(RotateFlipType.Rotate270FlipXY);
            return i;

        }

        public override string ToString() => Name;
    }
}