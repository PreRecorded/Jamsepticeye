using UnityEngine;
using UnityEngine.SceneManagement; 
public class PlayerCollisionManager : MonoBehaviour
{
    public GameObject pnl_GameOver;

    private void Start()
    {
        pnl_GameOver.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Vehicle"))
        {
            HitByVehicle();
        }
    }


    void HitByVehicle()
    {
        Time.timeScale = 0f;
        pnl_GameOver.SetActive(true);

    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
