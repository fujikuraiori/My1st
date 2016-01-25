///郵便番号を保管するためのクラス
///1行の文字として受け取った場合と自分のコピーとして受け取った場合と全ての項目にそれぞれ直接入れる場合に対応している
namespace Tsuchidasan
{
    class CsvDataTable
    {
        public int Count = 15;
        public string NationalLocalGovernmentCode;
        public string OldPostalCode;
        public string PostalCode;
        public string Prefectures;
        public string City;
        public string TownArea;
        public string PrefecturesKANZI;
        public string CityKANZI;
        public string TownAreaKANZI;
        public string Case1;
        public string Case2;
        public string Case3;
        public string Case4;
        public string Case5;
        public string ReasonForChange;
        public CsvDataTable()
        {
            NationalLocalGovernmentCode = "";
            OldPostalCode = "";
            PostalCode = "";
            Prefectures = "";
            City = "";
            TownArea = "";
            PrefecturesKANZI = "";
            CityKANZI = "";
            TownAreaKANZI = "";
            Case1 = "";
            Case2 = "";
            Case3 = "";
            Case4 = "";
            Case5 = "";
            ReasonForChange = "";
        }
        public CsvDataTable(CsvDataTable copyData)
        {
            this.NationalLocalGovernmentCode = copyData.NationalLocalGovernmentCode;
            this.OldPostalCode = copyData.OldPostalCode;
            this.PostalCode = copyData.PostalCode;
            this.Prefectures = copyData.Prefectures;
            this.City = copyData.City;
            this.TownArea = copyData.TownArea;
            this.PrefecturesKANZI = copyData.PrefecturesKANZI;
            this.CityKANZI = copyData.CityKANZI;
            this.TownAreaKANZI = copyData.TownAreaKANZI;
            this.Case1 = copyData.Case1;
            this.Case2 = copyData.Case2;
            this.Case3 = copyData.Case3;
            this.Case4 = copyData.Case4;
            this.Case5 = copyData.Case5;
            this.ReasonForChange = copyData.ReasonForChange;
        }
        public CsvDataTable(string line) {  
                string[] csvDatas;
                csvDatas = line.Split(',');
                NationalLocalGovernmentCode = csvDatas[0];
                OldPostalCode = csvDatas[1];
                PostalCode = csvDatas[2];
                Prefectures = csvDatas[3];
                City = csvDatas[4];
                TownArea = csvDatas[5];
                PrefecturesKANZI = csvDatas[6];
                CityKANZI = csvDatas[7];
                TownAreaKANZI = csvDatas[8];
                Case1 = csvDatas[9];
                Case2 = csvDatas[10];
                Case3 = csvDatas[11];
                Case4 = csvDatas[12];
                Case5 = csvDatas[13];
                ReasonForChange = csvDatas[14];
        }
        public CsvDataTable( string NationalLocalGovernmentCode,string OldPostalCode,string PostalCode,string Prefectures,string City,
          string TownArea,string PrefecturesKANZI,string CityKANZI,string TownAreaKANZI,string Case1,string Case2,string Case3,string Case4,string Case5,string ReasonForChange)
        {
            this.NationalLocalGovernmentCode = NationalLocalGovernmentCode;
            this.OldPostalCode = OldPostalCode;
            this.PostalCode = PostalCode;
            this.Prefectures = Prefectures;
            this.City = City;
            this.TownArea = TownArea;
            this.PrefecturesKANZI = PrefecturesKANZI;
            this.CityKANZI = CityKANZI;
            this.TownAreaKANZI = TownAreaKANZI;
            this.Case1 = Case1;
            this.Case2 = Case2;
            this.Case3 = Case3;
            this.Case4 = Case4;
            this.Case5 = Case5;
            this.ReasonForChange = ReasonForChange;
        }
    }
}
