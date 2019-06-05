using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Grabable : MonoBehaviour
{

    private Interactable interactable;

    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);


    void Awake()
    {
        interactable = this.GetComponent<Interactable>();
    }


    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            // Call this to continue receiving HandHoverUpdate messages,
            // and prevent the hand from hovering over anything else
            hand.HoverLock(interactable);

            // Attach this object to the hand
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
        }
        else if (isGrabEnding)
        {
            // Detach this object from the hand
            hand.DetachObject(gameObject);

            // Call this to undo HoverLock
            hand.HoverUnlock(interactable);
        }
    }

    private void Update()
    {

    }



}
