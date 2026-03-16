using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats stats = new PlayerStats();
    public PlayerStats Stats => stats;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = ReadMoveInput();
        rb.linearVelocity = moveInput.normalized * stats.vitaminB;
        FlipSprite(moveInput);
    }

    private Vector2 ReadMoveInput()
    {
        var kb = Keyboard.current;
        if (kb == null) return Vector2.zero;

        Vector2 dir = Vector2.zero;

        if (kb.wKey.isPressed || kb.upArrowKey.isPressed) dir.y += 1f;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed) dir.y -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) dir.x += 1f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) dir.x -= 1f;

        return dir;
    }

    private void FlipSprite(Vector2 moveInput)
    {
        if (spriteRenderer == null) return;
        if (moveInput.x > 0.01f) spriteRenderer.flipX = false;
        else if (moveInput.x < -0.01f) spriteRenderer.flipX = true;
    }

    public void ApplyStatUpgrade(StatType stat, float value)
    {
        switch (stat)
        {
            case StatType.VitaminD: stats.vitaminD += value; break;
            case StatType.VitaminC: stats.vitaminC = Mathf.Clamp(stats.vitaminC + value, 0f, 100f); break;
            case StatType.VitaminA: stats.vitaminA += value; break;
            case StatType.VitaminB: stats.vitaminB += value; break;
            case StatType.VitaminK: stats.vitaminK = Mathf.Clamp(stats.vitaminK + value, 0f, 100f); break;
            case StatType.VitaminE: stats.vitaminE += value; break;
            case StatType.VitaminPP: stats.vitaminPP += value; break;
        }
    }
}

public enum StatType
{
    VitaminD, VitaminC, VitaminA, VitaminB, VitaminK, VitaminE, VitaminPP
}