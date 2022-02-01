using UnityEngine;
using System.Collections;

public class GetASoundClip : MonoBehaviour
{
   
    public static AudioClip Hangi(NameOfAudioClip name) { return Resources.Load<AudioClip>("Sound/" + name.ToString()); }
}
