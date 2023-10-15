using System;
using System.IO;

public class IndexData
{
    public int index;

    // 보내는 값
    public static byte[] Serialize(object customobject)
    {
        IndexData id = (IndexData)customobject;
        MemoryStream ms = new MemoryStream(sizeof(int));

        // 각 변수들을 Byte 형식으로 변환, 마지막은 개별 사이즈
        ms.Write(BitConverter.GetBytes(id.index), 0, sizeof(int));

        return ms.ToArray();
    }

    // 전달받은 값
    public static object Deserialize(byte[] bytes)
    {
        IndexData id = new IndexData();

        // 바이트 배열을 필요한 만큼 자르고, 원하는 자료형으로 변환
        id.index = BitConverter.ToInt32(bytes, sizeof(char));

        return id;
    }
}
