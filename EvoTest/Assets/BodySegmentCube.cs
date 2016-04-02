using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
  public class BodySegmentCube : BodySegment
  {
    //-------------------------------------------------------------------------
    // Constants.

    public enum Face
    {
      TOP = 0,
      BOTTOM,
      NORTH,
      EAST,
      SOUTH,
      WEST,
      FACE_COUNT
    };
    
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
      int appPerRowCount =
        (int)Mathf.Floor( Size / ( AppendageDiameter * 2.0f ) );

      int appPerFaceCount = appPerRowCount * appPerRowCount;

      // Calculate the number of appendages we're going to have.
      int appCount = appPerFaceCount * (int)Face.FACE_COUNT;

      // Create the appendage positions.
      Vector3[,] faces = new Vector3[ (int)Face.FACE_COUNT, appPerFaceCount ];

      int breakoutCount = appCount * appCount;

      while( points.Count < appCount )
      {
        // Don't spin in this loop forever.
        if( --breakoutCount < 0 )
        {
          break;
        }

        // Randomly choose a face to add an appendage to.
        int f = Random.Range( 0, (int)Face.FACE_COUNT );

        // Randomly choose a position on the face.
        int p = Random.Range( 0, appPerFaceCount );

        // Is this position on this face already used?
        if( faces[ f, p ].magnitude > Mathf.Epsilon )
        {
          continue;
        }

        // Calculate the position of the appendage.
        faces[ f, p ] =
          CalculateAppendagePositionOnFace(
            (Face)f,
            Size,
            appPerRowCount,
            AppendageDiameter,
            p );

        points.Add( faces[ f, p ] );
      }
    }

    //-------------------------------------------------------------------------
    // Calculates the position where an appendage will be created on a
    // particular face on a cube.
    //
    // face: The face on which the appendage must be positioned.
    // cubeSize: Size of the cube on which the appendage is being placed.
    // appendagePerRowCount: Number of appendages on each row of a cube's face.
    // appendageDiameter: Diameter of appendage being added.
    // appendageIndexOnFace: Index in the range (0, max # appendages per face).

    private static Vector3 CalculateAppendagePositionOnFace(
      Face face,
      float cubeSize,
      int appendagePerRowCount,
      float appendageDiameter,
      int appendageIndexOnFace )
    {
      Vector3 position = new Vector3();

      // Calc the row & column on the face we're adding this appendage.
      int rowIndex = appendageIndexOnFace / appendagePerRowCount;
      int colIndex = appendageIndexOnFace % appendagePerRowCount;
      
      // Figure out position based on the face type.
      switch( face )
      {
        case Face.TOP:
          position.x = rowIndex * ( appendageDiameter * 2.0f );
          position.y = cubeSize * 0.5f;
          position.z = colIndex * ( appendageDiameter * 2.0f );
          break;

        case Face.BOTTOM:
          position.x = rowIndex * ( appendageDiameter * 2.0f );
          position.y = cubeSize * -0.5f;
          position.z = colIndex * ( appendageDiameter * 2.0f );
          break;

        case Face.NORTH:
          position.x = rowIndex * ( appendageDiameter * 2.0f );
          position.y = colIndex * ( appendageDiameter * 2.0f );
          position.z = cubeSize * 0.5f;
          break;

        case Face.EAST:
          position.x = cubeSize * 0.5f;
          position.y = rowIndex * ( appendageDiameter * 2.0f );
          position.z = colIndex * ( appendageDiameter * 2.0f );
          break;

        case Face.SOUTH:
          position.x = rowIndex * ( appendageDiameter * 2.0f );
          position.y = colIndex * ( appendageDiameter * 2.0f );
          position.z = cubeSize * -0.5f;
          break;

        case Face.WEST:
          position.x = cubeSize * -0.5f;
          position.y = rowIndex * ( appendageDiameter * 2.0f );
          position.z = colIndex * ( appendageDiameter * 2.0f );
          break;

        default:
          Debug.Assert( false );
          break;
      }

      return position;
    }

    //-------------------------------------------------------------------------
  }
}
