public interface ILockedCard
{
    public bool IsCardLocked { get; set; }
    public int UnlockCost { get; set; }
}