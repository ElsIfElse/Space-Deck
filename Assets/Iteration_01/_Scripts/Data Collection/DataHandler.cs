using System.Threading.Tasks;
using Supabase;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public Supabase.Client Client;
    public static DataHandler Instance;
    string URL = "https://picghllybsyhoutrtchg.supabase.co";
    string KEY ="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InBpY2dobGx5YnN5aG91dHJ0Y2hnIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzIxMTQ1OTYsImV4cCI6MjA4NzY5MDU5Nn0._EJbp9MehK9MxmLtN-swaB8kXR2nfDPlaXJAFRdsY48";

    private async void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

        await InitializeDataHandler();
        await SubmitNewUpgradePick("Bogger"); // pass a test card name
    }

    private async Task InitializeDataHandler()
    {
        Debug.Log("Initializing Supabase..");
        var options = new SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = true
        };

        Client = new Supabase.Client(URL,KEY,options);
        await Client.InitializeAsync();

        Debug.Log("Supabase is initialized!");
    }

    public async Task SubmitNewUpgradePick(string cardName)
    {
        Debug.Log("Submitting data..");

        var pickedCardData = new CardPickRecord();
        pickedCardData.CardName = cardName;

        await Client.From<CardPickRecord>().Insert(pickedCardData);
        Debug.Log("New data submitted!");
    }

}