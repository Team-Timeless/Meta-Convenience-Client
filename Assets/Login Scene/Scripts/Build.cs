using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Build : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Convenient Scene");     // build and run 할때 login Scene 필요없으니 스킵
    }

    // Update is called once per frame
    void Update()
    {

    }
}
