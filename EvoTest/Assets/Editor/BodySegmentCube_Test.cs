using System.Collections.Generic;
using UnityEngine;
using Assets;
using NUnit.Framework;

public class BodySegmentCube_Test
{
  //---------------------------------------------------------------------------
  // Test that the calculated appendage diameter & length is within the range
  // we're expecting - ( bodySize * minFactor, bodySize * maxFactor ).

  [Test]
  public void AppendageDiameterAndLength()
  {
    const float bodySize = 10.0f;
    const float diameterFactorMin = 0.05f;
    const float diameterFactorMax = 0.2f;
    const float lengthFactorMin = 0.5f;
    const float lengthFactorMax = 2.0f;

    BodySegmentCube segment =
      new BodySegmentCube(
        bodySize,
        diameterFactorMin,
        diameterFactorMax,
        lengthFactorMin,
        lengthFactorMax );

    // Diameter.
    Assert.GreaterOrEqual( segment.AppendageDiameter, bodySize * diameterFactorMin );
    Assert.LessOrEqual( segment.AppendageDiameter, bodySize * diameterFactorMax );

    // Length.
    Assert.GreaterOrEqual( segment.AppendageLength, bodySize * lengthFactorMin );
    Assert.LessOrEqual( segment.AppendageLength, bodySize * lengthFactorMax );
  }

  //---------------------------------------------------------------------------
  // Test that the number of appendage positions calculated is what we'd expect.

  [Test]
  public void AppendageCount()
  {
    // Create a body segment.
    float bodySize = 6f;

    BodySegmentCube segment =
      new BodySegmentCube(
        bodySize,
        0.1f,
        1f,
        1f,
        2f );

    // Figure out how many points we should get.
    // We multiply the appendage-diameter by 2 since we want there to be at
    // least a spacing between appendages of the appendage-diameter./
    // We then square it to calculate the number of appendages to cover the
    // entire 2d face.
    // We multiply by 6 since a cube has 6 faces.
    int expectedAppCount =
      (int)( Mathf.Floor(
        bodySize / ( segment.AppendageDiameter * 2.0f ) ) );

    expectedAppCount *= expectedAppCount;
    expectedAppCount *= 6;

    // Get the appendages.
    List<BodyAppendage> appendages = null;
    segment.CalculateAppendageProperties( 1.0f, out appendages );

    // Test.
    Assert.AreEqual( expectedAppCount, appendages.Count );

    // Get the appendages.
    segment.CalculateAppendageProperties( 0.5f, out appendages );

    // Test.
    Assert.AreEqual( expectedAppCount * 0.5f, appendages.Count );
  }

  //---------------------------------------------------------------------------
  // Test that the appendage positions are actually on a face of a cubic body.

  [Test]
  public void AppendagePositions()
  {

  }

  //---------------------------------------------------------------------------
}
