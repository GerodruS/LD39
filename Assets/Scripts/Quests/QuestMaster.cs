using System.Collections.Generic;
using UnityEngine;


public class QuestMaster : MonoBehaviour
{
    public enum Quest
    {
        None,
        SOS,
    }

    public const int QuestsCount = 1;
    public List<Quest> CompletedQuests;
    public Events.Empty OnQuestListWasChanged;

    public void OnQuestWasCompleted(Quest quest)
    {
        if (!CompletedQuests.Contains(quest))
        {
            CompletedQuests.Add(quest);
            OnQuestListWasChanged.Invoke();
        }
    }
}