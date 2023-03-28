using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 position, string damageAmount, Color color)
    {
        Transform damagePopupTranform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        
        DamagePopup damagePopup = damagePopupTranform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, color);

        return damagePopup;
    }
    
    private TextMeshPro _textMeshPro;
    private float _disappearTimer;
    private Color _textColor;

    private void Awake()
    {
        _textMeshPro = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(string damageAmount,Color color)
    {
        _textMeshPro.SetText(damageAmount);
        _textColor = color;
    }
    
    private void Update()
    {
        transform.position += new Vector3(0, 1.25f) * Time.deltaTime;
        
        _disappearTimer -= Time.deltaTime;
        if (_disappearTimer < 0)
        {
            float disappearSpeed = 10f;
            _textColor.a -= disappearSpeed * Time.deltaTime;
            _textMeshPro.color = _textColor;
            if (_textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
