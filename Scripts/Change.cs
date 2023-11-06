using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Change : MonoBehaviour
{

    // Настройка

    public      Vector3     Translate   =   Vector3.zero        ;
    public      Vector3     Rotate      =   Vector3.zero        ; 
    public      Vector3     Scale       =   Vector3.one         ;

    public      static Vector3     RotateC     =   Vector3.zero        ; 
    

    public      float       width       =   1                   ;
    public      float       height      =   1                   ;
    public      float       ZFar        =   10000               ;
    public      float       ZNear       =   0.01f               ;
    [Range(1, 179)]
    public      int         ViewAngle   =   60                  ;
    public      bool        OrtEnable   =   false               ;
    public      bool        LookAtEnable=   false               ;
    public      bool        PersEnable  =   false               ;
    public      Transform   CamTransf                           ;


    private     float       p           =   Mathf.Deg2Rad       ;

    private     Vector4[]   Cube        =   new Vector4[8]      ;   
    private     Vector4[]   TransMatrix =   new Vector4[4]      ;
    private     Vector4[]   RotMatrixX  =   new Vector4[4]      ;
    private     Vector4[]   RotMatrixY  =   new Vector4[4]      ;
    private     Vector4[]   RotMatrixZ  =   new Vector4[4]      ;

    private     Vector4[]   RotMatrixXC =   new Vector4[4]      ;
    private     Vector4[]   RotMatrixYC =   new Vector4[4]      ;
    private     Vector4[]   RotMatrixZC =   new Vector4[4]      ;

    private     Vector4[]   RotMatrix   =   new Vector4[4]      ;
    private     Vector4[]   RotMatrixC  =   new Vector4[4]      ;
    private     Vector4[]   Scaling     =   new Vector4[4]      ;

    private     Vector4[]   Ortogonal   =   new Vector4[4]      ;
    private     Vector4[]   Perspectiv  =   new Vector4[4]      ;
    private     Vector4[]   Transfor    =   new Vector4[4]      ;


    // Конец настройки

    void OnDrawGizmos()
    {

        // * * * * * * * * * * * * * * * *
        //     Создание вершин куба
        // * * * * * * * * * * * * * * * *



        for (int i = 0; i < 4; i++)
        {

            Cube[   i   ] = new Vector4(

                Mathf.Cos((90 * i + 45) * Mathf.Deg2Rad) * Mathf.Sqrt(2) / 4 ,
                1f                                                       / 4 ,
                Mathf.Sin((90 * i + 45) * Mathf.Deg2Rad) * Mathf.Sqrt(2) / 4 ,
                0

            );

            Cube[ i + 4 ] = new Vector4(

                Mathf.Cos((90 * i + 45) * Mathf.Deg2Rad) * Mathf.Sqrt(2) / 4,
              - 1.0f                                                     / 4,   
                Mathf.Sin((90 * i + 45) * Mathf.Deg2Rad) * Mathf.Sqrt(2) / 4,
                0

            );
        }

     
        float f = 1 / Mathf.Tan(ViewAngle * p / 2f);

        // Транслейт Матрицы

        TransMatrix = new Vector4[]{
            new Vector4( 1                      , 0                         , 0                         , Translate.x               ),
            new Vector4( 0                      , 1                         , 0                         , Translate.y               ),
            new Vector4( 0                      , 0                         , 1                         , Translate.z               ),
            new Vector4( 0                      , 0                         , 0                         , 1                         )

        };

        // Матрицы вращении
        
        RotMatrixXC = new Vector4[]{
            new Vector4( 1                      ,  0                            ,  0                            ,  0                            ),
            new Vector4( 0                      ,  Mathf.Cos(-RotateC.x * p)    , -Mathf.Sin(-RotateC.x * p)    ,  0                            ),
            new Vector4( 0                      ,  Mathf.Sin(-RotateC.x * p)    ,  Mathf.Cos(-RotateC.x * p)    ,  0                            ),
            new Vector4( 0                      ,  0                            ,  0                            ,  1                            ),

        };

        RotMatrixYC = new Vector4[]{
            new Vector4( Mathf.Cos(-RotateC.y * p)  ,  0                        ,  Mathf.Sin(-RotateC.y * p)    ,  0                        ),
            new Vector4( 0                          ,  1                        ,  0                            ,  0                        ),
            new Vector4(-Mathf.Sin(-RotateC.y * p)  ,  0                        ,  Mathf.Cos(-RotateC.y * p)    ,  0                        ),
            new Vector4( 0                          ,  0                        ,  0                            ,  1                        ),

        };

        RotMatrixZC = new Vector4[]{
            new Vector4( Mathf.Cos(-RotateC.z * p), -Mathf.Sin(-RotateC.z * p)  ,  0                        ,  0                        ),
            new Vector4( Mathf.Sin(-RotateC.z * p),  Mathf.Cos(-RotateC.z * p)  ,  0                        ,  0                        ),
            new Vector4( 0                          ,  0                        ,  1                        ,  0                        ),
            new Vector4( 0                          ,  0                        ,  0                        ,  1                        )
            
        };

        RotMatrixX = new Vector4[]{
            new Vector4( 1                      ,  0                        ,  0                        ,  0                        ),
            new Vector4( 0                      ,  Mathf.Cos(Rotate.x * p)  , -Mathf.Sin(Rotate.x * p)  ,  0                        ),
            new Vector4( 0                      ,  Mathf.Sin(Rotate.x * p)  ,  Mathf.Cos(Rotate.x * p)  ,  0                        ),
            new Vector4( 0                      ,  0                        ,  0                        ,  1                        ),

        };

        RotMatrixY = new Vector4[]{
            new Vector4( Mathf.Cos(Rotate.y * p),  0                        ,  Mathf.Sin(Rotate.y * p)  ,  0                        ),
            new Vector4( 0                      ,  1                        ,  0                        ,  0                        ),
            new Vector4(-Mathf.Sin(Rotate.y * p),  0                        ,  Mathf.Cos(Rotate.y * p)  ,  0                        ),
            new Vector4( 0                      ,  0                        ,  0                        ,  1                        ),

        };

        RotMatrixZ = new Vector4[]{
            new Vector4( Mathf.Cos(Rotate.z * p), -Mathf.Sin(Rotate.z * p)  ,  0                        ,  0                        ),
            new Vector4( Mathf.Sin(Rotate.z * p),  Mathf.Cos(Rotate.z * p)  ,  0                        ,  0                        ),
            new Vector4( 0                      ,  0                        ,  1                        ,  0                        ),
            new Vector4( 0                      ,  0                        ,  0                        ,  1                        )

        };

        // Матрица Увеличении

        Scaling = new Vector4[]{
            new Vector4( Scale.x                , 0                         , 0                         , 0                         ),
            new Vector4( 0                      , Scale.y                   , 0                         , 0                         ),
            new Vector4( 0                      , 0                         , Scale.z                   , 0                         ),
            new Vector4( 0                      , 0                         , 0                         , 1                         )

        };

        

        // Матрица ортогонального проекции

        Ortogonal = OrtEnable ? new Vector4[]{
            new Vector4( 1/width                , 0                         , 0                         , 0                         ),
            new Vector4( 0                      , 1/height                  , 0                         , 0                         ),
            new Vector4( 0                      , 0                         ,-2/(ZFar-ZNear)            ,-(ZFar+ZNear)/(ZFar-ZNear) ),
            new Vector4( 0                      , 0                         , 0                         , 1                         )

        } : new Vector4[]{
            new Vector4( 1                      , 0                         , 0                         , 0                         ),
            new Vector4( 0                      , 1                         , 0                         , 0                         ),
            new Vector4( 0                      , 0                         , 1                         , 0                         ),
            new Vector4( 0                      , 0                         , 0                         , 1                         )
        };

        // Матрица перспективного проекции
        Perspectiv = PersEnable ? new Vector4[]{
            new Vector4( f                      , 0                         , 0                         , 0                         ),
            new Vector4( 0                      , f                         , 0                         , 0                         ),
            new Vector4( 0                      , 0                         , (ZFar+ZNear)/(ZNear-ZFar) , 2*ZFar*ZNear/(ZNear-ZFar) ),
            new Vector4( 0                      , 0                         , -1                        , 0                         )

        } : new Vector4[]{
            new Vector4( 1                      , 0                         , 0                         , 0                         ),
            new Vector4( 0                      , 1                         , 0                         , 0                         ),
            new Vector4( 0                      , 0                         , 1                         , 0                         ),
            new Vector4( 0                      , 0                         , 0                         , 1                         )
        };



        RotMatrix = MatrixProduct(MatrixProduct(RotMatrixX, RotMatrixY), RotMatrixZ); 
        RotMatrixC = MatrixProduct(MatrixProduct(RotMatrixXC, RotMatrixYC), RotMatrixZC);
        Transfor = MatrixProduct(MatrixProduct( TransMatrix, RotMatrix), Scaling);

        
        

        for (int i = 0; i < 8; i++)
        {
            Cube[i].w = 1;
            Cube[i] = MatrixMultVector(MatrixProduct(MatrixProduct(MatrixProduct(RotMatrixC,Perspectiv), Ortogonal), Transfor), Cube[i]);
            if (Cube[i].w != 0) Cube[i] /= Cube[i].w;
        }

        // * * * * * * * * * * * * * * * *
        // Часть, который рисует наш куб
        // * * * * * * * * * * * * * * * *

        

        Gizmos.color = Color.red;


        for (int i = 0; i < 4; i++)
        {
            if (i != 3)
            { 
                Gizmos.DrawLine(Cube[i], Cube[i + 1])       ;
                Gizmos.DrawLine(Cube[i+4], Cube[i + 1 + 4]) ;
                Gizmos.DrawLine(Cube[i], Cube[i + 4])       ;

            }
            else
            {
                
                Gizmos.DrawLine(Cube[i], Cube[0])           ;
                Gizmos.DrawLine(Cube[i + 4], Cube[4])       ;
                Gizmos.DrawLine(Cube[i], Cube[i + 4])       ;
            }
        }

    }

    public static Vector4[] MatrixProduct(Vector4[] a, Vector4[] b)
    {
        // Метод, уможающий матрицу а (4х4) на матрицу b (4х4)

        Vector4[] c = new Vector4[4];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    c[i][j] += a[i][k] * b[k][j]; 
                }
            }
        }

        return c;
    }

    public static Vector4 MatrixMultVector(Vector4[] a, Vector4 b)
    {

        // Метод, уможающий Матрицу (4х4) на 4D Вектор 

        Vector4 c = new Vector4();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                c[i] += a[i][j] * b[j];
            }
                
        }

        return c;
    }

}
