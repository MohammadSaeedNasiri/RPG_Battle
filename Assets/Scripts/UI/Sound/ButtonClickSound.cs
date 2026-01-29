using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Form Play Clicked Button Effect Sound
[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour, IPointerClickHandler
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        float volume = 1;// PlayerPrefs.GetFloat("EffectsVolume", 1f);

        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound, volume);
        }
        else
        {
            Debug.LogWarning("Click sound not assigned to: " + gameObject.name);
        }
    }

}
