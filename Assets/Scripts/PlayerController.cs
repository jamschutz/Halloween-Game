using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LegInput[] legs;




    [System.Serializable]
    public class LegInput
    {
        public char inputKey;
        public GameObject leg;
    }
}
