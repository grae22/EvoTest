using System.Collections.Generic;
using UnityEngine;
using Assets;

public class EnemyBuilder : MonoBehaviour
{
  //---------------------------------------------------------------------------

  void Start()
  {
    List<BodyAppendage> appendages;

    BodySegmentCube seg =
      new BodySegmentCube(
        1.0f,
        0.05f,
        0.1f,
        0.5f,
        2.0f );

    seg.CalculateAppendageProperties(
      0.25f,
      out appendages );

    GameObject segCube = GameObject.CreatePrimitive( PrimitiveType.Cube );
    segCube.gameObject.transform.parent = transform;
    segCube.transform.localScale.Set( 1f, 1f, 1f );

    foreach( BodyAppendage app in appendages )
    {
      GameObject appGob = GameObject.CreatePrimitive( PrimitiveType.Cylinder );
      appGob.transform.parent = segCube.transform;
      appGob.transform.localScale = new Vector3( seg.AppendageDiameter, seg.AppendageLength, seg.AppendageDiameter );
      appGob.transform.position = app.Position;
      appGob.transform.rotation = app.Rotation;
    }
  }

  //---------------------------------------------------------------------------

  void Update()
  {
  }

  //---------------------------------------------------------------------------
}
