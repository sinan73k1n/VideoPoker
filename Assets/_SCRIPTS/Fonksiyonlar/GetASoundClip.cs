using UnityEngine;

public static class GetASoundClip
{
    public static AudioClip Hangi(NameOfAudioClip name) => Resources.Load<AudioClip>("Sound/" + name.ToString());
}
