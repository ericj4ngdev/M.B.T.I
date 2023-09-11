public class ColorData
{
    public byte r;
    public byte g;
    public byte b;
    public byte a;

    public static byte[] Serialize(object customobject)
    {
        ColorData cd = (ColorData)customobject;
        byte[] result = new byte[4];
        result[0] = cd.r;
        result[1] = cd.g;
        result[2] = cd.b;
        result[3] = cd.a;

        return result;
    }

    public static object Deserialize(byte[] bytes)
    {
        ColorData cd = new ColorData();
        cd.r = bytes[0];
        cd.g = bytes[1];
        cd.b = bytes[2];
        cd.a = bytes[3];
        return cd;
    }
}
