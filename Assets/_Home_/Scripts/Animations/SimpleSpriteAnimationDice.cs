using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSpriteAnimationDice : MonoBehaviour
{

    public float secondsBetweenFrameChange = 0.3f;


    [SerializeField]
    private List<Sprite> sprites;
    public List<Sprite> faceSprites;
    private SpriteRenderer _image;
    private int _currentSpriteIndex = 0;
    public GameObject mosca;
    private int spawnVariance = 8;
    public GameObject spawnEffect;

    private void Start()
    {
        _image = GetComponent<SpriteRenderer>();
        _currentSpriteIndex = 0;
        if (sprites == null || sprites.Count <= 0) return;
        StartCoroutine(ChangeSpriteCoroutine());
        spawnEffect.SetActive(false);
    }

    private IEnumerator ChangeSpriteCoroutine()
    {
        while (_currentSpriteIndex < sprites.Count)
        {
            _image.sprite = sprites[_currentSpriteIndex];
            yield return new WaitForSeconds(secondsBetweenFrameChange);
            _currentSpriteIndex++;
        }
        SelectFaceSprite();
    }
    
    private void SelectFaceSprite()
    {
        int num_face = (int)UnityEngine.Random.Range(1, 6);
        _image.sprite = faceSprites[num_face];
        Invoke("_destroy", 1f);
        spawnEffect.SetActive(true);
        for (int i = 0; i <= num_face; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x + UnityEngine.Random.Range(-spawnVariance, spawnVariance), 1f,
                transform.position.z + UnityEngine.Random.Range(-spawnVariance, spawnVariance));
            Instantiate(mosca, spawnPosition, transform.rotation);
        }
    }

    private void _destroy()
    {
        Destroy(gameObject);
    }
}
