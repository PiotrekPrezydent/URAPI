namespace URAPI
{
    public class YearOfStudies
    {
        public string Name;
        //learn how to get that clear
        public string CleanName;
        string _scheduleLink;

        public YearOfStudies(string name, string scheduleLink)
        {
            Name = name;
            CleanName = "WIP";
            _scheduleLink = scheduleLink;
        }
    }
}