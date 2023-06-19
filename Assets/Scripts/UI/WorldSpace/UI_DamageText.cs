using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_DamageText : MonoBehaviour
{

    private TextMeshPro _text;
    private Color _alpha;

    public int _damage;

    private float _moveSpeed;
    private float _alphaSpeed;
    private float _destroyTime;


    // Start is called before the first frame update
    void Start()
        {
            _moveSpeed = 2.0f;
            _alphaSpeed = 2.0f;
            _destroyTime = 2.0f;
    
            _text = GetComponent<TextMeshPro>();
            _alpha = _text.color;
            _text.text = _damage.ToString();
            Invoke("DestroyObject", _destroyTime);
        }
    
        // Update is called once per frame
        void Update()
        {
            transform.Translate(new Vector3(0, _moveSpeed * Time.deltaTime, 0)); // ?çÏä§???ÑÏπò
            
            _alpha.a = Mathf.Lerp(_alpha.a, 0, Time.deltaTime * _alphaSpeed); // ?çÏä§???åÌååÍ∞?
            _text.color = _alpha;
        }
    
        private void DestroyObject()
        {
            Destroy(gameObject);
        }
}
