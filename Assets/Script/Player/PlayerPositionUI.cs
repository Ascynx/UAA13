using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPositionUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI _text;

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigidbody = Jeu.Instance.player.GetComponent<Rigidbody2D>();
        Vector3 positionVector = rigidbody.position;
        Vector3 velocity = rigidbody.velocity;
        _text.text = $"X - {Math.Floor(positionVector.x * 10) / 10} Y - {Math.Floor(positionVector.y * 10) / 10}";
    }
}
