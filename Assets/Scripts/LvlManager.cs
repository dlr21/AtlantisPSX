using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlManager : MonoBehaviour
{

    public Item greenCrystal;

    [Header("Palabra Atlantis")]
    [SerializeField] private string[] atlantis;

    public bool complete;

    private void Start()
    {
        greenCrystal = null;
        atlantis = null;
        complete = false;
    }

    public void Letter(string s)
    {
        if (s.Equals("a"))
        {
            if (atlantis[0] == null)
            {
                atlantis[0] = s;
            }
            else
            {
                atlantis[3] = s;
            }

        }
        else if (s.Equals("t"))
        {
            if (atlantis[1] == null)
            {
                atlantis[1] = s;
            }
            else
            {
                atlantis[5] = s;
            }
        }
        else if (s.Equals("l"))
        {
            atlantis[2] = s;
        }
        else if (s.Equals("n"))
        {
            atlantis[4] = s;
        }
        else if (s.Equals("i"))
        {
            atlantis[6] = s;
        }
        else if (s.Equals("s"))
        {
            atlantis[7] = s;
        }

        ComprobarCompleto();
    }

    private void ComprobarCompleto() {
        //comprobar palabra complete ATLANTIS

    }

    public void CollectCrystal(Item green) {
        if (greenCrystal == null) {
            greenCrystal = green;
        }
    }

}
