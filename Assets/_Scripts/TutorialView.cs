using _Scripts.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class TutorialView : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("_Scenes/Game");
            }
        }
    }
}