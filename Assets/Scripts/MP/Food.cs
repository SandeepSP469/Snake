using UnityEngine;
public enum FoodType
{
    Gainer, Burner
}
public class Food : MonoBehaviour
{
    [SerializeField] public FoodType foodType;
    void Awake()
    {
        foodType = (FoodType)Random.Range(0, 2);
    }
}
