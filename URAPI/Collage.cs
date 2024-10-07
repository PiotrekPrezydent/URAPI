using HtmlAgilityPack;

namespace URAPI
{
    public class Collage
    {
        public static Action OnGetMajorsStarted = delegate { };
        public static Action OnGetMajorsEnded = delegate { };

        public readonly string Name;

        readonly string _url;

        public Collage(string name,string URL)
        {
            Name = name;
            _url = URL;
        }

        public async Task<List<Major>> GetMajors()
        {
            OnGetMajorsStarted.Invoke();
            var result = new List<Major>();
            string xpath = "//li/a[@title=\"Student\"][1]";
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(_url);

            var studentSubmenu = doc.DocumentNode.SelectNodes(xpath)[0].ParentNode;
            xpath = "ul//li/a[@title=\"Kierunki studiów (programy, rozkłady, sylabusy)\"][1]";
            var majorsLink = studentSubmenu.SelectNodes(xpath)[0];

            doc = web.Load(Client.UR_URL + "/" + majorsLink.Attributes["href"].Value);
            xpath = "//div[@class=\"main-content columns large-12\"]//ul[@class=\"level_1\"]//li/a[1]";

            var majorsLi = doc.DocumentNode.SelectNodes(xpath);

            foreach (var major in majorsLi)
                result.Add(new Major(this, major.Attributes["title"].Value, Client.UR_URL + "/" + major.Attributes["href"].Value));
            OnGetMajorsEnded.Invoke();
            return result;
        }

        public override string ToString() => Name;
    }
}