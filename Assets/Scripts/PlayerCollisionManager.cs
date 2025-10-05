using UnityEngine;
using UnityEngine.SceneManagement; 
public class PlayerCollisionManager : MonoBehaviour
{
    public GameObject pnl_GameOver;
    AudioSource Sound_FX_Source;
    public AudioClip[] gruntingSounds;
    PlayerController playerControllerScript;
    private void Start()
    {
        pnl_GameOver.SetActive(false);
        Sound_FX_Source = GameObject.FindGameObjectWithTag("SoundFX").GetComponent<AudioSource>();
        playerControllerScript = GetComponent<PlayerController>();
    }

    /*    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Vehicle"))
            {
                HitByVehicle();
            }
        }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Vehicle"))
        {
            HitByVehicle();
        }
    }


    void HitByVehicle()
    {
        Sound_FX_Source.PlayOneShot(gruntingSounds[Random.Range(0,gruntingSounds.Length)]);
        playerControllerScript.enabled = false;
        //Time.timeScale = 0f;
        pnl_GameOver.SetActive(true);

    }

    public void PlayAgain()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
