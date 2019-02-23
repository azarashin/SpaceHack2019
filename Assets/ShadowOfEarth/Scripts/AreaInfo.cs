using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInfo : MonoBehaviour
{
    [SerializeField]
    AreaElement _elementPrefab; 

    [SerializeField]
    public class AreaInfoData
    {
        public AreaInfoData()
        {
            Name = "";
            Lat = 0.0f;
            Lng = 0.0f;
            Note = "";
        }
        public string Name;
        public float Lat;
        public float Lng;
        public string Note;
    }


    List<AreaElement> _elements = new List<AreaElement>(); 

    // Start is called before the first frame update
    void Start()
    {

        //保存先のファイル名
        string fileName = @"area_info.xml";


        //XmlSerializerオブジェクトを作成
        System.Xml.Serialization.XmlSerializer serializer2 =
            new System.Xml.Serialization.XmlSerializer(typeof(AreaInfoData[]));
        //読み込むファイルを開く
        System.IO.StreamReader sr = new System.IO.StreamReader(
            fileName, new System.Text.UTF8Encoding(false));
        //XMLファイルから読み込み、逆シリアル化する
        AreaInfoData[] objs = (AreaInfoData[])serializer2.Deserialize(sr);
        //ファイルを閉じる
        sr.Close();

        foreach(AreaInfoData aid in objs)
        {
            AreaElement ae = Instantiate(_elementPrefab, transform);
            ae.transform.position = new Vector3(
                (float)Math.Sin(aid.Lng * Math.PI / 180.0f) * 0.45f,
                (float)Math.Sin(aid.Lat * Math.PI / 180.0f) * 0.45f,
                (float)Math.Cos(aid.Lng * Math.PI / 180.0f) * 0.45f); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
