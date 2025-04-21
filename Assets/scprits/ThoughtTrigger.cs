using UnityEngine;

public class ThoughtTrigger : MonoBehaviour
{
    public string thoughtText;
    public ThoughtBoxController thoughtBox;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            thoughtBox.ShowThought(thoughtText); 
            Destroy(gameObject);
        }
    }
}
