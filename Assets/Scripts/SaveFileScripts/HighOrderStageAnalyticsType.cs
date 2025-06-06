public enum StageType
{
    Early,
    Mid,
    Late
}

public enum SWBSTOrFreeform
{
    SWBST,
    Freeform
}

[System.Serializable]
public class HighOrderStageTypeAnalytics
{

    public StageType stageType;
    public EarlyStage earlyStage;
    public MidStage midStage;
    public LateStage lateStage;

    public HighOrderStageTypeAnalytics(StageType type) // 0 
    {
        switch (type)
        {
            case StageType.Early:
                earlyStage = new EarlyStage();
                break;
            case StageType.Mid:
                midStage = new MidStage();
                break;
            case StageType.Late:
                lateStage = new LateStage();
                break;
        }
        this.stageType = type;

    }

    public void AddScore(float score)
    {
        if (stageType == StageType.Mid)
        {
            midStage.scoresList.Add(score);
        }
        else if (stageType == StageType.Late)
        {
            lateStage.scoresList.Add(score);
        }
    }

    public void AddAnswer(HO_SubmitAttempt answer)
    {
        if (stageType == StageType.Mid)
        {
            midStage.attempts.Add(answer);
        }
        else if (stageType == StageType.Late)
        {
            lateStage.attempts.Add(answer);
        }
    }

    public void AddSWBSTOrFreeformList(SWBSTOrFreeform SWBSTOrFreeform)
    {
        if (stageType == StageType.Mid)
        {
            if (SWBSTOrFreeform == SWBSTOrFreeform.SWBST)
            {
                midStage.swbstOrFreeformList.Add("SWBST");
            }
            else if (SWBSTOrFreeform == SWBSTOrFreeform.Freeform)
            {
                midStage.swbstOrFreeformList.Add("Freeform");
            }

        }
        else if (stageType == StageType.Late)
        {
            if (SWBSTOrFreeform == SWBSTOrFreeform.SWBST)
            {
                lateStage.swbstOrFreeformList.Add("SWBST");
            }
            else if (SWBSTOrFreeform == SWBSTOrFreeform.Freeform)
            {
                lateStage.swbstOrFreeformList.Add("Freeform");
            }
        }


    }



}
