using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public int value;

    public Sprite[] sprites;

    public new SpriteRenderer renderer;
    public ParticleSystem removalParticleSystem;

	// Use this for initialization
	void Start ()
    {
        renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetValue(int value)
    {
        this.value = value;

        if (value == 0)
        {
            renderer.enabled = false;
        }
        else
        {
            renderer.sprite = sprites[value];
            renderer.enabled = true;
        }
    }

    public void PlayRemovalEffect()
    {
        removalParticleSystem.Play();
    }
}
