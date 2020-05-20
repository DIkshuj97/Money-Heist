using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    int index;

    private void Awake()
	{
		SetUpSingleton();
	}

	private void SetUpSingleton()
	{
		int numberGameSessions = FindObjectsOfType<ScenePersist>().Length;
		if (numberGameSessions > 1)
		{
			Destroy(gameObject);
		}

		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        int currentindex = SceneManager.GetActiveScene().buildIndex;
        if (currentindex != index)
        {
            Destroy(gameObject);
        }
    }
}
