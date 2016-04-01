using UnityEngine;
using Assets;
using NUnit.Framework;

public class BodySegmentSphere_Test
{
  //---------------------------------------------------------------------------
  // Tests the the calculated appendage diameter is within the range
  // we're expecting - ( bodyDiameter * minFactor, bodyDiamter * maxFactor ).

  [Test]
  public void AppendageDiameter()
  {
    const float bodyDiameter = 10.0f;
    const float appendageFactorMin = 0.05f;
    const float appendageFactorMax = 0.2f;

    BodySegmentSphere segment =
      new BodySegmentSphere(
        bodyDiameter,
        appendageFactorMin,
        appendageFactorMax );

    Assert.GreaterOrEqual( segment.AppendageDiameter, bodyDiameter * appendageFactorMin );
    Assert.LessOrEqual( segment.AppendageDiameter, bodyDiameter * appendageFactorMax );
  }

  //---------------------------------------------------------------------------
}
