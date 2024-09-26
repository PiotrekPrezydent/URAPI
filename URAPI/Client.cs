using HtmlAgilityPack;

namespace URAPI
{
    public static class Client
    {
        public const string UR_URL = "https://www.ur.edu.pl";

        internal static readonly string[] excludedWords =
        {
            "Zarządzenie",
            "Legenda",
            "LEGENDA",
            "AKTUALIZACJA",
            "kalendarz",
            "lokalizacja",
            "Division",
            "POŁOŻNICTWO",
            "lista",
            "album",
            "LISTA",
            "Organizacja"
        };

        public static List<Collage> GetCollages()
        {
            var result = new List<Collage>();
            string xpath = "//li/a[@title=\"Kolegia\"][1]";

            var web = new HtmlWeb();
            var doc = web.Load(UR_URL);
            var collagesSubmenu = doc.DocumentNode.SelectNodes(xpath)[0].ParentNode;

            var collagesLink = collagesSubmenu.SelectNodes("ul//li/a[1]");
            foreach (var collageHTML in collagesLink)
                result.Add(new Collage(collageHTML.InnerText, UR_URL + "/" + collageHTML.Attributes["href"].Value));

            return result;

        }

        internal static bool ContainsAny(this string a, params string[] tab)
        {
            for (int i = 0; i < tab.Length; i++)
                if (a.Contains(tab[i]))
                    return true;

            return false;
        }
    }
}
