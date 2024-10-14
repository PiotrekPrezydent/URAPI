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

        readonly string _scheduleLink;

        public Schedule(Major major,string name, string scheduleLink)
        {
            Major = major;
            Collage = Major.Collage;
            Name = name;
            CleanName = "WIP";
            _scheduleLink = scheduleLink;
        }

        public async Task<byte[]> GetPDFBytes()
        {
            //td some schedules might be in xlsx or xls
            HttpClient client = new HttpClient();
            byte[] bytes = await client.GetByteArrayAsync(_scheduleLink);
            return bytes;
        }

        public override string ToString() => Name;
    }
}