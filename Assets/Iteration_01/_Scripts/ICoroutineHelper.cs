using System.Collections;

public interface ICoroutineHelper
{
    public IEnumerator StartRoutine(IEnumerator routine);
    public void KillRoutine(IEnumerator routine);
}