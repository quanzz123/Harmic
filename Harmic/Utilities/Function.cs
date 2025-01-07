using SlugGenerator;
using System.Security.Cryptography;
using System.Text;

namespace Harmic.Utilities
{
    public class Function
    {
        //khai báo các biến nhăm lưu lại thông tin đăng nhập
        // trong quá trình thực hiện nếu kiểm tra không có thông tin này thì phải dăng nhập lại
        public static int _AccountId = 0;
        public static string _FullName = String.Empty;
        public static string _Email = String.Empty;
        public static string _Message = string.Empty;
        public static string _MessageEmail = string.Empty;


        public static string TitleSlugGenerationAlias(string title)
        {
            return SlugGenerator.SlugGenerator.GenerateSlug(title);
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strbui = new StringBuilder();
            for (int i=0; i< result.Length;i++)
           {
                strbui.Append(result[i].ToString("x2"));
            }
            return strbui.ToString();
        }
        public static string MD5Password(string? text)
        {
            string str = MD5Hash(text);
            //lặp thêm 5 lần mã hoá xâu đảm bảo tính bảo mật
            // mỗi lần lặp nhân đôi mã hoá, ở giữa thêm "_"
            for (int i=0; i<=5; i++)
            {
                str = MD5Hash(str + "_" + str);
                
            }
            return str;
        }
        public static bool IsLogin()
        {
            if(string.IsNullOrEmpty(Function._FullName) || string.IsNullOrEmpty(Function._Email) || (Function._AccountId <=0))
            {
                return false;
            }
            return true;
        }
        public static string getUsername(string? text)
        {
            
            return text;
        }
        public static string GetUsername1(string? fullname)
        {
            if (string.IsNullOrWhiteSpace(fullname))
            {
                return string.Empty; // Trả về chuỗi rỗng nếu `fullname` null hoặc toàn khoảng trắng
            }

            // Loại bỏ khoảng trắng thừa và tách chuỗi thành các từ
            var nameParts = fullname.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Trả về từ cuối cùng (thường là tên)
            return nameParts[^1];
        }
        public static string UploadImage(IFormFile image, string folder)
        {
            try
            {
                var FullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folder, image.FileName);
                using (var myfile = new FileStream(FullPath, FileMode.CreateNew))
                {
                    image.CopyTo(myfile);
                }
                return image.FileName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
