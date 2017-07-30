using UnityEngine;
using UnityEngine.UI;


public class ProgressLabel : MonoBehaviour
{
    [Multiline]
    [SerializeField]
    string Format;
    [SerializeField]
    Text Label;
    [SerializeField]
    GameObject WinWindow;

    int DiscoveredCellsCount;
    int AllCellsCount;
    int CompletedQuestsCount;
    int AllQuestsCount;

    void Start()
    {
        WinWindow.SetActive(false);
    }

    public void OnCellWasDiscovered()
    {
        ++DiscoveredCellsCount;
        Refresh();
    }

    public void OnCellProgressWasChanged(int discoveredCellsCount, int allCellsCount)
    {
        DiscoveredCellsCount = discoveredCellsCount;
        AllCellsCount = allCellsCount;
        Refresh();
    }

    public void OnQuestsProgressWasChanged(int completedQuestsCount, int allQuestsCount)
    {
        CompletedQuestsCount = completedQuestsCount;
        AllQuestsCount = allQuestsCount;
        Refresh();
    }

    void Refresh()
    {
        if (Label == null) return;
        Label.text = string.Format(Format, DiscoveredCellsCount, AllCellsCount, CompletedQuestsCount, AllQuestsCount);
        WinWindow.SetActive(DiscoveredCellsCount == AllCellsCount && CompletedQuestsCount == AllQuestsCount);
    }
}