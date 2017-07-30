using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestMaster : MonoBehaviour
{
    public enum Quest
    {
        None,
        SOS,
    }

    [Serializable]
    public class QuestToggle
    {
        public Quest Quest;
        public Toggle Toggle;
    }

    public const int QuestsCount = 1;
    public List<Quest> CompletedQuests;
    public QuestToggle[] TogglesByQuests;
//    public Events.Empty OnQuestListWasChanged;

    public void OnQuestWasCompleted(Quest quest)
    {
        if (!CompletedQuests.Contains(quest))
        {
            CompletedQuests.Add(quest);
//            OnQuestListWasChanged.Invoke();
            foreach (var elem in TogglesByQuests)
            {
                if (elem.Quest == quest)
                {
                    elem.Toggle.isOn = true;
                }
            }
        }
    }
}