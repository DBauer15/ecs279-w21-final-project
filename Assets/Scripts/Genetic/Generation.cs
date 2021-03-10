using UnityEngine;
using UnityEngine.Events;

class Generation
{
    public UnityEvent generationFinishedEvent;
    int numberOfCats, numberOfJoints, spawnHeight;
    GameObject catPrefab;

    public Generation(GameObject catPrefab, int numberOfCats, int spawnHeight)
    {
        generationFinishedEvent = new UnityEvent();
        this.catPrefab = catPrefab;
        this.numberOfCats = numberOfCats;
        this.spawnHeight = spawnHeight;
    }

    public void RunGeneration()
    {
        float xOffset = 2;

        for (int i = 0; i < numberOfCats; i++)
        {
            Vector3 spawnPosition = new Vector3(0, spawnHeight, 0 + xOffset * i);
            GameObject catGameObject = GameObject.Instantiate(catPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void Finished()
    {
        generationFinishedEvent.Invoke();
    }

}