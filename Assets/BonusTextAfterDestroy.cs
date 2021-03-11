using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusTextAfterDestroy : MonoBehaviour
{
    public AnimationCurve animationCurve;
    private Text currentText;
    private void OnEnable()
    {
        currentText = this.GetComponent<Text>();
        Debug.Log("Enable");
        StartCoroutine(MoveUp());
    }

    private IEnumerator MoveUp()
    {
        Debug.Log("StartCouroutine");
        Color currentColor = currentText.color;
        Vector2 startPos = this.transform.position;
        for (float i = 0; i < 1f; i += Time.deltaTime)
        {
            currentColor.a = 1f - animationCurve.Evaluate(i);
            
            currentText.color = currentColor;
            this.transform.position = Vector2.Lerp(startPos, startPos + 300f*Vector2.up,animationCurve.Evaluate(i));
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
        yield return null;
    }


}
