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
        public async Task<byte[]> GetScheduleByteArray()
        {
            HttpClient client = new HttpClient();
            byte[] bytes = await client.GetByteArrayAsync(_scheduleLink);
            return bytes;
        }

        public override string ToString() => Name;
    }
}