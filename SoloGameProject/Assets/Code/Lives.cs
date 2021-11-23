using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Lives : MonoBehaviour
{
    // Start is called before the first frame update

    public int lives = 3;

    public void livesLose()
    {
        lives--;
        

        if(lives == 0)
        {
          Time.timeScale = 1f;
          Destroy(gameObject);
          SceneManager.LoadScene("MainMenu");         
        }
    }

   void OnGUI()
    {
        GUI.Box(new Rect(10, 40, 100, 30), "lives: " + lives);
    }
  
    private void Update()
    {
        
        //testing
        if (Input.GetKeyDown(KeyCode.R))
            livesLose();
    }
   
}
