public class CellQuest : CellModification
{
    public QuestMaster.Quest Quest;
    public bool AlreadyActivated;

    public override void OnActivate()
    {
        if (AlreadyActivated) return;
        var questMaster = FindObjectOfType<QuestMaster>();
        questMaster.OnQuestWasCompleted(Quest);
        AlreadyActivated = true;
    }
}