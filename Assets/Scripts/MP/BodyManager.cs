using UnityEngine;
public class BodyManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Player player;
    bool shielding = false;
    public void TriggerShield()
    {
        shielding = !shielding;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "SnakeBody":
                if (!shielding)
                {
                    gameManager.GameOver(player);
                }
                break;
            case "Food":
                gameManager.FoodConsumed(player, other.GetComponent<Food>().foodType);
                Destroy(other.gameObject);
                break;
            case "PowerUp":
                gameManager.PowerUpConsumed(player, other.GetComponent<PowerUps>().powerUpType);
                Destroy(other.gameObject);
                break;
            default: break;
        }
    }
}
