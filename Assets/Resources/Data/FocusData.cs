using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FocusData", menuName = "Data/Focus")]
public class FocusData : ScriptableObject
{
    public enum Property { None, Anima, Convergence, Doublelift, Glimmer, Hearth, Hexawan, Kinesys, Miseri, Phalanx, Slashfik }
    public Property[] property;
    public Sprite[] FocusIcons;
    public string[] FocusText;
}
