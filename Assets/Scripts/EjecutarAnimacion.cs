using UnityEngine;
using UnityEngine.UI;

public class PlayFromButton : MonoBehaviour
{
    [SerializeField] private Animator anim;         
    [SerializeField] private AnimationClip clip;    
    [SerializeField] private Button playButton;     

    void Start()
    {
        playButton.onClick.AddListener(PlayAnimation);
    }

    public void PlayAnimation()
    {
        if (anim != null && clip != null)
        {
            anim.Play(clip.name, -1, 0f);
        }
    }
}
