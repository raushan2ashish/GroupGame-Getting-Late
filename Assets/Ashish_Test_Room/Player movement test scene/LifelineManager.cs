using UnityEngine;
using UnityEngine.UI;

public class LifelineManager : MonoBehaviour
{
    public GameObject lifelinePrefab;
    public Transform lifelineContainer;
    public int maxLives = 3;
    private int currentLives;
    public float spacing = 150f;
    private void Start()
    {
        currentLives = maxLives;
        CreateLifelines();
    }

    private void CreateLifelines()
    {
        for (int i = 0; i < maxLives; i++)
        {
            GameObject lifeline = Instantiate(lifelinePrefab, lifelineContainer);
            RectTransform rt = lifeline.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(i * spacing, 0); // Position each lifeline with spacing
        }
    }

    public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            Destroy(lifelineContainer.GetChild(currentLives).gameObject);
        }
    }

    public void GainLife()
    {
        if (currentLives < maxLives)
        {
            GameObject lifeline = Instantiate(lifelinePrefab, lifelineContainer);
            RectTransform rt = lifeline.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(currentLives * spacing, 0); // Position each new lifeline with spacing
            currentLives++;
        }
    }
    public int GetCurrentLives() // Add this method
    {
        return currentLives;
    }
}
