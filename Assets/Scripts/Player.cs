using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{

    float timer;

    Player()
    {
    }

    public int GetHealth()
    {
        return this.health;
    }
}
