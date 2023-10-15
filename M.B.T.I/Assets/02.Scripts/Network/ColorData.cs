using System;
using System.IO;
public class ColorData
{
    public float r;
    public float g;
    public float b;
    public float a;

    // 보내는 값
    public static byte[] Serialize(object customobject)
    {
        ColorData cd = (ColorData)customobject;
        MemoryStream ms = new MemoryStream(sizeof(int) * 4);
        
        // 각 변수들을 Byte 형식으로 변환, 마지막은 개별 사이즈
        ms.Write(BitConverter.GetBytes(cd.r), 0, sizeof(float));
        ms.Write(BitConverter.GetBytes(cd.g), 0, sizeof(float));
        ms.Write(BitConverter.GetBytes(cd.b), 0, sizeof(float));
        ms.Write(BitConverter.GetBytes(cd.a), 0, sizeof(float));

        return ms.ToArray();        
    }

    // 전달받은 값
    public static object Deserialize(byte[] bytes)
    {
        ColorData cd = new ColorData();

        // 바이트 배열을 필요한 만큼 자르고, 원하는 자료형으로 변환
        cd.r = BitConverter.ToSingle(bytes, 0);
        cd.g = BitConverter.ToSingle(bytes, sizeof(float));
        cd.b = BitConverter.ToSingle(bytes, sizeof(float) * 2);
        cd.a = BitConverter.ToSingle(bytes, sizeof(float) * 3);

        return cd;
    }

}
