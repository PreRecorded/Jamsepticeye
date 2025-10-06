using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public PlayerController playerControllerScript;
    public Animator cameraTransitionAnim;
    private Button playButton;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        playButton = root.Q<Button>("PlayButton");
        playButton.clicked += PlayGame;
    }


    private void PlayGame()
    {
        cameraTransitionAnim.SetTrigger("TransitionCamera");
        Destroy(cameraTransitionAnim.gameObject, 2f);
        playerControllerScript.enabled = true;
        gameObject.SetActive(false);
    }
}
