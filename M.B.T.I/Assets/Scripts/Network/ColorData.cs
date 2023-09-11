using System;
using System.IO;

public class ColorData
{
    public int r;
    public int g;
    public int b;
    public int a;

    public static byte[] Serialize(object customobject)
    {
        ColorData cd = (ColorData)customobject;
        MemoryStream ms = new MemoryStream(sizeof(int) * 4);
        
        // 각 변수들을 Byte 형식으로 변환, 마지막은 개별 사이즈
        ms.Write(BitConverter.GetBytes(cd.r), 0, sizeof(int));
        ms.Write(BitConverter.GetBytes(cd.g), 0, sizeof(int));
        ms.Write(BitConverter.GetBytes(cd.b), 0, sizeof(int));
        ms.Write(BitConverter.GetBytes(cd.a), 0, sizeof(int));
        
        return ms.ToArray();
        
        // byte[] result = new byte[4];
        // result[0] = cd.r;
        // result[1] = cd.g;
        // result[2] = cd.b;
        // result[3] = cd.a;

        // return result;
    }

    public static object Deserialize(byte[] bytes)
    {
        ColorData cd = new ColorData();
        
        // 바이트 배열을 필요한 만큼 자르고, 원하는 자료형으로 변환
        cd.r = BitConverter.ToInt32(bytes, sizeof(char));
        cd.g = BitConverter.ToInt32(bytes, sizeof(char));
        cd.b = BitConverter.ToInt32(bytes, sizeof(char));
        cd.a = BitConverter.ToInt32(bytes, sizeof(char));
        
        /*cd.r = bytes[0];
        cd.g = bytes[1];
        cd.b = bytes[2];
        cd.a = bytes[3];*/
        
        return cd;
    }
}
