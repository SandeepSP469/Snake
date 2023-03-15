using UnityEngine;
public enum PowerUp
{
    Shield, Multiplier, SpeedBoost
}
public class PowerUps : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] public PowerUp powerUpType;
    [SerializeField] Sprite shieldImg;
    [SerializeField] Sprite multiplierImg;
    [SerializeField] Sprite speedBoostImg;
    void Awake()
    {
        powerUpType = (PowerUp)Random.Range(0, 3);
        switch (powerUpType)
        {
            case PowerUp.Shield:
                spriteRenderer.sprite = shieldImg;
                break;
            case PowerUp.Multiplier:
                spriteRenderer.sprite = multiplierImg;
                break;
            case PowerUp.SpeedBoost:
                spriteRenderer.sprite = speedBoostImg;
                break;
        }
        spriteRenderer.color = Color.blue;
    }
}
