namespace URAPI
{
    public class YearOfStudies
    {
        public readonly Major Major;
        public readonly Collage Collage;
        public readonly string Name;
        //learn how to get that clear
        public readonly string CleanName;

        readonly string _scheduleLink;

        public YearOfStudies(Major major,string name, string scheduleLink)
        {
            Major = major;
            Collage = Major.Collage;
            Name = name;
            CleanName = "WIP";
            _scheduleLink = scheduleLink;
        }

        public override string ToString() => Name;
    }
}