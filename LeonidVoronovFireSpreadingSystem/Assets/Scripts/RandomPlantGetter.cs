using UnityEngine;

namespace FireSpreading
{
    public class RandomPlantGetter : MonoBehaviour
    {
        public GameObject GetRandomPlant()
        {
            int randomIndex = Random.Range(0, transform.childCount);

            if (randomIndex >= transform.childCount)
                return null;

            return transform.GetChild(randomIndex).gameObject;
        }
    }
}

