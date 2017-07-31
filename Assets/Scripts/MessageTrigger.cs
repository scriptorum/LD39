using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If collider, triggers messages when colliding
// If not, triggers message at awake
public class MessageTrigger : MonoBehaviour
{
    public bool triggered = false;
    public string[] message;
    private int messageId = 0;

    void Awake()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if(collider == null)
            Invoke("showNextMessage", 0.1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.tag == "Player")
            showNextMessage();
    }

    public void showNextMessage()
    {
        if (messageId >= message.Length)
		{
			Messages.instance.hide();
            triggered = true;
		}
        else
            Messages.instance.show(message[messageId++], () => { showNextMessage(); });
    }
}