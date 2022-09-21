using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BigCardRewardItem : MonoBehaviour
{
    [SerializeField] private Image Image;

    [SerializeField] private TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        Image = GetComponent<Image>();
        Text = GetComponent<TextMeshProUGUI>();
    }

    public void SetImage(Sprite sprite)
    {
        Image.sprite = sprite;
    }

    public void SetAmount(int amount)
    {
        Text.text = amount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
