using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace CodeBase.Cfg
{
    [CreateAssetMenu(menuName = "Satisroom/Sound pack")]
    public class SoundPack : ScriptableObject
    {
        [field: SerializeField] public AudioClip StartDragSound { get; private set; }
        [field: SerializeField] public AudioClip EndDragSound{ get; private set; }
        [field: SerializeField] public AudioClip DisableDragSound { get; private set; }
        [field: SerializeField] public AudioMixerGroup SoundGroup { get; private set; }
    }
}
