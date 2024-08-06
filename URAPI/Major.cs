using HtmlAgilityPack;

namespace URAPI
{
    public class Major
    {
        readonly string _url;
        public string Name;
        public Collage Collage;

        public Major(Collage collage,string name, string URL)
        {
            Collage = collage;
            Name = name;
            _url = URL;
        }

        public IEnumerable<YearOfStudies> GetYearOfStudies()
        {
            List<YearOfStudies> yearsOfStudies = new();
            string xpath = "//div[contains(@class, 'main-content') or contains(@class, 'inside')]//li//a[contains(@title, 'Rozkłady') or contains(@title, 'Schedule') or contains(@title, 'plany zajęć') or contains(@title, 'rozkład zajęć') or contains(@title, 'rozkłady zajęć')][1]";

            var web = new HtmlWeb();
            var doc = web.Load(_url);
            var scheleudesUrl = doc.DocumentNode.SelectNodes(xpath);

            //happens in Media, Visual and Social Communication
            if (scheleudesUrl == null)
                return yearsOfStudies;

            doc = web.Load(Client.UR_URL + "/" + scheleudesUrl![0].Attributes["href"].Value);
            xpath = "//div[contains(@class, 'main-content')]//a[contains(@title, 'studia stacjonarne') or contains(@title, 'studia niestacjonarne')]";
            var moreOptions = doc.DocumentNode.SelectNodes(xpath);

            //no yearofstudies in scheludes happens in Bezpieczeństwo i certyfikacja żywności and Nanotechnologia
            //or must select fulltimestudies
            if (moreOptions != null)
            {
                var links = doc.DocumentNode.SelectNodes(xpath);
                if (links == null)
                    return yearsOfStudies;

                foreach (var link in links)
                {
                    if (link.Attributes["href"].Value.EndsWith(".pdf") || link.Attributes["href"].Value.EndsWith(".xlsx"))
                    {
                        Console.WriteLine(link.Attributes["href"].Value);
                        continue;
                    }

                    GetScheludesFromURL(Client.UR_URL +"/"+ link.Attributes["href"].Value);
                }
                return yearsOfStudies;
            }
            GetScheludesFromURL(Client.UR_URL + "/" + scheleudesUrl![0].Attributes["href"].Value);


            return yearsOfStudies;


            void GetScheludesFromURL(string url)
            {
                doc = web.Load(url);
                xpath = "//div[contains(@class, 'main-content')]//a[contains(@href, '.pdf') or contains(@href,'.xlsx')]";
                var anyA = doc.DocumentNode.SelectNodes(xpath);
                if (anyA == null)
                    return;

                foreach (var a in anyA)
                {
                    string m = Uri.UnescapeDataString(a.Attributes["href"].Value);
                    if (m.ContainsAny(Client.excludedWords))
                        continue;
                    m = m.Substring(m.LastIndexOf("/") + 1);
                    yearsOfStudies.Add(new YearOfStudies(m.Substring(0, m.LastIndexOf(".")), Client.UR_URL + "/" + a.Attributes["href"].Value));
                }
            }
        }
    }
}