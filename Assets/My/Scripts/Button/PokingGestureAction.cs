using System;
using Zinnia.Action;

public class PokingGestureAction : BooleanAction
{
    public Boolean gripTrigger { get; set; }
    public Boolean indexTrigger { get; set; }
    public Boolean isGrabbing { get; set; }

    void Update()
    {
        Receive(gripTrigger && !indexTrigger && !isGrabbing);
    }
}
