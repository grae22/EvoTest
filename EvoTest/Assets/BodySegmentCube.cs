using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
  public class BodySegmentCube : BodySegment
  {
    //-------------------------------------------------------------------------
    // Constants.
    
    //-------------------------------------------------------------------------
    // Properties.

    public float Size { get; private set; }
    public float AppendageDiameter { get; set; }

    //-------------------------------------------------------------------------
    // Constructor.

    public BodySegmentCube(
      float size,
      float appendageFactorOfBodyDiameterMin,
      float appendageFactorOfBodyDiameterMax )
    {
      Size = size;

      CalculateAppendageDiameter(
        appendageFactorOfBodyDiameterMin,
        appendageFactorOfBodyDiameterMax );
    }

    //-------------------------------------------------------------------------
    // Calculates what the diameter of appendages connected to this body
    // will be. The resulting diameter will be a random value between
    // the min & max values passed in.

    private void CalculateAppendageDiameter(
      float appendageFactorOfBodyDiameterMin,
      float appendageFactorOfBodyDiameterMax )
    {
      AppendageDiameter =
        Random.Range(
          Size * appendageFactorOfBodyDiameterMin,
          Size * appendageFactorOfBodyDiameterMax );
    }

    //-------------------------------------------------------------------------
    // Implementation of BodySegment::CalculateAppendagePoints().
      
    public override void CalculateAppendagePoints(
      float fillFactor,
      out List<Vector3> points )
    {
      points = new List<Vector3>();

      // How many appendages can we fit per face of our cubic body?
      // We multiply the appendage-diameter by 2 since we want the appendages
      // to be spaced by at least their diameter.
      int appPerFaceCount =
        (int)Mathf.Floor( Size / ( AppendageDiameter * 2.0f ) );

      appPerFaceCount *= appPerFaceCount;

      // Calculate the number of appendages we're going to have.
      int appCount = appPerFaceCount * 6;  // 6 faces on a cube.

      // Create the appendage positions.
      for( int i = 0; i < appCount; i++ )
      {
        points.Add( new Vector3() );
      }
    }

    //-------------------------------------------------------------------------
  }
}
