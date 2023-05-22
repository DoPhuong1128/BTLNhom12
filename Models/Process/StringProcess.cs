using System.Text.RegularExpressions;
namespace BTL_Nhom12.Models.Process
{
    public class StringProcess
    {
        //phuong thức nhập mã tự động
        public string AutoGenerateKey ( string strInput )
        {
            string strResult="", numPart="", strPart=""; //khai báo
            numPart=Regex.Match(strInput,@"\d+").Value; //truyền vào dữ liệu tách ra phần số(d) và chữ(D)
            strPart=Regex.Match(strInput,@"\D+").Value;
            int intPart=(Convert.ToInt32(numPart)+1);
            for (int i=0; i<numPart.Length - intPart.ToString().Length;i++)// BỔ SUNG CÁC PHẦN CHỮ SỐ CÒN THIẾU
            {
                strPart +=0;
            }
            strResult=strPart+intPart; //GHÉP CHỮ VÀ SỐ
            return strResult;
        }
    }
}