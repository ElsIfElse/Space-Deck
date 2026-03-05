using UnityEngine;

public class MenuManager : MonoBehaviour
{
    #region Singleton Init
    public static MenuManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    #endregion

    public MenuSlotHandler MenuSlotHandler;
    public MenuSlotHandlerData MenuSlotHandlerData;

    public CurrencyHandler CurrencyHandler;

    public CardEffectDescriptions CardEffectDescriptions;
    [Header("Dev Settings")]
    public bool _cheating = true;
    void Cheat()
    {
        CurrencyHandler.AddCurrency_Primary(0);
    }
    public void Initialize()
    {
        CreateSubHandlers();
        if(_cheating) Cheat();
    }

    void CreateSubHandlers()
    {
        CardEffectDescriptions = new();
        MenuSlotHandler = new(MenuSlotHandlerData, CardEffectDescriptions);
        CurrencyHandler = new();
    }

    // private void OnGUI() 
    // {
    //     if(GUI.Button(new Rect(0,0,100,75), "Deck")) MenuSlotHandler.SetMenuSlots(true);   
    //     if(GUI.Button(new Rect(100,0,100,75), "Locked Cards")) MenuSlotHandler.SetMenuSlots(false);   
    // }
}