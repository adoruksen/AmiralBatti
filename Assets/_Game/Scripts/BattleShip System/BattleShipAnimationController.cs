using UnityEngine;

public class BattleShipAnimationController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _model;
    
    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetVisibility(bool isPlayer)
    {
        if(!isPlayer) _model.SetActive(false);
    }

    public void SetSpriteColor(bool isPlayer)
    {
        _spriteRenderer.color = isPlayer ? Color.green : Color.red;
    }
}
