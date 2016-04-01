using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
  public abstract class BodySegment
  {
    //-------------------------------------------------------------------------
    // Implementations of this method should calculate the positions (local
    // to each body) where appendages should be placed and populate the passed
    // list with these positions.
    //
    // fillFactor: (0.0, 1.0) 0.0 = No appendages; 1.0 = Max # of appendages.
    // points: Points list to be populated.

    public abstract void CalculateAppendagePoints(
      float fillFactor,             
      out List<Vector3> points );

    //-------------------------------------------------------------------------
  }
}
