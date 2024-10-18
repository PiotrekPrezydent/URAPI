using PDFtoImage;
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
        public async Task<MemoryStream> GetPDFAsImageStream(int page = 0)
        {
            //TD: convert xlsx and xls to pdf before
            MemoryStream ms = new();
            HttpClient client = new HttpClient();
            byte[] bytes = await client.GetByteArrayAsync(_scheduleLink);
            Conversion.SavePng(ms, bytes, page: page);
            ms.Position = 0;
            return ms;
        }

        public override string ToString() => Name;
    }
}