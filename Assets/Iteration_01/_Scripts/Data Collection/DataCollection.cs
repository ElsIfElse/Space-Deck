using Supabase;
using UnityEngine;

public class SupabaseTest : MonoBehaviour
{
    async void Start()
    {
        var client = new Client("https://picghllybsyhoutrtchg.supabase.co", "sb_publishable_4_M77EfREl28vyfuQRMkhw_aGjGhgqQ");
        await client.InitializeAsync();
        Debug.Log("Supabase connected!");
    }
}