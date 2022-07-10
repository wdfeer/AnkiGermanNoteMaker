using AnkiConnectLib;
using HtmlAgilityPack;

namespace AnkiGermanNoteMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void SearchButton_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string html = await (await client.GetAsync($"https://dict.cc/?s={searchTextBox.Text}")).Content.ReadAsStringAsync();
            HtmlAgilityPack.HtmlDocument doc = new();
            doc.LoadHtml(html);
            var wordNodes = doc.DocumentNode.SelectNodes("//tr/td[@class='td7nl'][1]");

            frontTextBox.Text = searchTextBox.Text;

            string back = "";
            int i = 0;
            foreach (var node in wordNodes)
            {
                var filteredOut = node.SelectNodes("dfn");
                if (filteredOut != null)
                    node.RemoveChildren(filteredOut);
                filteredOut = node.SelectNodes("a[position()>1]");
                if (filteredOut != null)
                    node.RemoveChildren(filteredOut);

                string text = node.InnerText;
                text = new string(text.Trim().Where(c => c == ' ' || char.IsLetter(c)).ToArray());
                back += $"{i}. " + text + System.Environment.NewLine;

                i++;
                if (i >= 3)
                    break;
            }
            backTextBox.Text = back;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Notes.AddNote(deckNameTextBox.Text,
                          "Basic (and reversed card)",
                          frontTextBox.Text,
                          backTextBox.Text);
        }
    }
}