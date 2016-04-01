using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
  public class BodySegmentSphere : BodySegment
  {
    //---------------------------------------------------------------------------
    // Constants.
    
    //---------------------------------------------------------------------------
    // Properties.

    public float Diameter { get; private set; }
    public float AppendageDiameter { get; set; }

    //---------------------------------------------------------------------------
    // Constructor.

    public BodySegmentSphere(
      float diameter,
      float appendageFactorOfBodyDiameterMin,
      float appendageFactorOfBodyDiameterMax )
    {
      Diameter = diameter;

      CalculateAppendageDiameter(
        appendageFactorOfBodyDiameterMin,
        appendageFactorOfBodyDiameterMax );
    }

    //---------------------------------------------------------------------------
    // Calculates what the diameter of appendages connected to this body
    // will be. The resulting diameter will be a random value between
    // the min & max values passed in.

    private void CalculateAppendageDiameter(
      float appendageFactorOfBodyDiameterMin,
      float appendageFactorOfBodyDiameterMax )
    {
      AppendageDiameter =
        Random.Range(
          Diameter * appendageFactorOfBodyDiameterMin,
          Diameter * appendageFactorOfBodyDiameterMax );
    }

    //---------------------------------------------------------------------------

    protected override void GetAppendagePoints(
      float fillFactor,
      out List<Vector3> points )
    {
      points = new List<Vector3>();
    }

    //---------------------------------------------------------------------------
  }
}
