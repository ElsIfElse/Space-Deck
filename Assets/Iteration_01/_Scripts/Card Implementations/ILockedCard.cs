public interface ILockedCard
{
    public bool isCardLocked { get; set; }
    public int UnlockCost { get; set; }
}