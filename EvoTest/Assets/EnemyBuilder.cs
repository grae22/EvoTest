using System.Collections.Generic;
using UnityEngine;
using Assets;

public class EnemyBuilder : MonoBehaviour
{
  //---------------------------------------------------------------------------

  void Start()
  {
    List<BodySegment.PositionAndNormal> appendagePoints;

    BodySegmentCube seg =
      new BodySegmentCube(
        1f,
        0.05f,
        0.2f );

    seg.CalculateAppendagePoints(
      0.5f,
      out appendagePoints );

    GameObject segCube = GameObject.CreatePrimitive( PrimitiveType.Cube );
    segCube.transform.localScale.Set( 1f, 1f, 1f );

    foreach( BodySegment.PositionAndNormal pos in appendagePoints )
    {
      GameObject app = GameObject.CreatePrimitive( PrimitiveType.Cylinder );
      app.transform.localScale.Set( 1f, seg.AppendageDiameter, 1f );
      app.transform.position = pos.position;
      //app.transform.Rotate( pos.normal );
    }
  }

  //---------------------------------------------------------------------------

  void Update()
  {
  }

  //---------------------------------------------------------------------------
}
