using System.Collections.Generic;
using UnityEngine;
using Assets;
using NUnit.Framework;

public class BodySegmentCube_Test
{
  //---------------------------------------------------------------------------
  // Test that the calculated appendage diameter is within the range
  // we're expecting - ( bodySize * minFactor, bodySize * maxFactor ).

  [Test]
  public void AppendageDiameter()
  {
    const float bodySize = 10.0f;
    const float appendageFactorMin = 0.05f;
    const float appendageFactorMax = 0.2f;

    BodySegmentCube segment =
      new BodySegmentCube(
        bodySize,
        appendageFactorMin,
        appendageFactorMax );

    Assert.GreaterOrEqual( segment.AppendageDiameter, bodySize * appendageFactorMin );
    Assert.LessOrEqual( segment.AppendageDiameter, bodySize * appendageFactorMax );
  }

  //---------------------------------------------------------------------------
  // Test that the number of appendage positions calculated is what we'd expect.

  [Test]
  public void AppendageCount()
  {
    // Create a body segment.
    const float bodySize = 6.0f;
    const float appendageFactorMin = 0.1666666666666667f;
    const float appendageFactorMax = 0.1666666666666667f;

    BodySegmentCube segment =
      new BodySegmentCube(
        bodySize,
        appendageFactorMin,
        appendageFactorMax );

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

    // Get the appendage points.
    List<Vector3> points = null;

    segment.CalculateAppendagePoints(
      1.0f,
      out points );

    // Test.
    Assert.AreEqual( expectedAppCount, points.Count );
  }

  //---------------------------------------------------------------------------
}
