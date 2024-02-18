using Moarx.Math;

namespace Raytracing.Camera;
public class CameraTransform {

    public Transform renderFromCamera;
    public Transform worldFromRender;

    public CameraTransform(Transform worldFromCamera) {

        //Point3D<double> pCamera = worldFromCamera * new Point3D<double>(0,0,0);
        //worldFromRender = Transform.Translate(pCamera.ToVector());

        worldFromRender = new();

        Transform renderFromWorld = worldFromRender.Inverse();
        renderFromCamera = renderFromWorld * worldFromCamera; 

    }

}
