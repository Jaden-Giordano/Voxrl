using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum StatusType {
    Stunned = 0,
    Slowed = 1,
    Hasted = 2
}

public class Status {

    public StatusType type;
    public float percentage;

    public float timeLeft;

    public bool Fisished {
        get { return timeLeft <= 0; }
    }

    public Status(StatusType type, float percentage, float time) {
        this.type = type;
        this.percentage = percentage;

        this.timeLeft = time;
    }

}
