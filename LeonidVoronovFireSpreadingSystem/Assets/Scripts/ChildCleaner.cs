using UnityEngine;

namespace FireSpreading
{
    public class ChildCleaner : MonoBehaviour
    {
        public void DestroyChildren()
        {
            foreach (Transform child in transform) 
                Destroy(child.gameObject);
        }
    }
}

