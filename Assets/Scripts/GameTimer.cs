using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    public float levelSeconds = 100;

    private Slider slider;
    private AudioSource audioSource;
    private bool isEndOfLevel = false;
    private LevelManager levelManager;
    private GameObject winLabel;
    private GameObject loseCollider;

	// Use this for initialization
	void Start ()
    {
        slider = GetComponent<Slider>();
        audioSource = GetComponent<AudioSource>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        FindYouWin();
        winLabel.SetActive(false);
        loseCollider = GameObject.Find("LoseCollider");
    }

    void FindYouWin()
    {
        winLabel = GameObject.Find("YouWin");
        if (!winLabel)
        {
            Debug.LogWarning("Please create You Win Object");
        }
    }

    // Update is called once per frame
    void Update () {
        slider.value = Time.timeSinceLevelLoad / levelSeconds;
        bool timeIsUp = (Time.timeSinceLevelLoad >= levelSeconds);
        if (timeIsUp && !isEndOfLevel)
        {
            HandleWinCondition();
        }
    }

    void HandleWinCondition()
    {
        DestroyAllTaggedObjects();
        winLabel.SetActive(true);
        loseCollider.SetActive(false);
        audioSource.Play();
        Invoke("LoadNextLevel", audioSource.clip.length);
        isEndOfLevel = true;
    }

    //destroys all objects with destroyOnWin tag
    void DestroyAllTaggedObjects()
    {
        GameObject[] destroyArray = GameObject.FindGameObjectsWithTag("destroyOnWin");
        foreach(GameObject destroy in destroyArray)
        {
            Destroy(destroy);
        }
    }

    void LoadNextLevel()
    {
        levelManager.LoadNextLevel();
    }
}
