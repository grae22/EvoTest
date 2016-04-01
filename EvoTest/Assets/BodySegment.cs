using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
  public abstract class BodySegment
  {
    //---------------------------------------------------------------------------

    protected abstract void GetAppendagePoints(
      float fillFactor,               // (0.0, 1.0) 0.0 = No appendages; 1.0 = Max # of appendages.
      out List<Vector3> points );   // Points list to be populated.

    //---------------------------------------------------------------------------
  }
}
