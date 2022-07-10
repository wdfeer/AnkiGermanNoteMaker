using System.Net;
using System.Reflection;

namespace AnkiConnectLib;
public static class Notes
{
    public static string AddNote(string deckName, string modelName, string front, string back)
    {
        var assembly = Assembly.GetExecutingAssembly();
        string data;
        using (Stream stream = assembly.GetManifestResourceStream("AnkiConnectLib.addNoteTemplate.json")!)
        using (StreamReader reader = new StreamReader(stream))
        {
            data = reader.ReadToEnd();
        }

        data = data.Replace("$deckName", deckName)
                   .Replace("$modelName", modelName)
                   .Replace("$front", front)
                   .Replace("$back", back);

        using (WebClient client = new WebClient())
        {
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            string response = client.UploadString(Main.URI, data);
            return response;
        }
    }
}