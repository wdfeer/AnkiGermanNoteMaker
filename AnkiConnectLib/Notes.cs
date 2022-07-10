using System.Net;

namespace AnkiConnectLib;
public static class Notes
{
    public static string AddNote(string deckName, string modelName, string front, string back)
    {
        string data = File.ReadAllText(@"C:\Users\wdf-win\source\repos\AnkiConnectLib\addNoteTemplate.json");
        data = data.Replace("$deckName", deckName)
                   .Replace("$modelName", modelName)
                   .Replace("$front", front)
                   .Replace("$back", back);

        WebClient client = new WebClient();
        client.Headers[HttpRequestHeader.ContentType] = "application/json";
        string response = client.UploadString(Main.URI, data);
        return response;
    }
}