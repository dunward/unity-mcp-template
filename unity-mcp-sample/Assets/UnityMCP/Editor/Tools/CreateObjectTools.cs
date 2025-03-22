using UnityEngine;

public static class CreateObjectTools
{
    private class CreateObjectInput
    {
        public string shape;
        public Vector3 position;
    }

    public static void CreateObject(string format)
    {
        var input = JsonUtility.FromJson<CreateObjectInput>(format);

        switch (input.shape.ToLower())
        {
            case "cube":
                GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = input.position;
                break;
            case "sphere":
                GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = input.position;
                break;
            case "capsule":
                GameObject.CreatePrimitive(PrimitiveType.Capsule).transform.position = input.position;
                break;
        }
    }
}