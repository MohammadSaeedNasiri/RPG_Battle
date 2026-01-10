using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    private void Start()
    {
        //Button btn = gameObject.GetComponent<Button>();
        //btn.(PlayPointerClickSound);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        float volume = PlayerPrefs.GetFloat("EffectsVolume", 1f);

        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound, volume);
        }
        else
        {
            Debug.LogWarning("Click sound not assigned to: " + gameObject.name);
        }
    }


   /*public void PlayPointerClickSound()
    {
        float volume = PlayerPrefs.GetFloat("EffectsVolume", 1f);

        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound, volume);
        }
        else
        {
            Debug.LogWarning("Click sound not assigned to: " + gameObject.name);
        }
    }*/
}
