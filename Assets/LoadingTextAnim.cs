using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingTextAnim : MonoBehaviour
{
    public Text loadingText;

    IEnumerator Start()
    {
        int dots = 0;

        while (true)
        {
            dots = (dots + 1) % 4;

            loadingText.text = "Loading" + new string('.', dots);

            yield return new WaitForSeconds(0.3f);
        }
    }
}