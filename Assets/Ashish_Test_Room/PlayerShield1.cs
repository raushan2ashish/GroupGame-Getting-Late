using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield1 : MonoBehaviour
{
    public GameObject shieldUmbrella; // Assign the ShieldUmbrella GameObject in the Inspector
    public string shieldButton = "Shield"; // Input Manager button name
    private bool isShieldActive = false; // Tracks whether the shield is active
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Enable the shield while the button is held down
        if (Input.GetButtonDown(shieldButton))
        {
            shieldUmbrella.SetActive(true);
            anim.SetBool("isShielding", true);
        }
        else if (Input.GetButtonUp(shieldButton))
        {
            shieldUmbrella.SetActive(false);
            anim.SetBool("isShielding", false);
        }
    }
    private void ToggleShield()
    {
        isShieldActive = !isShieldActive; // Toggle shield state
        shieldUmbrella.SetActive(isShieldActive); // Activate/Deactivate the shield
    }
}
