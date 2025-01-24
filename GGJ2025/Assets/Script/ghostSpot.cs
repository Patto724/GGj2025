using UnityEngine;

public class ghost : MonoBehaviour
{
     public Light spotlight; // Reference to the spotlight light
        public float minX = -10f, maxX = 10f; // Random position range (X-axis)
        public float minY = 1f, maxY = 10f; // Random position range (Y-axis)
        public float minZ = -10f, maxZ = 10f; // Random position range (Z-axis)
        public float spotlightDelay = 2f; // Time before spotlight appears after cutscene ends
    
        private bool cutsceneFinished = false; // Cutscene flag
        private float timer = 0f; // Timer to delay spotlight appearance
    
        // Start is called before the first frame update
        void Start()
        {
            spotlight.enabled = false; // Spotlight is initially off
        }
    
        // Update is called once per frame
        void Update()
        {
            if (cutsceneFinished)
            {
                timer += Time.deltaTime;
    
                // After the delay, make the spotlight appear at a random position
                if (timer >= spotlightDelay)
                {
                    if (!spotlight.enabled)
                    {
                        spotlight.enabled = true;
                        MoveSpotlightToRandomPosition();
                    }
                }
            }
        }
    
        // Function to end the cutscene and enable the spotlight
        public void EndCutscene()
        {
            cutsceneFinished = true;
        }
    
        // Function to move the spotlight to a random position
        private void MoveSpotlightToRandomPosition()
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            float randomZ = Random.Range(minZ, maxZ);
            
            spotlight.transform.position = new Vector3(randomX, randomY, randomZ);
        }
}
