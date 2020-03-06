using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;
using System;


public class PokeLogic : BooleanAction
{
   public Boolean GrabButton { get; set;}
   public Boolean IndexTrigger { get; set;}
   public Boolean isGrabbing { get; set;}

   public void update() {
       Receive(GrabButton && !IndexTrigger && !isGrabbing);
   }
}
