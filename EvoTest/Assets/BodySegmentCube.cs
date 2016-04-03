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
    public float AppendageDiameter { get; private set; }
    public float AppendageLength { get; private set; }

    //-------------------------------------------------------------------------
    // Constructor.

    public BodySegmentCube(
      float size,
      float appendageDiameterFactorOfBodySizeMin,
      float appendageDiameterFactorOfBodySizeMax,
      float appendageLengthFactorOfBodySizeMin,
      float appendageLengthFactorOfBodySizeMax )
    {
      Size = size;

      CalculateAppendageDiameterAndLength(
        appendageDiameterFactorOfBodySizeMin,
        appendageDiameterFactorOfBodySizeMax,
        appendageLengthFactorOfBodySizeMin,
        appendageLengthFactorOfBodySizeMax );
    }

    //-------------------------------------------------------------------------
    // Calculates what the diameter & length of appendages connected to this
    // body will be. The resulting diameter/length will be a random value
    // between the min & max values passed in.

    private void CalculateAppendageDiameterAndLength(
      float diameterFactorOfBodySizeMin,
      float diameterFactorOfBodySizeMax,
      float lengthFactorOfBodySizeMin,
      float lengthFactorOfBodySizeMax )
    {
      AppendageDiameter =
        Random.Range(
          Size * diameterFactorOfBodySizeMin,
          Size * diameterFactorOfBodySizeMax );

      AppendageLength =
        Random.Range(
          Size * lengthFactorOfBodySizeMin,
          Size * lengthFactorOfBodySizeMax );
    }

    //-------------------------------------------------------------------------
    // Implementation of BodySegment::CalculateAppendageProperties().
      
    public override void CalculateAppendageProperties(
      float fillFactor,
      out List<BodyAppendage> points )
    {
      points = new List<BodyAppendage>();

      // How many appendages can we fit per face of our cubic body?
      // We multiply the appendage-diameter by 2 since we want the appendages
      // to be spaced by at least their diameter.
      int appPerRowCount =
        (int)Mathf.Floor( Size / ( AppendageDiameter * 2.0f ) );

      int appPerFaceCount = appPerRowCount * appPerRowCount;

      // Calculate the number of appendages we're going to have.
      int appCount = appPerFaceCount * (int)Face.FACE_COUNT;

      // Create the appendage positions.
      BodyAppendage[,] faces =
        new BodyAppendage[ (int)Face.FACE_COUNT, appPerFaceCount ];

      int breakoutCount = appCount * appCount;

      while( points.Count < appCount * fillFactor )
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
        if( faces[ f, p ] != null )
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
            AppendageLength,
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

    private static BodyAppendage CalculateAppendagePositionOnFace(
      Face face,
      float cubeSize,
      int appendagePerRowCount,
      float appendageDiameter,
      float appendageLength,
      int appendageIndexOnFace )
    {
      BodyAppendage appendage = new BodyAppendage();

      appendage.Diameter = appendageDiameter;
      appendage.Length = appendageLength;

      // Calc the row & column on the face we're adding this appendage.
      int rowIndex = appendageIndexOnFace / appendagePerRowCount;
      int colIndex = appendageIndexOnFace % appendagePerRowCount;
      
      // Figure out position based on the face type.
      switch( face )
      {
        case Face.TOP:
          appendage.Position =
            new Vector3(
              ( -cubeSize * 0.5f ) + appendageDiameter + ( rowIndex * ( appendageDiameter * 2.0f ) ),
              ( cubeSize * 0.5f ) + ( appendageLength * 0.5f ),
              ( -cubeSize * 0.5f ) + appendageDiameter + ( colIndex * ( appendageDiameter * 2.0f ) ) );

          appendage.Rotation = Quaternion.Euler( 0f, 0f, 0f );
          break;

        case Face.BOTTOM:
          appendage.Position =
            new Vector3(
              ( -cubeSize * 0.5f ) + appendageDiameter + ( rowIndex * ( appendageDiameter * 2.0f ) ),
              ( cubeSize * -0.5f ) - ( appendageLength * 0.5f ),
              ( -cubeSize * 0.5f ) + appendageDiameter + ( colIndex * ( appendageDiameter * 2.0f ) ) );

          appendage.Rotation = Quaternion.Euler( 0f, 180f, 0f );
          break;

        case Face.NORTH:
          appendage.Position =
            new Vector3(
              ( -cubeSize * 0.5f ) + appendageDiameter + ( rowIndex * ( appendageDiameter * 2.0f ) ),
              ( -cubeSize * 0.5f ) + appendageDiameter + ( colIndex * ( appendageDiameter * 2.0f ) ),
              ( cubeSize * 0.5f ) + ( appendageLength * 0.5f ) );

          appendage.Rotation = Quaternion.Euler( 90f, 0f, 0f );
          break;

        case Face.EAST:
          appendage.Position =
            new Vector3(
              ( cubeSize * 0.5f ) + ( appendageLength * 0.5f ),
              ( -cubeSize * 0.5f ) + appendageDiameter + rowIndex * ( appendageDiameter * 2.0f ),
              ( -cubeSize * 0.5f ) + appendageDiameter + colIndex * ( appendageDiameter * 2.0f ) );

          appendage.Rotation = Quaternion.Euler( 0f, 0f, 90f );
          break;

        case Face.SOUTH:
          appendage.Position =
            new Vector3(
              ( -cubeSize * 0.5f ) + appendageDiameter + ( rowIndex * ( appendageDiameter * 2.0f ) ),
              ( -cubeSize * 0.5f ) + appendageDiameter + ( colIndex * ( appendageDiameter * 2.0f ) ),
              ( cubeSize * -0.5f ) - ( appendageLength * 0.5f ) );

          appendage.Rotation = Quaternion.Euler( 270f, 0f, 0f );
          break;

        case Face.WEST:
          appendage.Position =
            new Vector3(
              ( cubeSize * -0.5f ) - ( appendageLength * 0.5f ),
              ( -cubeSize * 0.5f ) + appendageDiameter + rowIndex * ( appendageDiameter * 2.0f ),
              ( -cubeSize * 0.5f ) + appendageDiameter + colIndex * ( appendageDiameter * 2.0f ) );

          appendage.Rotation = Quaternion.Euler( 0f, 0f, 270f );
          break;

        default:
          Debug.Assert( false );
          break;
      }

      return appendage;
    }

    //-------------------------------------------------------------------------
  }
}
